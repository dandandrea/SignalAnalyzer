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
                    var samplingResult = _audioAnalyzer.GetSamples(currentWindowStart, currentWindowStart + currentWindowLength);

                    var signChangeDetector = new SignChangeDetector(samplingResult.SampleRate);

                    var frequencies = new List<int>();

                    for (var sampleNumber = 0; sampleNumber < samplingResult.Samples.Count; sampleNumber++)
                    {
                        var currentTimeMilliseconds = sampleNumber * 1.0 / samplingResult.SampleRate * 1000.0;

                        var signChangeResult = signChangeDetector.DetectSignChange(samplingResult.Samples[sampleNumber], currentTimeMilliseconds);

                        if (signChangeResult.SignChanged == true && signChangeResult.TimeDifferenceMilliseconds != null)
                        {
                            var frequency = (int)(1.0 / signChangeResult.TimeDifferenceMilliseconds * 1000.0);
                            frequencies.Add(frequency);
                        }
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

        private class SignChangeDetector
        {
            private int _sampleRateSeconds;
            private int? _lastSign = null;
            private double? _lastSignChangeMilliseconds { get; set; } = null;

            public SignChangeDetector(int sampleRateSeconds)
            {
                _sampleRateSeconds = sampleRateSeconds;
            }

            public SignChangeResult DetectSignChange(short sample, double currentTimeMilliseconds)
            {
                bool signChanged = false;

                if (_lastSign != null && (_lastSign == 1 && sample < 0) || (_lastSign == 0 && sample >= 0))
                {
                    signChanged = true;
                }

                var signChangeResult = new SignChangeResult
                {
                    SignChanged = signChanged,
                    TimeDifferenceMilliseconds = _lastSignChangeMilliseconds != null ? currentTimeMilliseconds - _lastSignChangeMilliseconds : null
                };

                if (signChanged == true)
                {
                    _lastSignChangeMilliseconds = currentTimeMilliseconds;
                }

                _lastSign = sample >= 0 ? 1 : -1;

                return signChangeResult;
            }
        }

        internal class SignChangeResult
        {
            public bool SignChanged { get; set; } = false;
            public double? TimeDifferenceMilliseconds { get; set; }
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
