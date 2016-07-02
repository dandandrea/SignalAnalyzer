using MathNet.Numerics.Interpolation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Core.AudioGeneration
{
    public class AudioScaler : IAudioScaler
    {
        public int SamplesPerSymbol { get; set; }

        public float[] Scale(float[] samples, int sampleRate, int baudRate, int numberOfSymbols, int scaleWidth, int scaleHeight)
        {
            var xCoordinates = Array.ConvertAll(Enumerable.Range(0, samples.Length).ToArray(), Convert.ToDouble);
            var yCoordinates = Array.ConvertAll(samples, Convert.ToDouble);

            var cubicSplineFactor = (int)Math.Ceiling((double)samples.Length / scaleWidth);
            Debug.WriteLine($"[AudioScaler] Cubic spline factor: {cubicSplineFactor}");

            var interpolatedSamples = new List<float>();
            var cubicSpline = CubicSpline.InterpolateNatural(xCoordinates, yCoordinates);
            for (var n = 0; n < scaleWidth * cubicSplineFactor; n++)
            {
                interpolatedSamples.Add((float)cubicSpline.Interpolate((double)n * samples.Length / (cubicSplineFactor * scaleWidth)));
            }

            Debug.WriteLine($"[AudioScaler] Total samples: {samples.Length}, width: {scaleWidth}");

            var downSampledSamples = new List<float>();
            for (var i = 0; i < interpolatedSamples.Count(); i += cubicSplineFactor)
            {
                downSampledSamples.Add(interpolatedSamples[i]);
            }
            samples = downSampledSamples.ToArray();

            // TODO: Optimization: only build the List once
            var minValue = new List<float>(samples).Min();
            var maxValue = new List<float>(samples).Max();

            var verticalScalingFactor = scaleHeight / Math.Abs(maxValue) / 2;

            Debug.WriteLine($"[AudioScaler] Min: {minValue}, max: {maxValue}, height: {scaleHeight}, vertical scaling factor {verticalScalingFactor}");

            var outputSamples = new List<float>();
            foreach (var sample in samples)
            {
                outputSamples.Add((int)Math.Floor((sample + Math.Abs(minValue)) * verticalScalingFactor));
            }

            SamplesPerSymbol = (int)Math.Ceiling((double)scaleWidth / numberOfSymbols);

            Debug.WriteLine($"[AudioScaler] Samples per symbol: {SamplesPerSymbol}");

            return outputSamples.ToArray();
        }
    }
}
