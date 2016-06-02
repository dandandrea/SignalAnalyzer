using NAudio.Dsp;
using NAudio.Wave;
using System;
using System.Collections.Generic;

namespace Core
{
    public class SignalAnalyzer
    {
        public static ICollection<FrequencyComponent> GetFrequencyComponents(string filename)
        {
            // Open the file
            var reader = new WaveFileReader(filename);

            // Wave file details
            var numChannels = reader.WaveFormat.Channels;
            var sampleRate = reader.WaveFormat.SampleRate;
            var bitDepth = reader.WaveFormat.BitsPerSample;

            // Play the file
            // var waveOut = new WaveOut();
            // waveOut.Init(waveFileReader);
            // waveOut.Play();

            // Read the data into an array
            byte[] dataAllChannels = new byte[reader.Length];
            reader.Read(dataAllChannels, 0, dataAllChannels.Length);

            // Get the first channel
            // TODO: Need to take into account bitDepth
            // TODO: As bitDepth increasing, need to read in more successive bytes per channel
            /*
            byte[] dataFirstChannel = new byte[dataAllChannels.Length / numChannels];
            var j = 0;
            for (var i = 0; i < dataAllChannels.Length; i = i + numChannels)
            {
                dataFirstChannel[j] = dataAllChannels[i];
                j++;
            }
            */

            // Perform the sampling
            // You don't have to fill the FFT buffer to get valid results.
            // More noisy & smaller "magnitudes", but better freq. res.
            var fftSize = 8192;
            var sampledData = new NAudio.Dsp.Complex[fftSize];
            int bytesPerChannelSample = bitDepth / 8;
            var byteOffset = (int)Math.Floor((double)dataAllChannels.Length / bytesPerChannelSample / fftSize);
            for (var i = 0; i < fftSize; i++)
            {
                var samplePosition = i * byteOffset;
                sampledData[i] = new NAudio.Dsp.Complex();
                sampledData[i].X = BitConverter.ToInt16(dataAllChannels, samplePosition);
            }

            Console.WriteLine($"{numChannels} channel(s), {sampleRate} sample rate, {bitDepth} bits per sample");
            Console.WriteLine($"Overall data length {dataAllChannels.Length}, 1st ch. data length {dataAllChannels.Length}");
            Console.WriteLine($"# samples (FFT size) {fftSize}, channel data byte offset {byteOffset}");
            Console.WriteLine("Performing FFT");
            FastFourierTransform.FFT(true, (int)Math.Log(fftSize, 2), sampledData);

            var freqs = new List<FrequencyComponent>();
            for (int i = 0; i < sampledData.Length / 2; i++)
            {
                if (sampledData[i].Y == 0)
                {
                    continue;
                }

                var magnitude = Math.Sqrt(sampledData[i].X * sampledData[i].X + sampledData[i].Y * sampledData[i].Y);

                // *** I assume we don't care about phase???
                var freq = i * sampleRate / fftSize;
                freqs.Add(new FrequencyComponent() { Frequency = freq, Magnitude = magnitude });
            }

            freqs.Sort((b, a) => a.Magnitude.CompareTo(b.Magnitude));

            var j = 0;
            foreach (var freq in freqs)
            {
                Console.WriteLine($"{freq.Frequency} Hz ({freq.Magnitude})");

                if (j > 0 && j % 10 == 0)
                {
                    break;
                    Console.WriteLine("Hit enter for more values");
                    Console.ReadLine();
                }

                j++;
            }

            Console.WriteLine("Done");
            Console.ReadLine();

            return freqs;
        }
    }
}
