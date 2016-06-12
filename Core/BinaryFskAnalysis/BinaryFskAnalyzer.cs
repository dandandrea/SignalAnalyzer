using Accord.MachineLearning;
using System.Collections.Generic;
using System.Linq;
using Core.SignalAnalysis;
using System;

namespace Core.BinaryFskAnalysis
{
    public class BinaryFskAnalyzer : IBinaryFskAnalyzer
    {
        private ISignalAnalyzer _signalAnalyzer;

        public BinaryFskAnalyzer(ISignalAnalyzer signalAnalyzer)
        {
            if (signalAnalyzer == null)
            {
                throw new ArgumentNullException(nameof(signalAnalyzer));
            }

            _signalAnalyzer = signalAnalyzer;
        }

        public ICollection<int> GetWindowedFrequencyCandidates(string filename,
            int windowPositionStart, int windowPositionEnd, int windowLengthStart, int windowLengthEnd,
            int numberOfClusters, int cutoffFrequency)
        {
            var frequencyComponents = new List<FrequencyComponent>();

            for (var windowStart = windowPositionStart; windowStart < windowPositionEnd; windowStart++)
            {
                for (var windowLength = windowLengthStart; windowLength <= windowLengthEnd; windowLength++)
                {
                    var signalAnalysis = _signalAnalyzer.AnalyzeSignal(filename, windowStart, windowStart + windowLength);

                    frequencyComponents.Add(signalAnalysis.FrequencyComponents.First());
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
    }
}
