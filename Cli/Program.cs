using System;
using System.Linq;
using Core.BinaryFskAnalysis;

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

            var windowSamples = BinaryFskAnalyzer.GetWindowedFrequencyCandidates(filename, windowPositionStart,
                windowPositionEnd, windowLengthStart, windowLengthEnd);

            var windowFrequencies = windowSamples.Select(x => x.FrequencyComponent).ToList();

            var frequencyCandidates = BinaryFskAnalyzer.GetFrequencyCandidates(windowFrequencies);

            foreach (var frequencyCandidate in frequencyCandidates)
            {
                Console.WriteLine($"(Looking for 2,025 Hz or 2,225 Hz) {frequencyCandidate} Hz");
            }

            Console.ReadLine();
        }
    }
}
