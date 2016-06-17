namespace Core.AudioAnalysis
{
    public interface IAudioAnalyzer
    {
        double FileLengthInMilliseconds { get; }
        SamplingResult GetSamples(double? startMilliseconds = null, double? endMilliseconds = null);
    }
}
