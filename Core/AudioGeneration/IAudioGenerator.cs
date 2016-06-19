namespace Core.AudioGeneration
{
    public interface IAudioGenerator
    {
        void AddInterval(int frequency, double intervalMilliseconds);
    }
}
