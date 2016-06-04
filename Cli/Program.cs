using Core;
using System;
using System.Collections.Generic;
using System.IO;

namespace Cli
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var filenames = new List<string>
            {
                @"c:\Users\laptop\Desktop\SDR\Signal analysis\flex_long.wav",
                @"c:\Users\laptop\Desktop\SDR\Signal analysis\flex_long_payload.wav",
                @"c:\Users\laptop\Desktop\SDR\Signal analysis\SDRSharp_20160529_172631Z_929612500Hz_AF_short.wav",
                @"c:\Users\laptop\Desktop\SDR\Signal analysis\2600.wav",
                @"c:\Users\laptop\Desktop\SDR\Signal analysis\1450.wav",
                @"c:\Users\laptop\Desktop\SDR\Signal analysis\pocsag_1.wav"
            };

            foreach (var filename in filenames)
            {
                var signalAnalysis = SignalAnalyzer.AnalyzeSignal(filename);

                Console.WriteLine("------------------------------------------------------------");
                Console.WriteLine($"Filename: {Path.GetFileName(filename)}");
                Console.WriteLine($"Sample rate: {signalAnalysis.SampleRate:N0} Hz");
                Console.WriteLine($"Sample length in bytes: {signalAnalysis.SampleLengthInBytes:N0} bytes");
                Console.WriteLine($"Sample length in milliseconds: {signalAnalysis.SampleLengthInMilliseconds:N0} ms");
                Console.WriteLine($"Bits per sample: {signalAnalysis.BitsPerSample:N0} bits/sample");

                Console.WriteLine();
                Console.WriteLine("Predominant frequencies");
                Console.WriteLine("-----------------------");
                var i = 0;
                foreach (var frequencyComponent in signalAnalysis.FrequencyComponents)
                {
                    Console.WriteLine($"{frequencyComponent.Frequency:N0} Hz ({frequencyComponent.Magnitude:N2} magnitude)");

                    if (i > 0 && i % 15 == 0)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Press enter to continue");
                        Console.ReadLine();
                        break;
                    }

                    i++;
                }
            }
        }
    }
}
