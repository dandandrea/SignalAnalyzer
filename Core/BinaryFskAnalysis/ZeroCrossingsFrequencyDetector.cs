using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.BinaryFskAnalysis
{
    public class ZeroCrossingsFrequencyDetector : IFrequencyDetector
    {
        private SignChangeDetector _signChangeDetector;

        public ZeroCrossingsFrequencyDetector(int sampleRate = 88200)
        {
            _signChangeDetector = new SignChangeDetector(sampleRate);
        }

        public int DetectFrequency(IList<float> samples)
        {
            var frequencies = new List<int>();

            for (var sampleNumber = 0; sampleNumber < samples.Count; sampleNumber++)
            {
                var currentTimeMicroseconds = sampleNumber * 1.0 / _signChangeDetector.SampleRate * Math.Pow(10, 6);

                var signChangeResult = _signChangeDetector.DetectSignChange(samples[sampleNumber], currentTimeMicroseconds);

                if (signChangeResult.SignChanged == true && signChangeResult.TimeDifferenceMicroseconds != null)
                {
                    var frequency = (int)(1.0 / signChangeResult.TimeDifferenceMicroseconds * Math.Pow(10, 6) / 2.0);
                    frequencies.Add(frequency);
                }
            }

            _signChangeDetector.Reset();

            return frequencies.Count > 0 ? (int)frequencies.Average() : 0;
        }

        private class SignChangeDetector
        {
            private int? _lastSign = null;
            private double? _lastSignChangeMicroseconds { get; set; } = null;

            public int SampleRate { get; }

            public SignChangeDetector(int sampleRate)
            {
                SampleRate = sampleRate;
            }

            public SignChangeResult DetectSignChange(float sample, double currentTimeMicroseconds)
            {
                bool signChanged = false;

                if (_lastSign != null && ((_lastSign == 1 && sample < 0) || (_lastSign == 0 && sample > 0)))
                {
                    signChanged = true;
                }

                var signChangeResult = new SignChangeResult
                {
                    SignChanged = signChanged,
                    TimeDifferenceMicroseconds = _lastSignChangeMicroseconds != null ? currentTimeMicroseconds - _lastSignChangeMicroseconds : null
                };

                if (signChanged == true)
                {
                    _lastSignChangeMicroseconds = currentTimeMicroseconds;
                }

                if (sample > 0)
                {
                    _lastSign = 1;
                }
                else if (sample < 0)
                {
                    _lastSign = 0;
                }

                return signChangeResult;
            }

            public void Reset()
            {
                _lastSign = null;
                _lastSignChangeMicroseconds = null;
            }
        }

        private class SignChangeResult
        {
            public bool SignChanged { get; set; } = false;
            public double? TimeDifferenceMicroseconds { get; set; }
        }
    }
}
