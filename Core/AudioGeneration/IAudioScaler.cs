namespace Core.AudioGeneration
{
    public interface IAudioScaler
    {
        float[] Scale(float[] samples, int sampleRate, int baudRate, int numberOfSymbols, int scaleWidth, int scaleHeight);
        double SamplesPerSymbol { get; set; }
    }
}
