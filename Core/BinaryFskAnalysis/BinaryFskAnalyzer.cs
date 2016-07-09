using System.Collections.Generic;
using System;
using Core.AudioAnalysis;
using System.Linq;
using Core.BinaryData;
using System.Diagnostics;
using Core.Linq;

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

        public AnalysisResult AnalyzeSignal(string testString = null)
        {
            var baudRateIncrementMicroseconds = 1.0 / _settings.BaudRate * Math.Pow(10, 6);
            var windowPositionEndMicroseconds = _audioAnalyzer.FileLengthInMicroseconds;

            var frequencyDifferences = new List<int>();
            int numberOfZeroFrequencies = 0;

            var analysisFrames = new List<AnalysisFrame>();
            var i = 0;
            for (var currentWindowPositionMicroseconds = 0.0; currentWindowPositionMicroseconds < windowPositionEndMicroseconds; currentWindowPositionMicroseconds += baudRateIncrementMicroseconds)
            {
                // Debug.WriteLine($"Current pos: {currentWindowPositionMicroseconds:N1}, increment: {baudRateIncrementMicroseconds:N1}, end: {windowPositionEndMicroseconds :N1}");

                i++;

                var samplingResult = _audioAnalyzer.GetSamples(currentWindowPositionMicroseconds, currentWindowPositionMicroseconds + baudRateIncrementMicroseconds);

                // TODO: How to set this threshold?
                var targetNumberOfSamples = _audioAnalyzer.SampleRate / Math.Pow(10, 6) * baudRateIncrementMicroseconds;
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
                }

                if (frequencyDifference > _settings.FrequencyDeviationTolerance)
                {
                    Debug.WriteLine($"WARN: @ {currentWindowPositionMicroseconds / Math.Pow(10, 6):N3} seconds (Baud rate {_settings.BaudRate}) [{i}] outside of tolerance (frequency {frequency} Hz, difference {frequencyDifference} Hz, tolerance {_settings.FrequencyDeviationTolerance} Hz)");

                    frequencyDifferences.Add(frequencyDifference);
                }

                // Debug.WriteLine($"[{currentWindowStart:N3} us to {currentWindowStart + currentWindowLength:N3} us ({(currentWindowStart + currentWindowLength) - currentWindowStart:N3} us)] {frequency:N0} Hz average (+/- {frequencyDifference:N0} Hz) [Want {_settings.MarkFrequency:N0} Hz / {_settings.SpaceFrequency:N0} Hz] -> bit {bits.Count}: {markOrSpace}");

                analysisFrames.Add(
                    new AnalysisFrame
                    {
                        Bit = (frequency > 0 && frequencyDifference <= _settings.FrequencyDeviationTolerance) ? (markOrSpace == 0 ? false : true) : (bool?)null,
                        Frequency = frequency,
                        DecodeFailure = frequency > _settings.FrequencyDeviationTolerance ? true : false,
                        DifferenceFromExpectedFrequencies = frequencyDifference,
                        TimeOffsetMicroseconds = currentWindowPositionMicroseconds
                    }
                );
            }

            var analysisResult = new AnalysisResult
            {
                AnalysisFrames = analysisFrames,
                NumberOfFrequencyDifferences = frequencyDifferences.Count(),
                NumberOfZeroFrequencies = numberOfZeroFrequencies,
                MinimumFrequencyDifference = frequencyDifferences.Count() > 0 ? frequencyDifferences.Min() : 0,
                MaximumFrequencyDifference = frequencyDifferences.Count() > 0 ? frequencyDifferences.Max() : 0,
                AverageFrequencyDifference = frequencyDifferences.Count() > 0 ? frequencyDifferences.Average() : 0
            };

            bool? match = null;
            string resultingString = null;
            if (testString != null)
            {
                var bits = new List<bool>();
                analysisFrames.Where(x => x.Bit.HasValue == true).Select(x => x).ForEach(x => bits.Add(x.Bit.Value));

                resultingString = BitManipulator.BitsToString(bits);
                match = false;
                if (resultingString == testString)
                {
                    match = true;
                }
            }

            // Debug.WriteLine($"Boost freq.: {_audioAnalyzer.BoostFrequencyAmount} Hz, avg. freq. diff.: {averageFrequencyDifference}, # missed freqs.: {missedFrequencies}");

            AnalysisComplete((int)_settings.BaudRate, _audioAnalyzer.BoostFrequencyAmount, analysisResult, resultingString, match);

            return analysisResult;
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
