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

        public AudioAnalyzer(Stream inputStream, bool play = false, int? playTime = null)
        {
            if (inputStream == null)
            {
                throw new ArgumentNullException(nameof(inputStream));
            }

            inputStream.Position = 0;

            var reader = new WaveFileReader(inputStream);

            _sampleRate = reader.WaveFormat.SampleRate;
            _bitsPerSample = reader.WaveFormat.BitsPerSample;
            _fileLengthInBytes = reader.Length;

            if (play == true)
            {
                Play(inputStream, playTime);
            }

            var buffer = new byte[reader.Length];
            var read = reader.Read(buffer, 0, buffer.Length);
            _samples = new short[(int)Math.Ceiling(read / 2.0)];

            FileLengthInMilliseconds = _samples.Length / (double)_sampleRate * 1000.0;

            Debug.WriteLine($"_samples length {_samples.Length}, reader length {reader.Length}, _samples length * 2 = {_samples.Length * 2.0:N2}");

            Buffer.BlockCopy(buffer, 0, _samples, 0, read);
        }

        public AudioAnalyzer(string filename, bool play = false, int? playTime = null)
            : this(new StreamReader(filename).BaseStream, play, playTime) { }

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

        public static void Play(string filename, int lengthInSeconds)
        {
            if (string.IsNullOrEmpty(filename))
            {
                throw new ArgumentNullException(nameof(filename), "filename must not be null or empty");
            }

            using (var inputStream = new FileStream(filename, FileMode.Open))
            {
                Play(inputStream, lengthInSeconds);
            }
        }

        public static void Play(Stream inputStream, int? playTime)
        {
            if (inputStream == null)
            {
                throw new ArgumentNullException(nameof(inputStream));
            }

            inputStream.Seek(0, SeekOrigin.Begin);

            using (var reader = new WaveFileReader(inputStream))
            using (var waveOut = new WaveOut())
            {
                waveOut.Init(reader);
                waveOut.Play();
                Thread.Sleep(playTime == null ? 1000 : playTime.Value);
                inputStream.Seek(0, SeekOrigin.Begin);
            }
        }
    }
}
