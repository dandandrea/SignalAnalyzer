namespace Core.AudioAnalysis
{
    public interface IAudioAnalyzer
    {
        double FileLengthInMicroseconds { get; }
        int BoostFrequencyAmount { get; }
        int SampleRate { get; }
        SamplingResult GetSamples(double? startMicroseconds = null, double? endMicroseconds = null);
        void BoostFrequency(int frequency);
    }
}
