using System;

namespace Core.AudioGeneration
{
    public class SamplingResultEventArgs : EventArgs
    {
        public float[] Samples { get; set; }
    }
}
