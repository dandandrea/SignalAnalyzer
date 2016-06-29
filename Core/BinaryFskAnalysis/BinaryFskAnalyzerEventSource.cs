using System;

namespace Core.BinaryFskAnalysis
{
    public class BinaryFskAnalyzerEventSource
    {
        public event AnalysisCompletedEventHandler AnalysisCompleted;
        public event SamplingCompletedEventHandler SamplingCompleted;

        protected void AnalysisComplete(int baudRate, int boostFrequencyAmount, double minimumFrequencyDifference,
            double maximumFrequencyDifference, double averageFrequencyDifference, int numberOfMissedFrequencies,
            int numberOfZeroFrequencies, string resultingString = null, bool? matched = null)
        {
            var e = new AnalysisResultEventArgs
            {
                BaudRate = baudRate,
                BoostFrequencyAmount = boostFrequencyAmount,
                MinimumFrequencyDifference = minimumFrequencyDifference,
                MaximumFrequencyDifference = maximumFrequencyDifference,
                AverageFrequencyDifference = averageFrequencyDifference,
                NumberOfMissedFrequencies = numberOfMissedFrequencies,
                NumberOfZeroFrequencies = numberOfZeroFrequencies,
                ResultingString = resultingString,
                Matched = matched
            };

            AnalysisCompleted?.Invoke(this, e);
        }

        protected void SamplingComplete(float[] samples)
        {
            var e = new SamplingResultEventArgs
            {
                Samples = samples
            };

            SamplingCompleted?.Invoke(this, e);
        }
    }

    public class AnalysisResultEventArgs : EventArgs
    {
        public int BaudRate { get; set; }
        public int BoostFrequencyAmount { get; set; }
        public double AverageFrequencyDifference { get; set; }
        public double MinimumFrequencyDifference { get; set; }
        public double MaximumFrequencyDifference { get; set; }
        public int NumberOfMissedFrequencies { get; set; }
        public int NumberOfZeroFrequencies { get; set; }
        public string ResultingString { get; set; }
        public bool? Matched { get; set; }
    }

    public class SamplingResultEventArgs : EventArgs
    {
        public float[] Samples { get; set; }
    }
}
