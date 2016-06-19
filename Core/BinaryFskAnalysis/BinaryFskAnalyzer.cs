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
        private IFrequencyDetector _frequencyDetector;

        public BinaryFskAnalyzer(IAudioAnalyzer audioAnalyzer, IFrequencyDetector frequencyDetector)
        {
            if (audioAnalyzer == null)
            {
                throw new ArgumentNullException(nameof(audioAnalyzer));
            }

            if (frequencyDetector == null)
            {
                throw new ArgumentNullException(nameof(frequencyDetector));
            }

            _audioAnalyzer = audioAnalyzer;
            _frequencyDetector = frequencyDetector;
        }

        public ICollection<bool> AnalyzeSignal(double baudRate, int spaceFrequency, int markFrequency,
            double windowPositionStartMilliseconds, double windowPositionIncrementMilliseconds,
            double? windowPositionEndMilliseconds, double windowLengthStartMilliseconds,
            double windowLengthEndMilliseconds, double windowLengthIncrementMilliseconds,
            double frequencyDeviationTolerance)
        {
            var bits = new List<bool>();

            if (windowPositionEndMilliseconds == null)
            {
                windowPositionEndMilliseconds = _audioAnalyzer.FileLengthInMilliseconds;

                Debug.WriteLine($"Set window end position to total length of audio ({windowPositionEndMilliseconds} ms)");
            }

            for (var currentWindowStart = windowPositionStartMilliseconds; currentWindowStart <= windowPositionEndMilliseconds; currentWindowStart += windowPositionIncrementMilliseconds)
            {
                for (var currentWindowLength = windowLengthStartMilliseconds; currentWindowLength <= windowLengthEndMilliseconds; currentWindowLength += windowLengthIncrementMilliseconds)
                {
                    var samplingResult = _audioAnalyzer.GetSamples(currentWindowStart, currentWindowStart + currentWindowLength);
                    var frequency = _frequencyDetector.DetectFrequency(samplingResult.Samples);
                    var frequencyDifference = FrequencyDifference(frequency, spaceFrequency, markFrequency);
                    var markOrSpace = MarkOrSpace(frequency, spaceFrequency, markFrequency);

                    if (frequencyDifference > frequencyDeviationTolerance)
                    {
                        Debug.WriteLine($"WARN: Frequency outside of tolerance (frequency {frequency} Hz, difference {frequencyDifference} Hz, tolerance {frequencyDeviationTolerance} Hz)");
                        continue;
                    }

                    Debug.WriteLine($"[{currentWindowStart:N3} ms to {currentWindowStart + currentWindowLength:N3} ms ({(currentWindowStart + currentWindowLength) - currentWindowStart:N3} ms)] {frequency:N0} Hz average (+/- {frequencyDifference:N0} Hz) [Want {markFrequency:N0} Hz / {spaceFrequency:N0} Hz] -> bit {bits.Count}: {markOrSpace}");

                    bits.Add(markOrSpace == 0 ? false : true);
                }
            }

            return bits;
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
