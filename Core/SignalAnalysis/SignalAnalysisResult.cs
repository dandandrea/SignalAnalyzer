using System.Collections.Generic;
using System.Text;

namespace Core.SignalAnalysis
{
    public class SignalAnalysisResult
    {
        public int SampleRate { get; set; }
        public int BitsPerSample { get; set; }
        public int NumberOfBins { get; set; }
        public int BinSizeInHertz { get; set; }
        public int FileLengthInMilliseconds { get; set; }
        public int FileLengthInBytes { get; set; }
        public ICollection<FrequencyComponent> FrequencyComponents = new List<FrequencyComponent>();

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"File length in bytes: {FileLengthInBytes:N0} bytes");
            sb.AppendLine($"File length in milliseconds: {FileLengthInMilliseconds:N0} ms");
            sb.AppendLine($"Sample rate: {SampleRate:N0} Hz");
            sb.AppendLine($"Bits per sample: {BitsPerSample:N0} bits/sample");
            sb.AppendLine($"# bins: {NumberOfBins:N0}");
            sb.AppendLine($"Bin size in Hertz: {BinSizeInHertz:N0} Hertz");
            return sb.ToString();
        }
    }
}
