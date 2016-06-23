using System;
using System.Collections.Generic;

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

        public void GenerateAudio(double baudRate, int spaceFrequency, int markFrequency, ICollection<bool> bits)
        {
            double intervalLengthInMilliseconds = 1000.0 / baudRate;

            foreach (var bit in bits)
            {
                int frequency = spaceFrequency;
                if (bit == true)
                {
                    frequency = markFrequency;
                }

                _audioGenerator.AddInterval(frequency, intervalLengthInMilliseconds);
            }
        }
    }
}
