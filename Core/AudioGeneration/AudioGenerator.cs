using NAudio.Wave;
using System;
using System.Threading;

namespace Core.AudioGeneration
{
    public class AudioGenerator : IAudioGenerator
    {
        private WaveFileWriter _waveFileWriter;
        private int _sampleRate;

        public AudioGenerator(string filename, int sampleRate = 44100)
        {
            if (string.IsNullOrEmpty(filename))
            {
                throw new ArgumentNullException(nameof(filename), "filename cannot be null or empty");
            }

            _waveFileWriter = new WaveFileWriter(filename, new WaveFormat(sampleRate, 1));
            _sampleRate = sampleRate;
        }

        public void AddInterval(int frequency, double intervalMilliseconds)
        {
            int sampleCount = (int)Math.Ceiling(intervalMilliseconds / 1000.0 * _sampleRate);

            var shortSamples = GetSamples(frequency, sampleCount, _sampleRate);

            _waveFileWriter.WriteData(shortSamples, 0, shortSamples.Length);
        }

        public void Close()
        {
            _waveFileWriter.Close();
        }

        private short[] GetSamples(int frequency, int sampleCount, int sampleRate)
        {
            // TODO: How to determine best value for amplitude?
            var amplitude = 10000.0f;

            var samples = new short[sampleCount];
            for (int i = 0; i < sampleCount; i++)
            {
                samples[i] = (short)(amplitude * Math.Sin((2 * Math.PI * i * frequency) / sampleRate));
            }

            return samples;
        }

        public static void Play(string filename, int lengthInSeconds)
        {
            var reader = new WaveFileReader(filename);
            var waveOut = new WaveOut();
            waveOut.Init(reader);
            waveOut.Play();
            Thread.Sleep(lengthInSeconds);
            waveOut.Stop();
            waveOut.Dispose();
            reader.Close();
            reader.Dispose();
        }
    }
}
