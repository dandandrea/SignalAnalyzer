using Core.AudioAnalysis;
using Core.AudioGeneration;
using Core.BinaryData;
using Core.BinaryFskAnalysis;
using System;
using System.IO;

namespace Gui
{
    public class TestRunner
    {
        public delegate void SignalGenerationCompleteEventHandler(object sender, SignalGenerationResultEventArgs e);
        public event SignalGenerationCompleteEventHandler SignalGenerationCompleted;

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

        public void Run(SignalGenerationBasedFormInput input)
        {
            var fileTimestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");

            var bitManipulator = new BitManipulator();
            var bits = bitManipulator.StringToBits(input.TestString);

            for (var baudRate = input.BaudStart; baudRate <= input.BaudEnd; baudRate += input.BaudIncrement)
            {
                for (var loopBoostFrequency = input.BoostStart; loopBoostFrequency <= input.BoostEnd;
                    loopBoostFrequency += input.BoostIncrement)
                {
                    _binaryFskAnalyzerSettings = new Bell103BinaryFskAnalyzerSettings
                    {
                        SpaceFrequency = input.SpaceFrequency,
                        MarkFrequency = input.MarkFrequency,
                        BaudRate = baudRate,
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

                    SignalGenerationComplete(bits.Count, audioLengthInMicroseconds, samples, _audioAnalyzer.SampleRate);

                    if (input.WriteWavFiles == true)
                    {
                        using (var file = File.Create($"{fileTimestamp}_{baudRate}_baud.wav"))
                        {
                            var previousPosition = _audioStream.Position;
                            _audioStream.Position = 0;
                            _audioStream.CopyTo(file);
                            _audioStream.Position = previousPosition;
                        }
                    }

                    _binaryFskAnalyzerSettings = new Bell103BinaryFskAnalyzerSettings
                    {
                        SpaceFrequency = (int)(input.SpaceFrequency + loopBoostFrequency),
                        MarkFrequency = (int)(input.MarkFrequency + loopBoostFrequency),
                        BaudRate = baudRate,
                        FrequencyDeviationTolerance = input.Tolerance
                    };

                    _audioAnalyzer = new AudioAnalyzer(_audioStream, _audioGenerator, (int)loopBoostFrequency);

                    FskAnalyzer.Initialize(_audioAnalyzer, new ZeroCrossingsFrequencyDetector(), _binaryFskAnalyzerSettings);
                    FskAnalyzer.AnalyzeSignal(input.TestString);

                    if (input.PlayAudio == true)
                    {
                        AudioAnalyzer.Play(_audioStream, (int)Math.Ceiling((audioLengthInMicroseconds / Math.Pow(10, 3))));
                    }
                }
            }
        }

        public void Run(FileBasedFormInput input)
        {
            using (_audioStream = File.Open(input.Filename, FileMode.Open, FileAccess.Read))
            {
                for (var baudRate = input.BaudStart; baudRate <= input.BaudEnd; baudRate += input.BaudIncrement)
                {
                    for (var loopBoostFrequency = input.BoostStart; loopBoostFrequency <= input.BoostEnd;
                        loopBoostFrequency += input.BoostIncrement)
                    {
                        _audioStream.Position = 0;

                        _binaryFskAnalyzerSettings = new Bell103BinaryFskAnalyzerSettings
                        {
                            SpaceFrequency = (int)(input.SpaceFrequency + loopBoostFrequency),
                            MarkFrequency = (int)(input.MarkFrequency + loopBoostFrequency),
                            BaudRate = baudRate,
                            FrequencyDeviationTolerance = input.Tolerance
                        };

                        _audioAnalyzer = new AudioAnalyzer(_audioStream, _audioGenerator, (int)loopBoostFrequency);

                        FskAnalyzer.Initialize(_audioAnalyzer, new ZeroCrossingsFrequencyDetector(), _binaryFskAnalyzerSettings);
                        FskAnalyzer.AnalyzeSignal();

                        if (input.PlayAudio == true)
                        {
                            AudioAnalyzer.Play(_audioStream, (int)Math.Ceiling((_audioAnalyzer.FileLengthInMicroseconds / Math.Pow(10, 3))));
                        }
                    }
                }
            }
        }

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
    }

    public class SignalGenerationResultEventArgs : EventArgs
    {
        public int NumberOfBits { get; set; }
        public int AudioLengthInMicroseconds { get; set; }
        public float[] Samples { get; set; }
        public int SampleRate { get; set; }
    }
}
