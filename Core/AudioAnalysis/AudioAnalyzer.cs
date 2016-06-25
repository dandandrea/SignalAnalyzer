using Core.AudioGeneration;
using NAudio.Wave;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace Core.AudioAnalysis
{
    public class AudioAnalyzer : IAudioAnalyzer
    {
        public double FileLengthInMicroseconds { get; }
        public int BoostFrequencyAmount { get; private set; }

        private float[] _samples;
        private int _sampleRate;
        private int _bitsPerSample;
        private long _fileLengthInBytes;
        private IAudioGenerator _audioGenerator;

        public AudioAnalyzer(Stream inputStream, IAudioGenerator audioGenerator, int boostFrequency = 0, bool play = false, int? playTime = null)
        {
            if (inputStream == null)
            {
                throw new ArgumentNullException(nameof(inputStream));
            }

            if (audioGenerator == null)
            {
                throw new ArgumentNullException(nameof(audioGenerator));
            }

            inputStream.Position = 0;

            var reader = new WaveFileReader(inputStream);

            _sampleRate = reader.WaveFormat.SampleRate;
            _bitsPerSample = reader.WaveFormat.BitsPerSample;
            _fileLengthInBytes = reader.Length;
            _audioGenerator = audioGenerator;
            BoostFrequencyAmount = boostFrequency;

            if (play == true)
            {
                Play(inputStream, playTime);
            }

            var buffer = new byte[reader.Length];
            var read = reader.Read(buffer, 0, buffer.Length);
            _samples = new float[(int)Math.Ceiling(read / 4.0)];

            FileLengthInMicroseconds = _samples.Length / (double)_sampleRate * Math.Pow(10, 6);

            Buffer.BlockCopy(buffer, 0, _samples, 0, read);

            if (boostFrequency != 0)
            {
                BoostFrequency(boostFrequency);
            }
        }

        public AudioAnalyzer(string filename, IAudioGenerator audioGenerator, int boostFrequency = 0, bool play = false,
            int? playTime = null)
            : this(new StreamReader(filename).BaseStream, audioGenerator, boostFrequency, play, playTime) { }

        public SamplingResult GetSamples(double? startMicroseconds, double? endMicroseconds)
        {
            if (startMicroseconds == null)
            {
                startMicroseconds = 0;
                endMicroseconds = FileLengthInMicroseconds;
            }

            endMicroseconds = endMicroseconds > FileLengthInMicroseconds ? FileLengthInMicroseconds : endMicroseconds;

            var desiredSampleLengthInMicroseconds = endMicroseconds - startMicroseconds;
            var firstSampleNumber = (int)(startMicroseconds / Math.Pow(10, 6) * _sampleRate);
            var lastSampleNumber = (int)(endMicroseconds / Math.Pow(10, 6) * _sampleRate);
            var desiredNumberOfSamples = lastSampleNumber - firstSampleNumber;

            var desiredSamples = new float[desiredNumberOfSamples];
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
                FileLengthInMicroseconds = FileLengthInMicroseconds,
                Samples = desiredSamples
            };
        }

        public void BoostFrequency(int frequency)
        {
            var boostSamples = _audioGenerator.GenerateSamples(frequency, _samples.Length, _sampleRate);

            for (var i = 0; i < _samples.Length; i++)
            {
                _samples[i] = _samples[i] * boostSamples[i];
            }
        }

        public static void Play(string filename, int lengthInMilliseconds)
        {
            if (string.IsNullOrEmpty(filename))
            {
                throw new ArgumentNullException(nameof(filename), "filename must not be null or empty");
            }

            using (var inputStream = new FileStream(filename, FileMode.Open))
            {
                Play(inputStream, lengthInMilliseconds);
            }
        }

        public static void Play(Stream inputStream, int? playTime)
        {
            if (inputStream == null)
            {
                throw new ArgumentNullException(nameof(inputStream));
            }

            var previousPosition = inputStream.Position;
            inputStream.Position = 0;

            using (var reader = new WaveFileReader(inputStream))
            using (var waveOut = new WaveOut())
            {
                waveOut.Init(reader);
                waveOut.Volume = 1.0F;
                waveOut.Play();
                Thread.Sleep(playTime == null ? 1000 : playTime.Value);
            }

            inputStream.Position = previousPosition;
        }
    }
}
