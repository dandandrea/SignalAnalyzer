namespace Core.AudioAnalysis
{
    public interface IAudioAnalyzer
    {
        SamplingResult GetSamples(double? startMilliseconds = null, double? endMilliseconds = null);
    }
}
