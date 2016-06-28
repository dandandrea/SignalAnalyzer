namespace Gui
{
    public class TestRunnerArguments
    {
        public int SpaceFrequency { get; set; }
        public int MarkFrequency { get; set; }
        public int Tolerance { get; set; }

        public int BaudStart { get; set; }
        public int BaudIncrement { get; set; }
        public int BaudEnd { get; set; }

        public double BoostStart { get; set; }
        public double BoostIncrement { get; set; }
        public double BoostEnd { get; set; }

        public bool WriteFaveFiles { get; set; }
        public bool PlayAudio { get; set; }
        public string TestString { get; set; }
    }
}
