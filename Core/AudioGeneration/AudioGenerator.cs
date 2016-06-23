using NAudio.Wave;
using System;
using System.Diagnostics;
using System.IO;

namespace Core.AudioGeneration
{
    public class AudioGenerator : IAudioGenerator, IDisposable
    {
        private WaveFileWriter _waveFileWriter;
        private int _sampleRate;

        public AudioGenerator(Stream outputStream, int sampleRate = 88200)
        {
            if (outputStream == null)
            {
                throw new ArgumentNullException(nameof(outputStream));
            }

            _waveFileWriter = new WaveFileWriter(outputStream, WaveFormat.CreateIeeeFloatWaveFormat(sampleRate, 1));
            _sampleRate = sampleRate;
        }

        public AudioGenerator(string filename, int sampleRate = 88200)
            : this(new StreamWriter(filename).BaseStream, sampleRate) {}

        public void AddInterval(int frequency, double intervalMilliseconds)
        {
            int sampleCount = (int)Math.Ceiling(intervalMilliseconds / 1000.0 * _sampleRate);

            var samples = GenerateSamples(frequency, sampleCount, _sampleRate);

            _waveFileWriter.WriteSamples(samples, 0, samples.Length);

            _waveFileWriter.Flush();
        }

        public float[] GenerateSamples(int frequency, int sampleCount, int sampleRate)
        {
            // TODO: How to determine best value for amplitude?
            var amplitude = 100.0f;

            var samples = new float[sampleCount];
            for (int i = 0; i < sampleCount; i++)
            {
                samples[i] = (float)(amplitude * Math.Sin((2 * Math.PI * i * frequency) / sampleRate));
            }

            return samples;
        }

        public void Dispose()
        {
            _waveFileWriter.Dispose();
        }
    }
}
