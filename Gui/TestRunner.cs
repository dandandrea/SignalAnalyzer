using Core.AudioAnalysis;
using Core.AudioGeneration;
using Core.BinaryData;
using Core.BinaryFskAnalysis;
using System;
using System.Diagnostics;
using System.IO;

namespace Gui
{
    public class TestRunner
    {
        public delegate void SignalGenerationCompleteEventHandler(object sender, SignalGenerationResultEventArgs e);
        public event SignalGenerationCompleteEventHandler SignalGenerationCompleted;

        public IBinaryFskAnalyzer FskAnalyzer { get; private set; }
        private MemoryStream _audioStream;
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

        public void Run(TestRunnerArguments arguments)
        {
            var fileTimestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");

            var bitManipulator = new BitManipulator();
            var bits = bitManipulator.StringToBits(arguments.TestString);

            for (var baudRate = arguments.BaudStart; baudRate <= arguments.BaudEnd; baudRate += arguments.BaudIncrement)
            {
                for (var loopBoostFrequency = arguments.BoostStart; loopBoostFrequency <= arguments.BoostEnd;
                    loopBoostFrequency += arguments.BoostIncrement)
                {
                    _binaryFskAnalyzerSettings = new Bell103BinaryFskAnalyzerSettings
                    {
                        SpaceFrequency = arguments.SpaceFrequency,
                        MarkFrequency = arguments.MarkFrequency,
                        BaudRate = baudRate,
                        FrequencyDeviationTolerance = arguments.Tolerance
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

                    SignalGenerationComplete(bits.Count, audioLengthInMicroseconds, samples);

                    if (arguments.WriteFaveFiles == true)
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
                        SpaceFrequency = (int)(arguments.SpaceFrequency + loopBoostFrequency),
                        MarkFrequency = (int)(arguments.MarkFrequency + loopBoostFrequency),
                        BaudRate = baudRate,
                        FrequencyDeviationTolerance = arguments.Tolerance
                    };

                    _audioAnalyzer = new AudioAnalyzer(_audioStream, _audioGenerator, (int)loopBoostFrequency);

                    FskAnalyzer.Initialize(_audioAnalyzer, new ZeroCrossingsFrequencyDetector(), _binaryFskAnalyzerSettings);
                    FskAnalyzer.AnalyzeSignal(arguments.TestString);

                    if (arguments.PlayAudio == true)
                    {
                        AudioAnalyzer.Play(_audioStream, (int)Math.Ceiling((audioLengthInMicroseconds / Math.Pow(10, 3))));
                    }
                }
            }
        }

        private void SignalGenerationComplete(int numberOfBits, int audioLengthInMicroseconds, float[] samples)
        {
            var e = new SignalGenerationResultEventArgs
            {
                NumberOfBits = numberOfBits,
                AudioLengthInMicroseconds = audioLengthInMicroseconds,
                Samples = samples
            };

            SignalGenerationCompleted?.Invoke(this, e);
        }
    }

    public class SignalGenerationResultEventArgs : EventArgs
    {
        public int NumberOfBits { get; set; }
        public int AudioLengthInMicroseconds { get; set; }
        public float[] Samples { get; set; }
    }
}
