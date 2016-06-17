using NAudio.Wave;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace Core.AudioAnalysis
{
    public class AudioAnalyzer : IAudioAnalyzer
    {
        public double FileLengthInMilliseconds { get; }

        private short[] _samples;
        private int _sampleRate;
        private int _bitsPerSample;
        private long _fileLengthInBytes;

        public AudioAnalyzer(string filename, bool play = false, int? playTime = null)
            : this(new StreamReader(filename).BaseStream, play, playTime) {}

        public AudioAnalyzer(Stream fileInputStream, bool play = false, int? playTime = null)
        {
            var reader = new WaveFileReader(fileInputStream);

            _sampleRate = reader.WaveFormat.SampleRate;
            _bitsPerSample = reader.WaveFormat.BitsPerSample;
            _fileLengthInBytes = reader.Length;

            if (play == true)
            {
                Play(fileInputStream, playTime);
            }

            var buffer = new byte[reader.Length];
            var read = reader.Read(buffer, 0, buffer.Length);
            _samples = new short[(int)Math.Ceiling(read / 2.0)];

            FileLengthInMilliseconds = _samples.Length / (double)_sampleRate * 1000.0;

            Debug.WriteLine($"_samples length {_samples.Length}, reader length {reader.Length}, _samples length * 2 = {_samples.Length * 2.0:N2}");

            Buffer.BlockCopy(buffer, 0, _samples, 0, read);
        }

        public SamplingResult GetSamples(double? startMilliseconds, double? endMilliseconds)
        {
            if (startMilliseconds == null)
            {
                startMilliseconds = 0;
                endMilliseconds = FileLengthInMilliseconds;
            }

            endMilliseconds = endMilliseconds > FileLengthInMilliseconds ? FileLengthInMilliseconds : endMilliseconds;

            var desiredSampleLengthInMilliseconds = endMilliseconds - startMilliseconds;
            var firstSampleNumber = (int)(startMilliseconds / 1000.0 * _sampleRate);
            var lastSampleNumber = (int)(endMilliseconds / 1000.0 * _sampleRate);
            var desiredNumberOfSamples = lastSampleNumber - firstSampleNumber;

            var desiredSamples = new short[desiredNumberOfSamples];
            var j = 0;
            for (var i = firstSampleNumber; i < lastSampleNumber; i++)
            {
                desiredSamples[j] = _samples[i];
                j++;
            }

            return new SamplingResult
            {
                BitsPerSample = _bitsPerSample,
                SampleRate = _sampleRate,
                FileLengthInBytes = (int)_fileLengthInBytes,
                FileLengthInMilliseconds = FileLengthInMilliseconds,
                Samples = desiredSamples
            };
        }

        private static void Play(Stream fileInputStream, int? playTime)
        {
            fileInputStream.Seek(0, SeekOrigin.Begin);
            var reader = new WaveFileReader(fileInputStream);
            var waveOut = new WaveOut();
            waveOut.Init(reader);
            waveOut.Play();
            Thread.Sleep(playTime == null ? 1000 : playTime.Value);
            waveOut.Stop();
            waveOut.Dispose();
            reader.Close();
            reader.Dispose();
            fileInputStream.Seek(0, SeekOrigin.Begin);
        }
    }
}
