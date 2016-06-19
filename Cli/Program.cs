using System;
using Core.AudioAnalysis;
using SignalAnalyzer.Properties;
using Core.BinaryFskAnalysis;
using SignalAnalyzer.Ui;
using Core.AudioGeneration;
using Core.BinaryData;
using System.IO;

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

            var audioLengthInMillisecondsSeconds = (int)(myBits.Count * 1000.0 / binaryFskAnalyzerSettings.BaudRate);
            Console.WriteLine($"Length of audio in seconds: {audioLengthInMillisecondsSeconds / 1000.0:N1}");
            Console.WriteLine();

            Console.WriteLine(Resources.BigLebowskiQuote);
            Console.WriteLine();

            AudioAnalyzer.Play(audioStream, audioLengthInMillisecondsSeconds);

            // TODO: Add BitManipulator method to inject start and stop bits
            // var numberOfStartBits = 1;
            // var numberOfStopBits = 1;

            var binaryFskAnalyzer = new BinaryFskAnalyzer(new AudioAnalyzer(audioStream), new ZeroCrossingsFrequencyDetector(), binaryFskAnalyzerSettings);

            Console.WriteLine($"Window position start {binaryFskAnalyzerSettings.WindowPositionStartMilliseconds:N3} ms, window position end {binaryFskAnalyzerSettings.WindowPositionEndMilliseconds:N3} ms, window position increment {binaryFskAnalyzerSettings.WindowPositionIncrementMilliseconds:N3} ms");
            Console.WriteLine($"Window length start {binaryFskAnalyzerSettings.WindowLengthStartMilliseconds:N3} ms, window length end {binaryFskAnalyzerSettings.WindowLengthEndMilliseconds:N3} ms, window length increment {binaryFskAnalyzerSettings.WindowLengthIncrementMilliseconds:N3} ms");
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
