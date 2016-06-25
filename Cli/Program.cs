using System;
using Core.AudioAnalysis;
using SignalAnalyzer.Properties;
using Core.BinaryFskAnalysis;
using SignalAnalyzer.Ui;
using Core.AudioGeneration;
using Core.BinaryData;
using System.IO;
using System.Diagnostics;

namespace Cli
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var bitManipulator = new BitManipulator();
            var myBits = bitManipulator.StringToBits(Resources.BigLebowskiQuote);
            Console.WriteLine($"Length of string in bits: {myBits.Count}");

            var audioStream = new MemoryStream();

            var binaryFskAnalyzerSettings = new Bell103BinaryFskAnalyzerSettings();

            var audioGenerator = new AudioGenerator(audioStream);
            var fskAudioGenerator = new FskAudioGenerator(audioGenerator);
            fskAudioGenerator.GenerateAudio(binaryFskAnalyzerSettings.BaudRate,
                binaryFskAnalyzerSettings.SpaceFrequency, binaryFskAnalyzerSettings.MarkFrequency, myBits);

            var audioLengthInMicroseconds = (int)(myBits.Count * Math.Pow(10, 6) / binaryFskAnalyzerSettings.BaudRate);
            Console.WriteLine($"Length of audio in seconds: {audioLengthInMicroseconds / Math.Pow(10, 6):N1}");
            Console.WriteLine();

            Console.WriteLine(Resources.BigLebowskiQuote);
            Console.WriteLine();

            AudioAnalyzer.Play(audioStream, audioLengthInMicroseconds / 1000);

            audioGenerator = new AudioGenerator(audioStream);
            fskAudioGenerator = new FskAudioGenerator(audioGenerator);
            fskAudioGenerator.GenerateAudio(binaryFskAnalyzerSettings.BaudRate,
                binaryFskAnalyzerSettings.SpaceFrequency, binaryFskAnalyzerSettings.MarkFrequency, myBits);

            var binaryFskAnalyzer = new BinaryFskAnalyzer(new AudioAnalyzer(audioStream, audioGenerator), new ZeroCrossingsFrequencyDetector(), binaryFskAnalyzerSettings);

            Console.WriteLine($"Window position start {binaryFskAnalyzerSettings.WindowPositionStartMicroseconds:N3} us, window position end {binaryFskAnalyzerSettings.WindowPositionEndMicroseconds:N3} us, window position increment {binaryFskAnalyzerSettings.WindowPositionIncrementMicroseconds:N3} us");
            Console.WriteLine($"Window length start {binaryFskAnalyzerSettings.WindowLengthStartMicroseconds:N3} us, window length end {binaryFskAnalyzerSettings.WindowLengthEndMicroseconds:N3} us, window length increment {binaryFskAnalyzerSettings.WindowLengthIncrementMicroseconds:N3} us");
            Console.WriteLine();

            var bits = binaryFskAnalyzer.AnalyzeSignal();

            Console.WriteLine("Rendering bytes");
            Console.WriteLine();
            var renderer = (IRenderer)new RowRenderer();
            renderer.Render(BitManipulator.BitsToBytes(bits));
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("Rendering ASCII");
            Console.WriteLine();
            renderer = new AsciiRenderer();
            renderer.Render(BitManipulator.BitsToBytes(bits));
            Console.WriteLine();

            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}
