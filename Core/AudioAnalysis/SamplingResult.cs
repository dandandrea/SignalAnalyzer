using System.Collections.Generic;

namespace Core.AudioAnalysis
{
    public class SamplingResult
    {
        public IList<short> Samples { get; set; }
        public int SampleRate { get; set; }
        public int BitsPerSample { get; set; }
        public double FileLengthInMilliseconds { get; set; }
        public int FileLengthInBytes { get; set; }
    }
}
