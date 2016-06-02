using Core;

namespace Cli
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // SignalAnalyzer.GetFrequencyComponents(@"c:\Users\laptop\Desktop\SDR\Signal analysis\SDRSharp_20160529_172631Z_929612500Hz_AF_original.wav");
            SignalAnalyzer.GetFrequencyComponents(@"c:\Users\laptop\Desktop\SDR\Signal analysis\2600.wav");
        }
    }
}
