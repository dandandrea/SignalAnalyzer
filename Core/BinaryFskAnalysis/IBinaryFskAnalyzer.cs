using Core.AudioAnalysis;
using System.Collections.Generic;

namespace Core.BinaryFskAnalysis
{
    public delegate void AnalysisCompletedEventHandler(object sender, AnalysisResultEventArgs e);

    public interface IBinaryFskAnalyzer
    {
        ICollection<AnalysisResult> AnalyzeSignal(string testString = null);
        void Initialize(IAudioAnalyzer audioAnalyzer, IFrequencyDetector frequencyDetector,
            BinaryFskAnalyzerSettings binaryFskAnalzyerSettings);

        event AnalysisCompletedEventHandler AnalysisCompleted;
    }
}
