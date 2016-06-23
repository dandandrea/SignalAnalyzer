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
        public IBinaryFskAnalyzer FskAnalyzer { get; private set; }
        private MemoryStream _audioStream;
        private IAudioGenerator _audioGenerator;
        private BinaryFskAnalyzerSettings _binaryFskAnalyzerSettings;
        private IFskAudioGenerator _fskAudioGenerator;
        private IAudioAnalyzer _audioAnalyzer;
        private IFrequencyDetector _frequencyDetector;
        private string _testString = "ABCDEFGHIJK";

        public TestRunner(BinaryFskAnalyzerSettings binaryFskAnalyzerSettings = null,
            IFrequencyDetector frequencyDetector = null)
        {
            _audioStream = new MemoryStream();
            _audioGenerator = new AudioGenerator(_audioStream);
            _fskAudioGenerator = new FskAudioGenerator(_audioGenerator);
            _binaryFskAnalyzerSettings = binaryFskAnalyzerSettings != null ? binaryFskAnalyzerSettings : new Bell103BinaryFskAnalyzerSettings();
            var bitManipulator = new BitManipulator();
            _fskAudioGenerator.GenerateAudio(_binaryFskAnalyzerSettings.BaudRate, _binaryFskAnalyzerSettings.SpaceFrequency,
                _binaryFskAnalyzerSettings.MarkFrequency, bitManipulator.StringToBits(_testString));
            _audioAnalyzer = new AudioAnalyzer(_audioStream, _audioGenerator);
            _frequencyDetector = frequencyDetector != null ? frequencyDetector : new ZeroCrossingsFrequencyDetector();
            FskAnalyzer = new BinaryFskAnalyzer(_audioAnalyzer, _frequencyDetector, _binaryFskAnalyzerSettings);
        }

        public void Run(double spaceFrequency, double markFrequency, int baudRate, double boostStartFrequency,
            double boostIncrement, double boostEndFrequency)
        {
            var bitManipulator = new BitManipulator();
            var bits = bitManipulator.StringToBits(_testString);
            Debug.WriteLine($"Length of string in bits: {bits.Count}");

            _binaryFskAnalyzerSettings = new Bell103BinaryFskAnalyzerSettings
            {
                SpaceFrequency = (int)spaceFrequency,
                MarkFrequency = (int)markFrequency,
                BaudRate = baudRate
            };

            _audioStream = new MemoryStream();

            _audioGenerator = new AudioGenerator(_audioStream);
            _fskAudioGenerator = new FskAudioGenerator(_audioGenerator);
            _fskAudioGenerator.GenerateAudio(_binaryFskAnalyzerSettings.BaudRate,
                _binaryFskAnalyzerSettings.SpaceFrequency, _binaryFskAnalyzerSettings.MarkFrequency, bits);

            var audioLengthInMillisecondsSeconds = (int)(bits.Count * 1000.0 / _binaryFskAnalyzerSettings.BaudRate);
            Debug.WriteLine($"Length of audio in seconds: {audioLengthInMillisecondsSeconds / 1000.0:N1}");
            Debug.WriteLine(string.Empty);

            for (var loopBoostFrequency = boostStartFrequency; loopBoostFrequency <= boostEndFrequency;
                loopBoostFrequency += boostIncrement)
            {
                _binaryFskAnalyzerSettings = new Bell103BinaryFskAnalyzerSettings
                {
                    SpaceFrequency = (int)spaceFrequency,
                    MarkFrequency = (int)markFrequency,
                    BaudRate = baudRate
                };

                _audioStream = new MemoryStream();
                _audioGenerator = new AudioGenerator(_audioStream);
                _fskAudioGenerator = new FskAudioGenerator(_audioGenerator);
                _fskAudioGenerator.GenerateAudio(_binaryFskAnalyzerSettings.BaudRate,
                    _binaryFskAnalyzerSettings.SpaceFrequency, _binaryFskAnalyzerSettings.MarkFrequency, bits);

                _binaryFskAnalyzerSettings = new Bell103BinaryFskAnalyzerSettings
                {
                    SpaceFrequency = (int)(spaceFrequency + loopBoostFrequency),
                    MarkFrequency = (int)(markFrequency + loopBoostFrequency),
                    BaudRate = baudRate,
                    FrequencyDeviationTolerance = 100
                };

                _audioAnalyzer = new AudioAnalyzer(_audioStream, _audioGenerator, (int)loopBoostFrequency);

                FskAnalyzer.Initialize(_audioAnalyzer, new ZeroCrossingsFrequencyDetector(), _binaryFskAnalyzerSettings);
                FskAnalyzer.AnalyzeSignal(_testString);
            }
        }
    }
}
