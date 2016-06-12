using System.Collections.Generic;

namespace Core.BinaryFskAnalysis
{
    public interface IBinaryFskAnalyzer
    {
        ICollection<int> GetWindowedFrequencyCandidates(string filename,
            int windowPositionStart, int windowPositionEnd, int windowLengthStart, int windowLengthEnd,
            int numberOfClusters = 10, int cutoffFrequency = 4000);
    }
}
