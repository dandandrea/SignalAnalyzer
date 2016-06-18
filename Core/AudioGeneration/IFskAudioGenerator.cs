using System.Collections.Generic;

namespace Core.AudioGeneration
{
    public interface IFskAudioGenerator
    {
        void GenerateAudio(double baudRate, int spaceFrequency, int markFrequency, ICollection<bool> bits);
    }
}
