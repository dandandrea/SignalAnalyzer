﻿using Core.AudioAnalysis;

namespace Core.BinaryFskAnalysis
{
    public delegate void AnalysisCompletedEventHandler(object sender, AnalysisResultEventArgs e);

    public interface IBinaryFskAnalyzer
    {
        AnalysisResult AnalyzeSignal(string testString = null);
        void Initialize(IAudioAnalyzer audioAnalyzer, IFrequencyDetector frequencyDetector,
            BinaryFskAnalyzerSettings binaryFskAnalzyerSettings);

        event AnalysisCompletedEventHandler AnalysisCompleted;
    }
}
