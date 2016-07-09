namespace Core.BinaryFskAnalysis
{
    public class BinaryFskAnalyzerSettings
    {
        public double BaudRate { get; set; }
        public int SpaceFrequency { get; set; }
        public int MarkFrequency { get; set; }
        public double WindowPositionStartMicroseconds { get; set; } = 0.0;
        public double? WindowPositionIncrementMicroseconds { get; set; }
        public double? WindowPositionEndMicroseconds { get; set; }
        public double? WindowLengthStartMicroseconds { get; set; }
        public double? WindowLengthIncrementMicroseconds { get; set; }
        public double? WindowLengthEndMicroseconds { get; set; }
        public double FrequencyDeviationTolerance { get; set; } = 20.0;

        public BinaryFskAnalyzerSettings() {}

        public BinaryFskAnalyzerSettings(BinaryFskAnalyzerSettings sourceSettings)
        {
            BaudRate = sourceSettings.BaudRate;
            SpaceFrequency = sourceSettings.SpaceFrequency;
            MarkFrequency = sourceSettings.MarkFrequency;
            FrequencyDeviationTolerance = sourceSettings.FrequencyDeviationTolerance;
        }
    }
}
