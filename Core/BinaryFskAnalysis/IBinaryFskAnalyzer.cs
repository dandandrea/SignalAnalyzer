using System.Collections.Generic;

namespace Core.BinaryFskAnalysis
{
    public interface IBinaryFskAnalyzer
    {
        void AnalyzeSignal(double baudRate, int spaceFrequency, int markFrequency,
            double windowPositionStartMilliseconds = 0.0, double windowPositionIncrementMilliseconds = 1.0,
            double? windowPositionEndMilliseconds = null, double windowLengthStartMilliseconds = 1.0,
            double windowLengthEndMilliseconds = 2.0, double windowLengthIncrementMilliseconds = 1.0,
            double frequencyDeviationTolerance = 20.0);
       }
}
