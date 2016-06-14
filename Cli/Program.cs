using System;
using System.Linq;
using Core.AudioAnalysis;
using SignalAnalyzer.Properties;
using Core.BinaryFskAnalysis;
using System.Collections.Generic;

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

            var bits = binaryFskAnalyzer.AnalyzeSignal(baudRate, spaceFrequency, markFrequency, windowPositionStart, windowPositionIncrement,
                windowPositionEnd, windowLengthStart, windowLengthEnd, windowLengthIncrement, frequencyDeviationTolerance);

            Console.WriteLine("Rendering rows");
            Console.WriteLine();
            RenderRows(BitsToBytes(SwallowFirstBit(bits)));
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Done");
            Console.ReadLine();

            Console.WriteLine("Rendering columns");
            Console.WriteLine();
            RenderColumns(BitsToBytes(SwallowFirstBit(bits)));
            Console.WriteLine();
            Console.WriteLine("Done");

            Console.ReadLine();
        }

        private static void RenderRows(List<List<bool>> bytes, int blocksPerRow = 8)
        {
            foreach (var byteBlock in bytes.Select((value, index) => new { value, index }))
            {
                if ((byteBlock.index) % blocksPerRow == 0)
                {
                    Console.Write($"{byteBlock.index + 1,3}:  ");
                }

                foreach (var bit in byteBlock.value.Select((value, index) => new { value, index }))
                {
                    Console.Write(bit.value == true ? 1 : 0);

                    if ((bit.index + 1) % 4 == 0)
                    {
                        Console.Write(" ");
                    }

                    if ((bit.index + 1) % 8 == 0)
                    {
                        Console.Write(" ");
                    }
                }

                if ((byteBlock.index + 1) % (blocksPerRow) == 0)
                {
                    Console.WriteLine();
                }
            }
        }

        private static void RenderColumns(List<List<bool>> bytes, int rowsBeforePause = 20)
        {
            foreach (var byteBlock in bytes.Select((value, index) => new { value, index }))
            {
                Console.Write($"{byteBlock.index + 1, 3}: ");

                foreach (var bit in byteBlock.value.Select((value, index) => new { value, index }))
                {
                    Console.Write(bit.value == true ? 1 : 0);
                    
                    if (bit.index == 3)
                    {
                        Console.Write(" ");
                    }
                }

                Console.WriteLine();

                if ((byteBlock.index + 1) % rowsBeforePause == 0)
                {
                    Console.ReadLine();
                }
            }
        }

        private static List<List<bool>> BitsToBytes(ICollection<bool> bits)
        {
            var bytes = new List<List<bool>>();

            foreach (var bit in bits.Select((value, index) => new { value, index }))
            {
                if (bit.index == 0 || bit.index % 8 == 0)
                {
                    bytes.Add(new List<bool>());
                }

                bytes.ElementAt(bytes.Count - 1).Add(bit.value);
            }

            return bytes;
        }

        private static ICollection<bool> SwallowFirstBit(ICollection<bool> incomingBits)
        {
            var bits = new List<bool>();

            var n = 0;
            foreach (var bit in incomingBits)
            {
                n++;

                if (n == 1)
                {
                    continue;
                }

                bits.Add(bit);
            }

            return bits;
        }
    }
}
