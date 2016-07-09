using System;

namespace Core.BinaryFskAnalysis
{
    public class AnalysisResultEventArgs : EventArgs
    {
        public int BaudRate { get; set; }
        public int BoostFrequencyAmount { get; set; }
        public AnalysisResult AnalysisResult { get; set; } = new AnalysisResult();
        public string ResultingString { get; set; }
        public bool? Matched { get; set; }

        // TODO: Needed for DataGridView / BindingList -- is there a better way to do this?
        public int NumberOfFrequencyDifferences { get { return AnalysisResult.NumberOfFrequencyDifferences; } }
        public int MinimumFrequencyDifference { get { return AnalysisResult.MinimumFrequencyDifference; } }
        public int MaximumFrequencyDifference { get { return AnalysisResult.MaximumFrequencyDifference; } }
        public double AverageFrequencyDifference { get { return AnalysisResult.AverageFrequencyDifference; } }
        public int NumberOfZeroFrequencies { get { return AnalysisResult.NumberOfZeroFrequencies; } }
    }
}
