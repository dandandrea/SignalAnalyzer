using Core.SignalAnalysis;

namespace Core.BinaryFskAnalysis
{
    public class WindowSample
    {
        public int WindowStart { get; set; }
        public int WindowLength { get; set; }
        public FrequencyComponent FrequencyComponent { get; set; }
    }
}
