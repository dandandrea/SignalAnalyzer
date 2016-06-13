namespace Core.SignalAnalysis
{
    public interface ISignalAnalyzer
    {
        SignalAnalysisResult AnalyzeSignal(double? startMilliseconds = null,
            double? endMilliseconds = null, int minFftSize = 8192, int maxFftSize = 16384);
    }
}
