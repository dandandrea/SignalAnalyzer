using Accord.MachineLearning;
using System.Collections.Generic;
using System.Linq;
using Core.SignalAnalysis;
using System;

namespace Core.BinaryFskAnalysis
{
    public class BinaryFskAnalyzer
    {
        public static ICollection<WindowSample> GetWindowedFrequencyCandidates(string filename,
            int windowPositionStart, int windowPositionEnd, int windowLengthStart, int windowLengthEnd,
            int numberOfClusters = 10, int cutoffFrequency = 4000)
        {
            var windowSamples = new List<WindowSample>();

            for (var windowStart = windowPositionStart; windowStart < windowPositionEnd; windowStart++)
            {
                for (var windowLength = windowLengthStart; windowLength <= windowLengthEnd; windowLength++)
                {
                    var signalAnalysis = SignalAnalyzer.AnalyzeSignal(filename, windowStart, windowStart + windowLength);

                    windowSamples.Add(
                        new WindowSample
                        {
                            WindowStart = windowStart,
                            WindowLength = windowLength,
                            FrequencyComponent = signalAnalysis.FrequencyComponents.First()
                        }
                    );
                }
            }

            return windowSamples;
        }

        public static ICollection<int> GetFrequencyCandidates(IList<FrequencyComponent> frequencyComponents,
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
