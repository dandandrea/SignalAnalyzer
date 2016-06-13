using System;
using System.Linq;
using System.Collections.Generic;
using Core.AudioAnalysis;

namespace Cli
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var filename = @"c:\Users\laptop\Desktop\SDR\Signal analysis\Emergency Alert System preamble.wav";

            var baudRate = 520.83;
            var spaceFrequency = 2083;
            var markFrequency = 1563;

            var windowPositionStart = 400.0;
            var windowPositionIncrement = 1.0 / baudRate * 1000.0 / 2.0;
            var windowPositionEnd = windowPositionIncrement * 500.0;
            var windowLengthStart = 1.0 / baudRate * 1000.0 / 2.0;
            var windowLengthIncrement = 1.0 / baudRate * 1000.0 / 2.0;
            var windowLengthEnd = windowLengthStart + windowLengthIncrement * 4.0;;

            Console.WriteLine($"Window position start {windowPositionStart:N3}, window position end {windowPositionEnd:N3}, window position increment {windowPositionIncrement:N3}");
            Console.WriteLine($"Window length start {windowLengthStart:N3}, window length end {windowLengthEnd:N3}, window length increment {windowLengthIncrement:N3}");
            Console.WriteLine();

            var audioAnalyzer = (IAudioAnalyzer)new AudioAnalyzer(filename);

            var n = 0;

            for (var currentWindowStart = windowPositionStart; currentWindowStart <= windowPositionEnd; currentWindowStart += windowPositionIncrement)
            {
                for (var currentWindowLength = windowLengthStart; currentWindowLength <= windowLengthEnd; currentWindowLength += windowLengthIncrement)
                {
                    var samples = audioAnalyzer.GetSamples(currentWindowStart, currentWindowStart + currentWindowLength);

                    var frequencies = new List<int>();

                    int? lastSign = null;
                    double? lastSignChangeMilliseconds = null;
                    for (var i = 0; i < samples.Samples.Count; i++)
                    {
                        if (lastSign != null && (lastSign == 1 && samples.Samples[i] < 0) || (lastSign == 0 && samples.Samples[i] >= 0))
                        {
                            var currentTime = i * 1.0 / samples.SampleRate * 1000.0;

                            if (lastSignChangeMilliseconds != null)
                            {
                                var timeDifference = currentTime - lastSignChangeMilliseconds;
                                var frequency = (int)(1.0 / timeDifference * 1000.0);
                                frequencies.Add(frequency);
                            }

                            lastSignChangeMilliseconds = currentTime;
                        }

                        lastSign = samples.Samples[i] >= 0 ? 1 : -1;
                    }

                    var averageFrequency = (int)frequencies.Average();
                    var frequencyDifference = FrequencyDifference(averageFrequency, spaceFrequency, markFrequency);
                    Console.WriteLine($"[{currentWindowStart:N3} ms to {currentWindowStart + currentWindowLength:N3} ms ({(currentWindowStart + currentWindowLength) - currentWindowStart:N3} ms)] {frequencies.Average():N0} Hz average (+/- {frequencyDifference:N0} Hz) [Want {markFrequency:N0} Hz / {spaceFrequency:N0} Hz]");

                    if ((n + 1) % 20 == 0)
                    {
                        Console.ReadLine();
                    }

                    n++;
                }
            }

            Console.WriteLine("Done");
            Console.ReadLine();
        }

        private static int FrequencyDifference(int frequency, int spaceFrequency, int markFrequency)
        {
            int distanceFromSpaceFrequency = Math.Abs(frequency - spaceFrequency);
            int distanceFromMarkFrequency = Math.Abs(frequency - markFrequency);

            return distanceFromSpaceFrequency <= distanceFromMarkFrequency ? distanceFromSpaceFrequency : distanceFromMarkFrequency;
        }
    }
}
