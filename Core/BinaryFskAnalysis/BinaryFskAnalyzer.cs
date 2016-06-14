using System.Collections.Generic;
using System.Linq;
using System;
using System.Diagnostics;
using Core.AudioAnalysis;

namespace Core.BinaryFskAnalysis
{
    public class BinaryFskAnalyzer : IBinaryFskAnalyzer
    {
        private IAudioAnalyzer _audioAnalyzer;

        public BinaryFskAnalyzer(IAudioAnalyzer audioAnalyzer)
        {
            if (audioAnalyzer == null)
            {
                throw new ArgumentNullException(nameof(_audioAnalyzer));
            }

            _audioAnalyzer = audioAnalyzer;
        }

        public void AnalyzeSignal(double baudRate, int spaceFrequency, int markFrequency,
            double windowPositionStartMilliseconds, double windowPositionIncrementMilliseconds,
            double? windowPositionEndMilliseconds, double windowLengthStartMilliseconds,
            double windowLengthEndMilliseconds, double windowLengthIncrementMilliseconds,
            double frequencyDeviationTolerance)
        {
            var n = 0;
            var bitNumber = 1;

            for (var currentWindowStart = windowPositionStartMilliseconds; currentWindowStart <= windowPositionEndMilliseconds; currentWindowStart += windowPositionIncrementMilliseconds)
            {
                for (var currentWindowLength = windowLengthStartMilliseconds; currentWindowLength <= windowLengthEndMilliseconds; currentWindowLength += windowLengthIncrementMilliseconds)
                {
                    var samples = _audioAnalyzer.GetSamples(currentWindowStart, currentWindowStart + currentWindowLength);

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
                    var markOrSpace = MarkOrSpace(averageFrequency, spaceFrequency, markFrequency);
                    if (frequencyDifference <= frequencyDeviationTolerance)
                    {
                        Debug.WriteLine($"[{currentWindowStart:N3} ms to {currentWindowStart + currentWindowLength:N3} ms ({(currentWindowStart + currentWindowLength) - currentWindowStart:N3} ms)] {frequencies.Average():N0} Hz average (+/- {frequencyDifference:N0} Hz) [Want {markFrequency:N0} Hz / {spaceFrequency:N0} Hz] -> bit {bitNumber}: {markOrSpace}");

                        // TODO: Remove this hack for swallowing the first bit
                        if (bitNumber == 1)
                        {
                            bitNumber = 5;
                            continue;
                        }

                        Console.Write(markOrSpace);

                        if (bitNumber % 4 == 0)
                        {
                            Console.Write(" ");
                        }

                        if (bitNumber % 72 == 0)
                        {
                            Console.WriteLine();
                        }

                        bitNumber++;
                    }

                    n++;
                }
            }
        }

        private static int FrequencyDifference(int frequency, int spaceFrequency, int markFrequency)
        {
            int distanceFromSpaceFrequency = Math.Abs(frequency - spaceFrequency);
            int distanceFromMarkFrequency = Math.Abs(frequency - markFrequency);

            return distanceFromSpaceFrequency <= distanceFromMarkFrequency ? distanceFromSpaceFrequency : distanceFromMarkFrequency;
        }

        private static int MarkOrSpace(int frequency, int spaceFrequency, int markFrequency)
        {
            int distanceFromSpaceFrequency = Math.Abs(frequency - spaceFrequency);
            int distanceFromMarkFrequency = Math.Abs(frequency - markFrequency);

            return distanceFromSpaceFrequency <= distanceFromMarkFrequency ? 0 : 1;
        }

        private class WindowSample
        {
            public int Frequency { get; set; }
            public int Distance { get; set; }
            public int WindowStartPosition { get; set; }
            public int WindowLength { get; set; }
        }
    }
}
