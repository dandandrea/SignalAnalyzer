namespace Core.AudioGeneration
{
    public interface IAudioGenerator
    {
        void AddInterval(int frequency, double intervalMilliseconds);
        float[] GenerateSamples(int frequency, int sampleCount, int sampleRate);
    }
}
