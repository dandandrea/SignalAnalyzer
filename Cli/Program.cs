using System;
using System.Linq;
using Core.AudioAnalysis;
using SignalAnalyzer.Properties;
using Core.BinaryFskAnalysis;
using System.Collections.Generic;
using SignalAnalyzer.Ui;
using Core.AudioGeneration;

namespace Cli
{
    public class Program
    {
        private static readonly int _spaceFrequency300Baud = 1060;
        private static readonly int _markFrequency300Baud = 1250;

        public static void Main(string[] args)
        {
            var baudRate = 300.0;
            var spaceFrequency = _spaceFrequency300Baud;
            var markFrequency = _markFrequency300Baud;

            string filename = "some.wav";

            var bitManipulator = new BitManipulator();
            var myBits = bitManipulator.StringToBits(Resources.BigLebowskiQuote);
            Console.WriteLine($"Length of string in bits: {myBits.Count}");
            Console.WriteLine();

            // TODO: Work with audio in streams, not just files
            var audioGenerator = new AudioGenerator(filename);
            var fskAudioGenerator = new FskAudioGenerator(audioGenerator);
            fskAudioGenerator.GenerateAudio(baudRate, spaceFrequency, markFrequency, myBits);

            var audioLengthInMillisecondsSeconds = (int)(myBits.Count * 1000.0 / baudRate);
            Console.WriteLine($"Length of audio in seconds: {audioLengthInMillisecondsSeconds / 1000.0:N1}");
            Console.WriteLine();
            Console.WriteLine("Hit enter to continue");
            Console.ReadLine();

            Console.WriteLine(Resources.BigLebowskiQuote);
            Console.WriteLine();

            AudioGenerator.Play(filename, audioLengthInMillisecondsSeconds);

            var windowPositionStart = 0.0;
            var windowPositionIncrement = 1.0 / baudRate * 1000.0;
            double? windowPositionEnd = null;
            var windowLengthStart = 1.0 / baudRate * 1000.0;
            var windowLengthIncrement = 1.0 / baudRate * 1000.0 / 2.0;
            var windowLengthEnd = windowLengthStart;

            var frequencyDeviationTolerance = 30.0;

            // TODO: Add BitManipulator method to inject start and stop bits
            // var numberOfStartBits = 1;
            // var numberOfStopBits = 1;

            var binaryFskAnalyzer = new BinaryFskAnalyzer(new AudioAnalyzer(filename));

            Console.WriteLine($"Window position start {windowPositionStart:N3}, window position end {windowPositionEnd:N3}, window position increment {windowPositionIncrement:N3}");
            Console.WriteLine($"Window length start {windowLengthStart:N3}, window length end {windowLengthEnd:N3}, window length increment {windowLengthIncrement:N3}");
            Console.WriteLine();

            var bits = binaryFskAnalyzer.AnalyzeSignal(baudRate, spaceFrequency, markFrequency, windowPositionStart, windowPositionIncrement,
                windowPositionEnd, windowLengthStart, windowLengthEnd, windowLengthIncrement, frequencyDeviationTolerance);

            var renderer = (IRenderer)new UnformattedRenderer();
            renderer.Render(BitManipulator.BitsToBytes(bits));
            Console.WriteLine();

            renderer = new UnformattedRenderer();
            renderer.Render(BitManipulator.BitsToBytes(bits));
            Console.WriteLine();

            renderer = new UnformattedRenderer();
            renderer.Render(BitManipulator.BitsToBytes(bits));
            Console.WriteLine();

            Console.WriteLine("Rendering unformatted");
            Console.WriteLine();
            renderer = (IRenderer)new UnformattedRenderer();
            renderer.Render(BitManipulator.BitsToBytes(bits));
            Console.WriteLine();

            Console.WriteLine("Rendering rows");
            Console.WriteLine();
            renderer = new RowRenderer();
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
