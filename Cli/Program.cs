using System;
using System.Linq;
using Core.BinaryFskAnalysis;
using Core.SignalAnalysis;

namespace Cli
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var filename = @"c:\Users\laptop\Desktop\SDR\Signal analysis\bell_103_payload.wav";

            var windowPositionStart = 0;
            var windowPositionEnd = 100;
            var windowLengthStart = 3;
            var windowLengthEnd = 5;

            var binaryFskAnalyzer = (IBinaryFskAnalyzer)new BinaryFskAnalyzer(new SignalAnalyzer());

            var frequencyCandidates = binaryFskAnalyzer.GetWindowedFrequencyCandidates(filename, windowPositionStart,
                windowPositionEnd, windowLengthStart, windowLengthEnd);

            foreach (var frequencyCandidate in frequencyCandidates)
            {
                Console.WriteLine($"(Looking for 2,025 Hz or 2,225 Hz) {frequencyCandidate} Hz");
            }

            Console.ReadLine();
        }
    }
}
