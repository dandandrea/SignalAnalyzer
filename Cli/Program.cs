using System;
using System.Linq;
using Core.AudioAnalysis;
using SignalAnalyzer.Properties;
using Core.BinaryFskAnalysis;
using System.Collections.Generic;
using SignalAnalyzer.Ui;

namespace Cli
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var baudRate = 300.0;
            var spaceFrequency = 1060;
            var markFrequency = 1250;

            var windowPositionStart = 0.0;
            var windowPositionIncrement = 1.0 / baudRate * 1000.0;
            double? windowPositionEnd = null;
            var windowLengthStart = 1.0 / baudRate * 1000.0;
            var windowLengthIncrement = 1.0 / baudRate * 1000.0 / 2.0;
            var windowLengthEnd = windowLengthStart;

            var frequencyDeviationTolerance = 30.0;

            var binaryFskAnalyzer = new BinaryFskAnalyzer(new AudioAnalyzer(Resources.Bell_103_ten_z_chars));

            Console.WriteLine($"Window position start {windowPositionStart:N3}, window position end {windowPositionEnd:N3}, window position increment {windowPositionIncrement:N3}");
            Console.WriteLine($"Window length start {windowLengthStart:N3}, window length end {windowLengthEnd:N3}, window length increment {windowLengthIncrement:N3}");
            Console.WriteLine();

            var bits = binaryFskAnalyzer.AnalyzeSignal(baudRate, spaceFrequency, markFrequency, windowPositionStart, windowPositionIncrement,
                windowPositionEnd, windowLengthStart, windowLengthEnd, windowLengthIncrement, frequencyDeviationTolerance);

            Console.WriteLine("Rendering unformatted");
            Console.WriteLine();
            var renderer = (IRenderer)new UnformattedRenderer();
            renderer.Render(BitManipulator.BitsToBytes(BitManipulator.RemoveBit(bits, 1)));
            Console.WriteLine();

            Console.WriteLine("Rendering rows");
            Console.WriteLine();
            renderer = new RowRenderer();
            renderer.Render(BitManipulator.BitsToBytes(BitManipulator.RemoveBit(bits, 1)));
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
