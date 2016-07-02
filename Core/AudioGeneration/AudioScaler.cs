using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.AudioGeneration
{
    public class AudioScaler : IAudioScaler
    {
        public int SamplesPerSymbol { get; set; }

        public float[] Scale(float[] samples, int sampleRate, int baudRate, int scaleWidth, int scaleHeight)
        {
            var outputSamples = new List<float>();

            var inputSamples = new List<float>(samples);
            var minValue = inputSamples.Min();
            var maxValue = inputSamples.Max();

            var verticalScalingFactor = scaleHeight / Math.Abs(maxValue) / 2;

            Console.WriteLine($"[AudioScaler] Min: {minValue}, max: {maxValue}, height: {scaleHeight}, vertical scaling factor {verticalScalingFactor}");

            var sampleInterval = samples.Length > scaleWidth ? (int)Math.Floor((double)samples.Length / scaleWidth) : 1;

            Console.WriteLine($"[AudioScaler] Total samples: {samples.Length}, width: {scaleWidth}, sample every: {sampleInterval}");

            SamplesPerSymbol = (int)Math.Floor((double)sampleRate / baudRate) / sampleInterval;

            Console.WriteLine($"[AudioScaler] Samples per symbol: {SamplesPerSymbol}");

            var downSampledSamples = new List<float>();
            for (var i = 0; i < samples.Length; i += sampleInterval)
            {
                downSampledSamples.Add(samples[i]);
            }
            samples = downSampledSamples.ToArray();

            foreach (var sample in samples)
            {
                outputSamples.Add((int)Math.Floor((sample + Math.Abs(minValue)) * verticalScalingFactor));
            }

            return outputSamples.ToArray();
        }
    }
}
