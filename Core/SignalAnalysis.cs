using System.Collections.Generic;

namespace Core
{
    public class SignalAnalysis
    {
        public int SampleRate { get; set; }
        public int BitsPerSample { get; set; }
        public int SampleLengthInMilliseconds { get; set; }
        public int SampleLengthInBytes { get; set; }
        public ICollection<FrequencyComponent> FrequencyComponents = new List<FrequencyComponent>();
    }
}
