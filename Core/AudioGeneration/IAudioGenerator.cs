using System;

namespace Core.AudioGeneration
{
    public interface IAudioGenerator : IDisposable
    {
        void AddInterval(int frequency, double intervalMicroseconds);
        float[] GenerateSamples(int frequency, int sampleCount, int sampleRate, double phase = 0);
    }
}
