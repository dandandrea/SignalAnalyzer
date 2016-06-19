namespace Core.BinaryFskAnalysis
{
    public class BinaryFskAnalyzerSettings
    {
        public double BaudRate { get; set; }
        public int SpaceFrequency { get; set; }
        public int MarkFrequency { get; set; }
        public double WindowPositionStartMilliseconds { get; set; } = 0.0;
        public double? WindowPositionIncrementMilliseconds { get; set; }
        public double? WindowPositionEndMilliseconds { get; set; }
        public double? WindowLengthStartMilliseconds { get; set; }
        public double? WindowLengthIncrementMilliseconds { get; set; }
        public double? WindowLengthEndMilliseconds { get; set; }
        public double FrequencyDeviationTolerance { get; set; } = 20.0;

        public BinaryFskAnalyzerSettings() {}

        public BinaryFskAnalyzerSettings(BinaryFskAnalyzerSettings sourceSettings)
        {
            BaudRate = sourceSettings.BaudRate;
            SpaceFrequency = sourceSettings.SpaceFrequency;
            MarkFrequency = sourceSettings.MarkFrequency;
            WindowPositionStartMilliseconds = sourceSettings.WindowPositionStartMilliseconds;
            WindowPositionIncrementMilliseconds = sourceSettings.WindowPositionIncrementMilliseconds;
            WindowPositionEndMilliseconds = sourceSettings.WindowPositionEndMilliseconds;
            WindowLengthStartMilliseconds = sourceSettings.WindowLengthStartMilliseconds;
            WindowLengthIncrementMilliseconds = sourceSettings.WindowLengthIncrementMilliseconds;
            WindowLengthEndMilliseconds = sourceSettings.WindowLengthEndMilliseconds;
            FrequencyDeviationTolerance = sourceSettings.FrequencyDeviationTolerance;
        }
    }
}
