namespace Gui
{
    public class FormInput
    {
        public string SpaceFrequency { get; set; }
        public string MarkFrequency { get; set; }
        public string Tolerance { get; set; }

        public string BaudStart { get; set; }
        public string BaudIncrement { get; set; }
        public string BaudEnd { get; set; }

        public string BoostStart { get; set; }
        public string BoostIncrement { get; set; }
        public string BoostEnd { get; set; }

        public bool WriteWavFiles { get; set; }
        public bool PlayAudio { get; set; }
        public string TestString { get; set; }
    }
}
