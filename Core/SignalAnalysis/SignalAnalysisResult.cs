using Core.AudioAnalysis;
using System.Collections.Generic;
using System.Text;

namespace Core.SignalAnalysis
{
    public class SignalAnalysisResult
    {
        public SamplingResult SamplingResult { get; set; }
        public int NumberOfBins { get; set; }
        public int BinSizeInHertz { get; set; }
        public ICollection<FrequencyComponent> FrequencyComponents = new List<FrequencyComponent>();

        public override string ToString()
        {
            var fileLengthInBytes = SamplingResult != null ? SamplingResult.FileLengthInBytes : 0;
            var fileLengthInMicroseconds = SamplingResult != null ? SamplingResult.FileLengthInMicroseconds : 0;
            var sampleRate = SamplingResult != null ? SamplingResult.SampleRate : 0;
            var bitsPerSample = SamplingResult != null ? SamplingResult.BitsPerSample : 0;

            var sb = new StringBuilder();
            sb.AppendLine($"File length in bytes: {fileLengthInBytes:N0} bytes");
            sb.AppendLine($"File length in microseconds: {fileLengthInMicroseconds:N0} us");
            sb.AppendLine($"Sample rate: {sampleRate:N0} Hz");
            sb.AppendLine($"Bits per sample: {bitsPerSample:N0} bits/sample");
            sb.AppendLine($"# bins: {NumberOfBins:N0}");
            sb.AppendLine($"Bin size in Hertz: {BinSizeInHertz:N0} Hertz");
            return sb.ToString();
        }
    }
}
