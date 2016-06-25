using System.Collections.Generic;

namespace Core.AudioAnalysis
{
    public class SamplingResult
    {
        public IList<float> Samples { get; set; }
        public int SampleRate { get; set; }
        public int BitsPerSample { get; set; }
        public double FileLengthInMicroseconds { get; set; }
        public int FileLengthInBytes { get; set; }
    }
}
