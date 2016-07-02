using MathNet.Numerics.Interpolation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Core.AudioGeneration
{
    public class AudioScaler : IAudioScaler
    {
        public double SamplesPerSymbol { get; set; }

        public float[] Scale(float[] samples, int sampleRate, int baudRate, int numberOfSymbols, int scaleWidth, int scaleHeight)
        {
            var cubicSplineFactor = GetCubicSplineFactor(samples, scaleWidth);

            Debug.WriteLine($"[AudioScaler] Total samples: {samples.Length}, width: {scaleWidth}, cubic spline factor {cubicSplineFactor}");

            samples = Interpolate(samples, scaleWidth, cubicSplineFactor);
            samples = DownSample(samples, cubicSplineFactor);

            // TODO: Optimization: only build the List once
            var minValue = new List<float>(samples).Min();
            var maxValue = new List<float>(samples).Max();

            var verticalScalingFactor = scaleHeight / Math.Abs(maxValue) / 2;

            Debug.WriteLine($"[AudioScaler] Min: {minValue}, max: {maxValue}, height: {scaleHeight}, vertical scaling factor {verticalScalingFactor}");

            var outputSamples = new List<float>();
            foreach (var sample in samples)
            {
                outputSamples.Add(ApplyVerticalScaling(sample, minValue, verticalScalingFactor));
            }

            // TODO: This calculation is still probably slightly off
            SamplesPerSymbol = (double)scaleWidth / numberOfSymbols;

            Debug.WriteLine($"[AudioScaler] Samples per symbol: {scaleWidth} / {numberOfSymbols} = {SamplesPerSymbol:N3}");

            return outputSamples.ToArray();
        }

        private static float[] Interpolate(float[] samples, int newSize, int cubicSplineFactor)
        {
            var xCoordinates = Array.ConvertAll(Enumerable.Range(0, samples.Length).ToArray(), Convert.ToDouble);
            var yCoordinates = Array.ConvertAll(samples, Convert.ToDouble);

            var interpolatedSamples = new List<float>();
            var cubicSpline = CubicSpline.InterpolateNatural(xCoordinates, yCoordinates);
            for (var n = 0; n < newSize * cubicSplineFactor; n++)
            {
                interpolatedSamples.Add((float)cubicSpline.Interpolate((double)n * samples.Length / (cubicSplineFactor * newSize)));
            }

            return interpolatedSamples.ToArray();
        }

        private static float[] DownSample(float[] samples, int cubicSplineFactor)
        {
            var downSampledSamples = new List<float>();
            for (var i = 0; i < samples.Count(); i += cubicSplineFactor)
            {
                downSampledSamples.Add(samples[i]);
            }
            return downSampledSamples.ToArray();
        }

        private static int GetCubicSplineFactor(float[] samples, int newSize) => (int)Math.Ceiling((double)samples.Length / newSize);

        private static int ApplyVerticalScaling(float sample, float minValue, float verticalScalingFactor)
            => (int)Math.Floor((sample + Math.Abs(minValue)) * verticalScalingFactor);
    }
}
