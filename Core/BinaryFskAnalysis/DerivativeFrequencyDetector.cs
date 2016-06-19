using Accord.Math.Differentiation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Core.BinaryFskAnalysis
{
    public class DerivativeFrequencyDetector : IFrequencyDetector
    {
        public int DetectFrequency(IList<short> samples)
        {
            /*
            def freq_from_der(sig, srate):
            """Estimate frequency using by computing derivative
                sig(2*pi*f *t + phi)' = 2 * pi * f * derivative(2*pi*f * sample / srate + phi)

                f = (sig/derivative) *  srate / (2 * pi)
            """
            sig = np.array(sig,dtype=np.float64)
            der = np.gradient(sig) # computer derivative of signal
            rms_def = np.sqrt(np.mean(der**2)) # compute root mean squared of derivative
            rms_sig = np.sqrt(np.mean(sig * sig)) # compute root mean squared of signal
            freq = (srate / ( 2 * np.pi)) * (rms_def / rms_sig)
            return freq
            */

            if (samples.Count() < 2)
            {
                return 0;
            }

            var gradient = new List<double>();
            for (var i = 0; i < samples.Count(); i++)
            {
                if (i == 0) {
                    gradient.Add(samples[i + 1] - samples[i]);
                }
                else if (i == samples.Count() - 1)
                {
                    gradient.Add(samples[i] - samples[i - 1]);
                }
	            else
                {
                    gradient.Add((samples[i + 1] - samples[i - 1]) / 2);
                }
            }

            var gradientRms = Math.Sqrt(gradient.Select(x => Math.Pow(x, 2)).Average());
            var samplesRms = Math.Sqrt(samples.Select(x => Math.Pow(x, 2)).Average());

            var frequency = (44100.0 / (2 * Math.PI)) * (gradientRms / samplesRms);

            return (int)frequency;
        }
    }
}
