using System;

namespace Core.BinaryFskAnalysis
{
    public class BinaryFskAnalyzerEventSource
    {
        public event AnalysisCompletedEventHandler AnalysisCompleted;

        protected void AnalysisComplete(int boostFrequencyAmount, double minimumFrequencyDifference,
            double maximumFrequencyDifference, double averageFrequencyDifference, int numberOfMissedFrequencies,
            string resultingString = null, bool? matched = null)
        {
            var e = new AnalysisResultEventArgs
            {
                BoostFrequencyAmount = boostFrequencyAmount,
                MinimumFrequencyDifference = minimumFrequencyDifference,
                MaximumFrequencyDifference = maximumFrequencyDifference,
                AverageFrequencyDifference = averageFrequencyDifference,
                NumberOfMissedFrequencies = numberOfMissedFrequencies,
                ResultingString = resultingString,
                Matched = matched
            };

            AnalysisCompleted?.Invoke(this, e);
        }
    }

    public class AnalysisResultEventArgs : EventArgs
    {
        public int BoostFrequencyAmount { get; set; }
        public double AverageFrequencyDifference { get; set; }
        public double MinimumFrequencyDifference { get; set; }
        public double MaximumFrequencyDifference { get; set; }
        public int NumberOfMissedFrequencies { get; set; }
        public string ResultingString { get; set; }
        public bool? Matched { get; set; }
    }
}
