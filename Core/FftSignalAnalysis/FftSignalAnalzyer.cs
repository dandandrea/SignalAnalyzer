using System;
using System.Collections.Generic;
using System.Numerics;
using MathNet.Numerics;
using MathNet.Numerics.IntegralTransforms;
using Core.AudioAnalysis;
using Accord.MachineLearning;
using System.Linq;

namespace Core.SignalAnalysis
{
    public class SignalAnalyzer : IFftSignalAnalyzer
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

        public FftSignalAnalysisResult AnalyzeSignal(double? startMicroseconds,
            double? endMicroseconds, int minFftSize, int maxFftSize)
        {
            if ((startMicroseconds != null && endMicroseconds == null) || (startMicroseconds == null && endMicroseconds != null))
            {
                throw new ArgumentOutOfRangeException("startMicroseconds and endMicroseconds must be either both null or both not null", (Exception)null);
            }

            if (startMicroseconds != null && endMicroseconds != null && endMicroseconds <= startMicroseconds)
            {
                throw new ArgumentOutOfRangeException($"endMicroseconds ({endMicroseconds}) must be greater than startMicroseconds ({startMicroseconds})", (Exception)null);
            }

            var samplingResult = _audioAnalyzer.GetSamples(startMicroseconds, endMicroseconds);

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

            return new FftSignalAnalysisResult
            {
                SamplingResult = samplingResult,
                NumberOfBins = fftSize,
                BinSizeInHertz = samplingResult.SampleRate / fftSize,
                FrequencyComponents = frequencyComponents
            };
        }

        public ICollection<int> GetFrequencyCandidates(string filename,
            int windowPositionStart, int windowPositionEnd, int windowLengthStart, int windowLengthEnd,
            int numberOfClusters, int cutoffFrequency)
        {
            if (string.IsNullOrEmpty(filename))
            {
                throw new ArgumentNullException(nameof(filename), "filename must not be null or empty");
            }

            var frequencyComponents = new List<FrequencyComponent>();

            for (var currentWindowStart = windowPositionStart; currentWindowStart <= windowPositionEnd; currentWindowStart++)
            {
                for (var currentWindowLength = windowLengthStart; currentWindowLength <= windowLengthEnd; currentWindowLength++)
                {
                    var signalAnalysisResult = ((IFftSignalAnalyzer)this).AnalyzeSignal(currentWindowStart, currentWindowStart + currentWindowLength);

                    frequencyComponents.Add(signalAnalysisResult.FrequencyComponents.First());
                }
            }

            return GetFrequencyCandidates(frequencyComponents);
        }

        private static ICollection<int> GetFrequencyCandidates(IList<FrequencyComponent> frequencyComponents,
            int numberOfClusters = 10, int cutoffFrequency = 4000)
        {
            var filteredFrequencyComponents = frequencyComponents.Where(y => y.Frequency <= cutoffFrequency).ToList();

            var weightedFrequencies = WeightFrequencyComponents(filteredFrequencyComponents);

            var observations = weightedFrequencies.Select(x => new double[] { x }).ToArray();

            var kMeans = new KMeans(numberOfClusters);
            kMeans.Compute(observations);
            return kMeans.Clusters.Select(x => (int)x.Mean[0]).ToList();
        }

        private static ICollection<double> WeightFrequencyComponents(ICollection<FrequencyComponent> frequencyComponents,
            int magnitudeDivisonFactor = 10)
        {
            var weightedFrequencies = new List<double>();

            foreach (var frequencyComponent in frequencyComponents)
            {
                for (int i = 0; i < frequencyComponent.Magnitude / magnitudeDivisonFactor; i++)
                {
                    weightedFrequencies.Add(frequencyComponent.Frequency);
                }
            }

            return weightedFrequencies;
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
