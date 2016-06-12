using System;
using System.Collections.Generic;
using System.Numerics;
using NAudio.Wave;
using MathNet.Numerics;
using MathNet.Numerics.IntegralTransforms;

namespace Core.SignalAnalysis
{
    public class SignalAnalyzer : ISignalAnalyzer
    {
        public SignalAnalysisResult AnalyzeSignal(string filename, double? startMilliseconds,
            double? endMilliseconds, int minFftSize, int maxFftSize)
        {
            if ((startMilliseconds != null && endMilliseconds == null) || (startMilliseconds == null && endMilliseconds != null))
            {
                throw new ArgumentOutOfRangeException("startMilliseconds and endMilliseconds must be either both null or both not null", (Exception)null);
            }

            if (startMilliseconds != null && endMilliseconds != null && endMilliseconds <= startMilliseconds)
            {
                throw new ArgumentOutOfRangeException($"endMilliseconds ({endMilliseconds}) must be greater than startMilliseconds ({startMilliseconds})", (Exception)null);
            }

            var reader = new WaveFileReader(filename);
            var bytes = new byte[reader.Length];
            reader.Read(bytes, 0, (int)reader.Length);

            var sampleRate = reader.WaveFormat.SampleRate;
            var bitsPerSample = reader.WaveFormat.BitsPerSample;
            var fileLengthInBytes = reader.Length;
            var fileLengthInMilliseconds = reader.TotalTime.Milliseconds;

            if (startMilliseconds == null)
            {
                startMilliseconds = 0;
                endMilliseconds = fileLengthInMilliseconds;
            }

            var sampleLengthInMilliseconds = endMilliseconds - startMilliseconds;
            var numberOfSamples = (int)(sampleRate * sampleLengthInMilliseconds / 1000.0);
            var firstSampleNumber = (int)(startMilliseconds / 1000.0 * sampleRate);
            var lastSampleNumber = (int)(endMilliseconds / 1000.0 * sampleRate);
            int fftSize = NextPowerOfTwo(numberOfSamples, minFftSize, maxFftSize);

            var shorts = new short[numberOfSamples];
            var j = firstSampleNumber;
            for (var i = 0; i < numberOfSamples; i++)
            {
                shorts[i] = BitConverter.ToInt16(bytes, j);
                j = j + 2;
            }

            var window = Window.Hann(fftSize);

            var complexes = new Complex[fftSize];
            for (var i = 0; i < (fftSize > numberOfSamples ? numberOfSamples : fftSize); i++)
            {
                complexes[i] = new Complex(shorts[i] * window[i], 0);
            }

            Fourier.Forward(complexes);

            var frequencyComponents = new List<FrequencyComponent>();
            for (var i = 1; i < complexes.Length / 2; i++)
            {
                var freq = i * sampleRate / complexes.Length;

                var frequencyComponent = new FrequencyComponent
                {
                    Frequency = freq,
                    Magnitude = complexes[i].Magnitude,
                    BinNumber = i
                };

                frequencyComponents.Add(frequencyComponent);
            }

            frequencyComponents.Sort((a, b) => b.Magnitude.CompareTo(a.Magnitude));

            return new SignalAnalysisResult
            {
                SampleRate = sampleRate,
                BitsPerSample = bitsPerSample,
                NumberOfBins = fftSize,
                BinSizeInHertz = sampleRate / fftSize,
                FileLengthInMilliseconds = fileLengthInMilliseconds,
                FileLengthInBytes = (int)fileLengthInBytes,
                FrequencyComponents = frequencyComponents
            };
        }

        private static int NextPowerOfTwo(int n, int minPowerOfTwo, int maxPowerOfTwo)
        {
            int powerOfTwo = minPowerOfTwo;

            for (var i = 1; i <= n; i++)
            {
                if (i > powerOfTwo)
                {
                    powerOfTwo = powerOfTwo * 2;
                }

                if (i == maxPowerOfTwo)
                {
                    break;
                }
            }

            return powerOfTwo;
        }
    }
}
