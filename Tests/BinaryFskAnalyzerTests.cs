using Core.AudioAnalysis;
using Core.AudioGeneration;
using Core.BinaryData;
using Core.BinaryFskAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using Tests.Properties;

namespace Tests
{
    [TestClass]
    public class BinaryFskAnalyzerTests
    {
        private Mock<IAudioAnalyzer> _audioAnalyzer;
        private Mock<IFrequencyDetector> _frequencyDetector;
        private BitManipulator _bitManipulator;
        private BinaryFskAnalyzerSettings _binaryFskAnalyzerSettings;

        [TestInitialize]
        public void TestInitialize()
        {
            _audioAnalyzer = new Mock<IAudioAnalyzer>();
            _frequencyDetector = new Mock<IFrequencyDetector>();
            _bitManipulator = new BitManipulator();

            _binaryFskAnalyzerSettings = new BinaryFskAnalyzerSettings
            {
                BaudRate = 300.0,
                SpaceFrequency = 1000,
                MarkFrequency = 1500
            };
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_NullAudioAnalyzer_Throws()
        {
            new BinaryFskAnalyzer(null, _frequencyDetector.Object, _binaryFskAnalyzerSettings);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_NullFrequencyDetector_Throws()
        {
            new BinaryFskAnalyzer(_audioAnalyzer.Object, null, _binaryFskAnalyzerSettings);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_NullSettings_Throws()
        {
            new BinaryFskAnalyzer(_audioAnalyzer.Object, _frequencyDetector.Object, null);
        }

        [TestMethod]
        public void AnalyzeSignal_KnownSignal_ReturnsExpectedValues()
        {
            var audioStream = new MemoryStream();

            var audioGenerator = new AudioGenerator(audioStream);
            var fskAudioGenerator = new FskAudioGenerator(audioGenerator);
            fskAudioGenerator.GenerateAudio(_binaryFskAnalyzerSettings.BaudRate,
                _binaryFskAnalyzerSettings.SpaceFrequency, _binaryFskAnalyzerSettings.MarkFrequency,
                _bitManipulator.StringToBits(Resources.LoremIpsumTestString));

            var audioAnalyzer = new AudioAnalyzer(audioStream, audioGenerator);
            var binaryFskAnalyzer = (IBinaryFskAnalyzer)new BinaryFskAnalyzer(audioAnalyzer, new ZeroCrossingsFrequencyDetector(), _binaryFskAnalyzerSettings);
            var results = binaryFskAnalyzer.AnalyzeSignal();

            var result = binaryFskAnalyzer.AnalyzeSignal();
            var bits = new List<bool>();
            foreach (var frame in result.AnalysisFrames)
            {
                if (frame.Bit.HasValue == true)
                {
                    bits.Add(frame.Bit.Value);
                }
            }

            var ascii = BitManipulator.BitsToString(bits);

            Assert.AreEqual(Resources.LoremIpsumTestString, ascii);
        }
    }
}
