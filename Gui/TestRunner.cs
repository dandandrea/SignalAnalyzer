using Core.AudioAnalysis;
using Core.AudioGeneration;
using Core.BinaryData;
using Core.BinaryFskAnalysis;
using System;
using System.IO;
using System.Linq;

namespace Gui
{
    public class TestRunner
    {
        public IBinaryFskAnalyzer FskAnalyzer { get; private set; }
        private Stream _audioStream;
        private IAudioGenerator _audioGenerator;
        private BinaryFskAnalyzerSettings _binaryFskAnalyzerSettings;
        private IFskAudioGenerator _fskAudioGenerator;
        private IAudioAnalyzer _audioAnalyzer;
        private IFrequencyDetector _frequencyDetector;

        public TestRunner(BinaryFskAnalyzerSettings binaryFskAnalyzerSettings = null,
            IFrequencyDetector frequencyDetector = null)
        {
            _audioStream = new MemoryStream();
            _audioGenerator = new AudioGenerator(_audioStream);
            _fskAudioGenerator = new FskAudioGenerator(_audioGenerator);
            _binaryFskAnalyzerSettings = binaryFskAnalyzerSettings != null ? binaryFskAnalyzerSettings : new Bell103BinaryFskAnalyzerSettings();
            var bitManipulator = new BitManipulator();
            _fskAudioGenerator.GenerateAudio(_binaryFskAnalyzerSettings.BaudRate, _binaryFskAnalyzerSettings.SpaceFrequency,
                _binaryFskAnalyzerSettings.MarkFrequency, bitManipulator.StringToBits("Test string"));
            _audioAnalyzer = new AudioAnalyzer(_audioStream, _audioGenerator);
            _frequencyDetector = frequencyDetector != null ? frequencyDetector : new ZeroCrossingsFrequencyDetector();
            FskAnalyzer = new BinaryFskAnalyzer(_audioAnalyzer, _frequencyDetector, _binaryFskAnalyzerSettings);
        }

        public AnalysisResult Run(SignalGenerationBasedFormInput input)
        {
            var fileTimestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");

            var bitManipulator = new BitManipulator();
            var bits = bitManipulator.StringToBits(input.TestString);

            _binaryFskAnalyzerSettings = new Bell103BinaryFskAnalyzerSettings
            {
                SpaceFrequency = input.SpaceFrequency,
                MarkFrequency = input.MarkFrequency,
                BaudRate = input.BaudStart,
                FrequencyDeviationTolerance = input.Tolerance
            };

            _audioStream = new MemoryStream();

            _audioGenerator = new AudioGenerator(_audioStream);
            _fskAudioGenerator = new FskAudioGenerator(_audioGenerator);
            _fskAudioGenerator.GenerateAudio(_binaryFskAnalyzerSettings.BaudRate,
                _binaryFskAnalyzerSettings.SpaceFrequency, _binaryFskAnalyzerSettings.MarkFrequency, bits);

            var audioLengthInMicroseconds = (int)(bits.Count * Math.Pow(10, 6) / _binaryFskAnalyzerSettings.BaudRate);

            _audioStream = new MemoryStream();
            _audioGenerator = new AudioGenerator(_audioStream);
            _fskAudioGenerator = new FskAudioGenerator(_audioGenerator);
            var samples = _fskAudioGenerator.GenerateAudio(_binaryFskAnalyzerSettings.BaudRate,
                _binaryFskAnalyzerSettings.SpaceFrequency, _binaryFskAnalyzerSettings.MarkFrequency, bits);

            // SignalGenerationComplete(bits.Count, audioLengthInMicroseconds, samples, _audioAnalyzer.SampleRate);

            if (input.WriteWavFiles == true)
            {
                using (var file = File.Create($"{fileTimestamp}_{input.BaudStart}_baud.wav"))
                {
                    var previousPosition = _audioStream.Position;
                    _audioStream.Position = 0;
                    _audioStream.CopyTo(file);
                    _audioStream.Position = previousPosition;
                }
            }

            _binaryFskAnalyzerSettings = new Bell103BinaryFskAnalyzerSettings
            {
                SpaceFrequency = input.SpaceFrequency,
                MarkFrequency = input.MarkFrequency,
                BaudRate = input.BaudStart,
                FrequencyDeviationTolerance = input.Tolerance
            };

            _audioAnalyzer = new AudioAnalyzer(_audioStream, _audioGenerator);

            FskAnalyzer.Initialize(_audioAnalyzer, new ZeroCrossingsFrequencyDetector(), _binaryFskAnalyzerSettings);
            var analysisResult = FskAnalyzer.AnalyzeSignal(input.TestString);

            if (input.PlayAudio == true)
            {
                AudioAnalyzer.Play(_audioStream, (int)Math.Ceiling((audioLengthInMicroseconds / Math.Pow(10, 3))));
            }

            return analysisResult;
        }

        public AnalysisResult Run(FileBasedFormInput input)
        {
            AnalysisResult analysisResult = null;

            using (_audioStream = File.Open(input.Filename, FileMode.Open, FileAccess.Read))
            {
                _audioStream.Position = 0;

                _binaryFskAnalyzerSettings = new Bell103BinaryFskAnalyzerSettings
                {
                    SpaceFrequency = input.SpaceFrequency,
                    MarkFrequency = input.MarkFrequency,
                    BaudRate = input.BaudStart,
                    FrequencyDeviationTolerance = input.Tolerance
                };

                _audioAnalyzer = new AudioAnalyzer(_audioStream, _audioGenerator);

                FskAnalyzer.Initialize(_audioAnalyzer, new ZeroCrossingsFrequencyDetector(), _binaryFskAnalyzerSettings);
                analysisResult = FskAnalyzer.AnalyzeSignal();

                var numberOfBits = analysisResult.AnalysisFrames.Where(x => x.Bit.HasValue).Count();

                // SignalGenerationComplete(numberOfBits, (int)(_audioAnalyzer.FileLengthInMicroseconds / Math.Pow(10, 3)), _audioAnalyzer.GetSamples().Samples.ToArray(), _audioAnalyzer.SampleRate);

                var signalGenerationInformation = new SignalGenerationInformation
                {
                    NumberOfBits = numberOfBits,
                    AudioLengthInMicroseconds = (int)(_audioAnalyzer.FileLengthInMicroseconds / Math.Pow(10, 3)),
                    Samples = _audioAnalyzer.GetSamples().Samples,
                    SampleRate = _audioAnalyzer.SampleRate
                };

                analysisResult.SignalGenerationInformation = signalGenerationInformation;

                if (input.PlayAudio == true)
                {
                    AudioAnalyzer.Play(_audioStream, (int)Math.Ceiling((_audioAnalyzer.FileLengthInMicroseconds / Math.Pow(10, 3))));
                }
            }

            return analysisResult;
        }

        /*
        private void SignalGenerationComplete(int numberOfBits, int audioLengthInMicroseconds, float[] samples, int sampleRate)
        {
            var e = new SignalGenerationResultEventArgs
            {
                NumberOfBits = numberOfBits,
                AudioLengthInMicroseconds = audioLengthInMicroseconds,
                Samples = samples,
                SampleRate = sampleRate
            };

            SignalGenerationCompleted?.Invoke(this, e);
        }
        */
    }

    /*
    public class SignalGenerationResultEventArgs : EventArgs
    {
        public int NumberOfBits { get; set; }
        public int AudioLengthInMicroseconds { get; set; }
        public float[] Samples { get; set; }
        public int SampleRate { get; set; }
    }
    */
}
