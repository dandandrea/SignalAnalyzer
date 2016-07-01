using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.AudioGeneration
{
    public class FskAudioGenerator : IFskAudioGenerator
    {
        private IAudioGenerator _audioGenerator;

        public FskAudioGenerator(IAudioGenerator audioGenerator)
        {
            if (audioGenerator == null)
            {
                throw new ArgumentNullException(nameof(audioGenerator));
            }

            _audioGenerator = audioGenerator;
        }

        public float[] GenerateAudio(double baudRate, int spaceFrequency, int markFrequency, ICollection<bool> bits)
        {
            var samples = new List<float[]>();

            double intervalLengthInMicroseconds = Math.Pow(10, 6) / baudRate;

            foreach (var bit in bits)
            {
                int frequency = spaceFrequency;
                if (bit == true)
                {
                    frequency = markFrequency;
                }

                samples.Add(_audioGenerator.AddInterval(frequency, intervalLengthInMicroseconds));
            }

            return samples.SelectMany(floats => floats).ToArray();
        }
    }
}
