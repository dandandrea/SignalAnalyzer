using System.Collections.Generic;
using System;
using Core.AudioAnalysis;
using System.Linq;
using Core.BinaryData;
using System.Diagnostics;

namespace Core.BinaryFskAnalysis
{
    public class BinaryFskAnalyzer : BinaryFskAnalyzerEventSource, IBinaryFskAnalyzer
    {
        private IAudioAnalyzer _audioAnalyzer;
        private IFrequencyDetector _frequencyDetector;
        private BinaryFskAnalyzerSettings _settings;

        public BinaryFskAnalyzer(IAudioAnalyzer audioAnalyzer, IFrequencyDetector frequencyDetector,
            BinaryFskAnalyzerSettings binaryFskAnalzyerSettings)
        {
            Initialize(audioAnalyzer, frequencyDetector, binaryFskAnalzyerSettings);
        }

        public void Initialize(IAudioAnalyzer audioAnalyzer, IFrequencyDetector frequencyDetector,
            BinaryFskAnalyzerSettings binaryFskAnalzyerSettings)
        {
            if (audioAnalyzer == null)
            {
                throw new ArgumentNullException(nameof(audioAnalyzer));
            }

            if (frequencyDetector == null)
            {
                throw new ArgumentNullException(nameof(frequencyDetector));
            }

            if (binaryFskAnalzyerSettings == null)
            {
                throw new ArgumentNullException(nameof(binaryFskAnalzyerSettings));
            }

            _audioAnalyzer = audioAnalyzer;
            _frequencyDetector = frequencyDetector;
            _settings = ProcessSettings(binaryFskAnalzyerSettings);
        }

        public ICollection<bool> AnalyzeSignal(string testString = null)
        {
            var bits = new List<bool>();

            var frequencyDifferences = new List<int>();
            int numberOfZeroFrequencies = 0;

            if (_settings.WindowPositionEndMicroseconds == null)
            {
                _settings.WindowPositionEndMicroseconds = _audioAnalyzer.FileLengthInMicroseconds;

                // Debug.WriteLine($"No window end position specified, setting to total length of audio ({_settings.WindowPositionEndMicroseconds} us)");
            }

            var i = 0;
            for (var currentWindowStart = _settings.WindowPositionStartMicroseconds; currentWindowStart <= _settings.WindowPositionEndMicroseconds; currentWindowStart += _settings.WindowPositionIncrementMicroseconds.Value)
            {
                for (var currentWindowLength = _settings.WindowLengthStartMicroseconds; currentWindowLength <= _settings.WindowLengthEndMicroseconds; currentWindowLength += _settings.WindowLengthIncrementMicroseconds)
                {
                    i++;

                    var samplingResult = _audioAnalyzer.GetSamples(currentWindowStart, currentWindowStart + currentWindowLength);

                    var targetNumberOfSamples = _audioAnalyzer.SampleRate / Math.Pow(10, 6) * currentWindowLength;

                    // Debug.WriteLine($"Got {samplingResult.Samples.Count()} samples, want {targetNumberOfSamples:N1} samples");

                    // TODO: How to set this threshold?
                    var sampleThreshold = 0.9;
                    if (samplingResult.Samples.Count() < (targetNumberOfSamples * sampleThreshold))
                    {
                        continue;
                    }

                    var frequency = _frequencyDetector.DetectFrequency(samplingResult.Samples);
                    var frequencyDifference = FrequencyDifference(frequency, _settings.SpaceFrequency, _settings.MarkFrequency);
                    var markOrSpace = MarkOrSpace(frequency, _settings.SpaceFrequency, _settings.MarkFrequency);

                    if (frequency <= 0)
                    {
                        numberOfZeroFrequencies++;
                        continue;
                    }

                    if (frequencyDifference > _settings.FrequencyDeviationTolerance)
                    {
                        // Debug.WriteLine($"WARN: @ {currentWindowStart / Math.Pow(10, 6):N3} seconds (Baud rate {_settings.BaudRate}) [{i}] outside of tolerance (frequency {frequency} Hz, difference {frequencyDifference} Hz, tolerance {_settings.FrequencyDeviationTolerance} Hz)");

                        frequencyDifferences.Add(frequencyDifference);

                        continue;
                    }

                    // Debug.WriteLine($"[{currentWindowStart:N3} us to {currentWindowStart + currentWindowLength:N3} us ({(currentWindowStart + currentWindowLength) - currentWindowStart:N3} us)] {frequency:N0} Hz average (+/- {frequencyDifference:N0} Hz) [Want {_settings.MarkFrequency:N0} Hz / {_settings.SpaceFrequency:N0} Hz] -> bit {bits.Count}: {markOrSpace}");

                    bits.Add(markOrSpace == 0 ? false : true);
                }
            }

            var minimumFrequencyDifference = frequencyDifferences.Count() > 0 ? frequencyDifferences.Min() : 0;
            var maximumFrequencyDifference = frequencyDifferences.Count() > 0 ? frequencyDifferences.Max() : 0;
            var averageFrequencyDifference = frequencyDifferences.Count() > 0 ? frequencyDifferences.Average() : 0;

            bool? match = null;
            string resultingString = null;
            if (testString != null)
            {
                resultingString = BitManipulator.BitsToString(bits);
                match = false;
                if (resultingString == testString)
                {
                    match = true;
                }
            }

            // Debug.WriteLine($"Boost freq.: {_audioAnalyzer.BoostFrequencyAmount} Hz, avg. freq. diff.: {averageFrequencyDifference}, # missed freqs.: {missedFrequencies}");

            AnalysisComplete((int)_settings.BaudRate, _audioAnalyzer.BoostFrequencyAmount, minimumFrequencyDifference,
                maximumFrequencyDifference, averageFrequencyDifference, frequencyDifferences.Count(), numberOfZeroFrequencies,
                resultingString, match);

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

        private BinaryFskAnalyzerSettings ProcessSettings(BinaryFskAnalyzerSettings sourceSettings)
        {
            var updatedSettings = new BinaryFskAnalyzerSettings(sourceSettings);

            var baudRateIncrement = 1.0 / updatedSettings.BaudRate * Math.Pow(10, 6);

            if (updatedSettings.WindowPositionIncrementMicroseconds == null)
            {
                updatedSettings.WindowPositionIncrementMicroseconds = baudRateIncrement;
            }

            if (updatedSettings.WindowLengthStartMicroseconds == null)
            {
                updatedSettings.WindowLengthStartMicroseconds = baudRateIncrement;
            }

            if (updatedSettings.WindowLengthIncrementMicroseconds == null)
            {
                updatedSettings.WindowLengthIncrementMicroseconds = 1.0;
            }

            if (updatedSettings.WindowLengthEndMicroseconds == null)
            {
                updatedSettings.WindowLengthEndMicroseconds = baudRateIncrement;
            }

            return updatedSettings;
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
