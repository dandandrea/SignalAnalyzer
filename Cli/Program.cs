using System;
using System.Linq;
using System.Collections.Generic;
using Core.AudioAnalysis;
using System.Diagnostics;
using SignalAnalyzer.Properties;
using Core.BinaryFskAnalysis;

namespace Cli
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var baudRate = 520.83;
            var spaceFrequency = 1563;
            var markFrequency = 2083;

            var windowPositionStart = 0.0;
            var windowPositionIncrement = 1.0 / baudRate * 1000.0;
            var windowPositionEnd = 1000.0;
            var windowLengthStart = 1.0 / baudRate * 1000.0;
            var windowLengthIncrement = 1.0 / baudRate * 1000.0 / 2.0;
            var windowLengthEnd = windowLengthStart;

            var frequencyDeviationTolerance = 20.0;

            Console.WriteLine($"Window position start {windowPositionStart:N3}, window position end {windowPositionEnd:N3}, window position increment {windowPositionIncrement:N3}");
            Console.WriteLine($"Window length start {windowLengthStart:N3}, window length end {windowLengthEnd:N3}, window length increment {windowLengthIncrement:N3}");
            Console.WriteLine();

            var binaryFskAnalyzer = new BinaryFskAnalyzer(new AudioAnalyzer(Resources.Emergency_Alert_System_alternative));
            binaryFskAnalyzer.AnalyzeSignal(baudRate, spaceFrequency, markFrequency, windowPositionStart, windowPositionIncrement,
                windowPositionEnd, windowLengthStart, windowLengthEnd, windowLengthIncrement, frequencyDeviationTolerance);

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}
