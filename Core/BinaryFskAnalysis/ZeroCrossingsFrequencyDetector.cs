using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.BinaryFskAnalysis
{
    public class ZeroCrossingsFrequencyDetector : IFrequencyDetector
    {
        private SignChangeDetector _signChangeDetector;

        public ZeroCrossingsFrequencyDetector(int sampleRate = 44100)
        {
            _signChangeDetector = new SignChangeDetector(sampleRate);
        }

        public int DetectFrequency(IList<short> samples)
        {
            var frequencies = new List<int>();

            for (var sampleNumber = 0; sampleNumber < samples.Count; sampleNumber++)
            {
                var currentTimeMilliseconds = sampleNumber * 1.0 / _signChangeDetector.SampleRate * 1000.0;

                var signChangeResult = _signChangeDetector.DetectSignChange(samples[sampleNumber], currentTimeMilliseconds);

                if (signChangeResult.SignChanged == true && signChangeResult.TimeDifferenceMilliseconds != null)
                {
                    var frequency = (int)(1.0 / signChangeResult.TimeDifferenceMilliseconds * 1000.0);
                    frequencies.Add(frequency);
                }
            }

            _signChangeDetector.Reset();

            return frequencies.Count > 0 ? (int)frequencies.Average() : 0;
        }

        private class SignChangeDetector
        {
            private int? _lastSign = null;
            private double? _lastSignChangeMilliseconds { get; set; } = null;

            public int SampleRate { get; }

            public SignChangeDetector(int sampleRate)
            {
                SampleRate = sampleRate;
            }

            public SignChangeResult DetectSignChange(short sample, double currentTimeMilliseconds)
            {
                bool signChanged = false;

                if (_lastSign != null && _lastSign == 1 && sample < 0)
                {
                    signChanged = true;
                }

                var signChangeResult = new SignChangeResult
                {
                    SignChanged = signChanged,
                    TimeDifferenceMilliseconds = _lastSignChangeMilliseconds != null ? currentTimeMilliseconds - _lastSignChangeMilliseconds : null
                };

                if (signChanged == true)
                {
                    _lastSignChangeMilliseconds = currentTimeMilliseconds;
                }

                _lastSign = sample >= 0 ? 1 : -1;

                return signChangeResult;
            }

            public void Reset()
            {
                _lastSign = null;
                _lastSignChangeMilliseconds = null;
            }
        }

        private class SignChangeResult
        {
            public bool SignChanged { get; set; } = false;
            public double? TimeDifferenceMilliseconds { get; set; }
        }
    }
}
