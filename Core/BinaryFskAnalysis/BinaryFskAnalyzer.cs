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

        public ICollection<bool> AnalyzeSignal(double baudRate, int spaceFrequency, int markFrequency,
            double windowPositionStartMilliseconds, double windowPositionIncrementMilliseconds,
            double? windowPositionEndMilliseconds, double windowLengthStartMilliseconds,
            double windowLengthEndMilliseconds, double windowLengthIncrementMilliseconds,
            double frequencyDeviationTolerance)
        {
            var bits = new List<bool>();

            for (var currentWindowStart = windowPositionStartMilliseconds; currentWindowStart <= windowPositionEndMilliseconds; currentWindowStart += windowPositionIncrementMilliseconds)
            {
                for (var currentWindowLength = windowLengthStartMilliseconds; currentWindowLength <= windowLengthEndMilliseconds; currentWindowLength += windowLengthIncrementMilliseconds)
                {
                    var samplingResult = _audioAnalyzer.GetSamples(currentWindowStart, currentWindowStart + currentWindowLength);

                    var frequencyDetector = new FrequencyDetector(new SignChangeDetector(samplingResult.SampleRate));
                    var frequency = frequencyDetector.DetectFrequency(samplingResult.Samples);

                    var frequencyDifference = FrequencyDifference(frequency, spaceFrequency, markFrequency);
                    var markOrSpace = MarkOrSpace(frequency, spaceFrequency, markFrequency);

                    if (frequencyDifference <= frequencyDeviationTolerance)
                    {
                        Debug.WriteLine($"[{currentWindowStart:N3} ms to {currentWindowStart + currentWindowLength:N3} ms ({(currentWindowStart + currentWindowLength) - currentWindowStart:N3} ms)] {frequency:N0} Hz average (+/- {frequencyDifference:N0} Hz) [Want {markFrequency:N0} Hz / {spaceFrequency:N0} Hz] -> bit {bits.Count}: {markOrSpace}");

                        bits.Add(markOrSpace == 0 ? false : true);
                    }
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

        private class FrequencyDetector
        {
            private SignChangeDetector _signChangeDetector;

            public FrequencyDetector(SignChangeDetector signChangeDetector)
            {
                if (signChangeDetector == null)
                {
                    throw new ArgumentNullException(nameof(signChangeDetector));
                }

                _signChangeDetector = signChangeDetector;
            }

            public int DetectFrequency(IList<short> samples)
            {
                var frequencies = new List<int>();

                for (var sampleNumber = 0; sampleNumber < samples.Count; sampleNumber++)
                {
                    var currentTimeMilliseconds = sampleNumber * 1.0 / _signChangeDetector.SampleRate * 1000.0;

                    var signChangeResult = _signChangeDetector.DetectSignChange(samples[sampleNumber], currentTimeMilliseconds);

                    if (signChangeResult.SignChanged == true && signChangeResult.TimeDifferenceMilliseconds != null)
                    {
                        var frequency = (int)(1.0 / signChangeResult.TimeDifferenceMilliseconds * 1000.0);
                        frequencies.Add(frequency);
                    }
                }

                return (int)frequencies.Average();
            }
        }

        private class SignChangeDetector
        {
            private int? _lastSign = null;
            private double? _lastSignChangeMilliseconds { get; set; } = null;

            public int SampleRate { get; }

            public SignChangeDetector(int sampleRate)
            {
                SampleRate = sampleRate;
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

        private class SignChangeResult
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
