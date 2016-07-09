using System.Collections.Generic;

namespace Core.BinaryFskAnalysis
{
    public class AnalysisResult
    {
        public int NumberOfFrequencyDifferences { get; set; }
        public int MinimumFrequencyDifference { get; set; }
        public int MaximumFrequencyDifference { get; set; }
        public double AverageFrequencyDifference { get; set; }
        public int NumberOfZeroFrequencies { get; set; }
        public ICollection<AnalysisFrame> AnalysisFrames { get; set; } = new List<AnalysisFrame>();
    }

    public class AnalysisFrame
    {
        public bool? Bit { get; set; }
        public int Frequency { get; set; }
        public int DifferenceFromExpectedFrequencies { get; set; }
        public bool DecodeFailure { get; set; }
        public double TimeOffsetMicroseconds { get; set; }
    }
}
