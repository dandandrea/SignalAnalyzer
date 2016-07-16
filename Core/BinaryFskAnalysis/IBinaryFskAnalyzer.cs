using Core.AudioAnalysis;

namespace Core.BinaryFskAnalysis
{
    public interface IBinaryFskAnalyzer
    {
        AnalysisResult AnalyzeSignal(string testString = null);
        void Initialize(IAudioAnalyzer audioAnalyzer, IFrequencyDetector frequencyDetector,
            BinaryFskAnalyzerSettings binaryFskAnalzyerSettings);
    }
}
