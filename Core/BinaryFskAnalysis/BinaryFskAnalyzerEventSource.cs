using System;

namespace Core.BinaryFskAnalysis
{
    public class BinaryFskAnalyzerEventSource
    {
        public event AnalysisCompletedEventHandler AnalysisCompleted;

        protected void AnalysisComplete(int baudRate, int boostFrequencyAmount, AnalysisResult analysisResult,
            string resultingString = null, bool? matched = null)
        {
            var e = new AnalysisResultEventArgs
            {
                BaudRate = baudRate,
                BoostFrequencyAmount = boostFrequencyAmount,
                AnalysisResult = analysisResult,
                ResultingString = resultingString,
                Matched = matched
            };

            AnalysisCompleted?.Invoke(this, e);
        }
    }
}
