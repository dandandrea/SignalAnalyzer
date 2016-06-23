namespace Core.AudioAnalysis
{
    public interface IAudioAnalyzer
    {
        double FileLengthInMilliseconds { get; }
        int BoostFrequencyAmount { get; }
        SamplingResult GetSamples(double? startMilliseconds = null, double? endMilliseconds = null);
        void BoostFrequency(int frequency);
    }
}
