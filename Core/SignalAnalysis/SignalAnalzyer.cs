using System;
using System.Collections.Generic;
using System.Numerics;
using MathNet.Numerics;
using MathNet.Numerics.IntegralTransforms;
using Core.AudioAnalysis;

namespace Core.SignalAnalysis
{
    public class SignalAnalyzer : ISignalAnalyzer
    {
        private IAudioAnalyzer _audioAnalyzer;

        public SignalAnalyzer(IAudioAnalyzer audioAnalyzer)
        {
            if (audioAnalyzer == null)
            {
                throw new ArgumentNullException(nameof(audioAnalyzer));
            }

            _audioAnalyzer = audioAnalyzer;
        }

        public SignalAnalysisResult AnalyzeSignal(double? startMilliseconds,
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

            var samplingResult = _audioAnalyzer.GetSamples(startMilliseconds, endMilliseconds);

            int fftSize = NextPowerOfTwo(samplingResult.Samples.Count, minFftSize, maxFftSize);

            var window = Window.Hann(fftSize);

            var complexes = new Complex[fftSize];
            for (var i = 0; i < (fftSize > samplingResult.Samples.Count ? samplingResult.Samples.Count : fftSize); i++)
            {
                complexes[i] = new Complex(samplingResult.Samples[i] * window[i], 0);
            }

            Fourier.Forward(complexes);

            var frequencyComponents = new List<FrequencyComponent>();
            for (var i = 1; i < complexes.Length / 2; i++)
            {
                var freq = i * samplingResult.SampleRate / complexes.Length;

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
                SamplingResult = samplingResult,
                NumberOfBins = fftSize,
                BinSizeInHertz = samplingResult.SampleRate / fftSize,
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
