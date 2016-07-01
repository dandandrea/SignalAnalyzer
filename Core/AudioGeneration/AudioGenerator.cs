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
        private double _phase = 0;

        // TODO: How to determine best value for amplitude?
        private float _amplitude = 1;

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

        public float[] AddInterval(int frequency, double intervalMicroseconds)
        {
            int sampleCount = (int)Math.Ceiling(intervalMicroseconds / Math.Pow(10, 6) * _sampleRate);

            _phase += _previousFrequency.HasValue ? (_amplitude * 2 * Math.PI * sampleCount * _previousFrequency.Value / _sampleRate) : 0;

            var samples = GenerateSamples(frequency, sampleCount, _sampleRate, _phase);

            _waveFileWriter.WriteSamples(samples, 0, samples.Length);

            _waveFileWriter.Flush();

            // Debug.WriteLine($"Previous frequency: {_previousFrequency}, phase {phase}");

            _previousFrequency = frequency;

            return samples;
        }

        public float[] GenerateSamples(int frequency, int sampleCount, int sampleRate, double phase = 0)
        {
            var samples = new float[sampleCount];
            for (int i = 0; i < sampleCount; i++)
            {
                samples[i] = (float)(_amplitude * Math.Sin((2 * Math.PI * i * frequency) / sampleRate + phase));
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
