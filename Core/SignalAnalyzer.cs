using System;
using System.Collections.Generic;
using System.Numerics;
using NAudio.Wave;
using System.Diagnostics;

namespace Core
{
    public class SignalAnalyzer
    {
        public static SignalAnalysis AnalyzeSignal(string filename)
        {
            var reader = new WaveFileReader(filename);
            var bytes = new byte[reader.Length];
            reader.Read(bytes, 0, (int)reader.Length);

            var sampleRate = reader.WaveFormat.SampleRate;
            var bitsPerSample = reader.WaveFormat.BitsPerSample;
            var sampleLengthInBytes = reader.Length;
            var sampleLengthInMilliseconds = reader.TotalTime.Milliseconds;

            var shorts = new short[bytes.Length / 2];
            var j = 0;
            for (var i = 0; i < shorts.Length; i++)
            {
                shorts[i] = BitConverter.ToInt16(bytes, j);
                j = j + 2;
            }

            var complexes = new Complex[shorts.Length];
            for (var i = 0; i < shorts.Length; i++)
            {
                complexes[i] = new Complex(shorts[i], 0);
            }

            Debug.WriteLine("Performing FFT");
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            MathNet.Numerics.IntegralTransforms.Fourier.Forward(complexes);
            stopWatch.Stop();
            Debug.WriteLine($"Performed FFT in {stopWatch.ElapsedMilliseconds} milliseconds");

            var frequencyComponents = new List<FrequencyComponent>();
            for (var i = 0; i < complexes.Length / 2; i++)
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

            return new SignalAnalysis
            {
                SampleRate = sampleRate,
                BitsPerSample = bitsPerSample,
                SampleLengthInMilliseconds = sampleLengthInMilliseconds,
                SampleLengthInBytes = (int)sampleLengthInBytes,
                FrequencyComponents = frequencyComponents
            };
        }
    }
}
