using System;

namespace Core.BinaryFskAnalysis
{
    public class BinaryFskAnalyzerEventSource
    {
        public event AnalysisCompletedEventHandler AnalysisCompleted;

        protected void AnalysisComplete(int boostFrequencyAmount, double averageFrequencyDifference, int numberOfMissedFrequencies,
            string resultingString = null, bool? matched = null)
        {
            var e = new AnalysisResultEventArgs
            {
                BoostFrequencyAmount = boostFrequencyAmount,
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
        public int NumberOfMissedFrequencies { get; set; }
        public string ResultingString { get; set; }
        public bool? Matched { get; set; }
    }
}
