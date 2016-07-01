using System.Collections.Generic;

namespace Core.AudioGeneration
{
    public interface IFskAudioGenerator
    {
        float[] GenerateAudio(double baudRate, int spaceFrequency, int markFrequency, ICollection<bool> bits);
    }
}
