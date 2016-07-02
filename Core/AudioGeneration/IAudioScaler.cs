namespace Core.AudioGeneration
{
    public interface IAudioScaler
    {
        float[] Scale(float[] samples, int sampleRate, int baudRate, int scaleWidth, int scaleHeight);
        int SamplesPerSymbol { get; set; }
    }
}
