using System.Collections.Generic;

namespace Core.BinaryFskAnalysis
{
    public interface IBinaryFskAnalyzer
    {
        ICollection<bool> AnalyzeSignal();
    }
}
