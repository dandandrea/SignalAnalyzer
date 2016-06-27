using NAudio.Wave;
using System;
using System.IO;

namespace Core.AudioGeneration
{
    public class AudioGenerator : IAudioGenerator
    {
        private WaveFileWriter _waveFileWriter;
        private int _sampleRate;
        private int? _previousFrequency;

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

        public void AddInterval(int frequency, double intervalMicroseconds)
        {
            int sampleCount = (int)Math.Ceiling(intervalMicroseconds / Math.Pow(10, 6) * _sampleRate);

            var phase = _previousFrequency.HasValue ? (2 * Math.PI * (sampleCount - 1) * _previousFrequency.Value / _sampleRate) : 0;

            var samples = GenerateSamples(frequency, sampleCount, _sampleRate, phase);

            _waveFileWriter.WriteSamples(samples, 0, samples.Length);

            _waveFileWriter.Flush();

            // Debug.WriteLine($"Previous frequency: {_previousFrequency}, phase {phase}");

            _previousFrequency = frequency;
        }

        public float[] GenerateSamples(int frequency, int sampleCount, int sampleRate, double phase = 0)
        {
            // TODO: How to determine best value for amplitude?
            var amplitude = 1.0f;

            var samples = new float[sampleCount];
            for (int i = 0; i < sampleCount; i++)
            {
                samples[i] = (float)(amplitude * Math.Sin((2 * Math.PI * i * frequency) / sampleRate + phase));
            }

            return samples;
        }

        public void Dispose()
        {
            _waveFileWriter.Close();
            _waveFileWriter.Dispose();
        }
    }
}
