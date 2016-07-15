using System;

namespace Gui
{
    public class SignalGenerationBasedFormInput : FormInput
    {
        public bool WriteWavFiles { get; private set; }
        public string TestString { get; private set; }

        public SignalGenerationBasedFormInput(string spaceFrequency, string markFrequency, string tolerance, string baudStart, string baudIncrement,
            string baudEnd, string boostStart, string boostIncrement, string boostEnd, bool writeWavFiles, bool playAudio, string testString)
            : base(spaceFrequency, markFrequency, tolerance, baudStart, baudIncrement, baudEnd, boostStart, boostIncrement, boostEnd, playAudio)
        {
            WriteWavFiles = writeWavFiles;

            if (string.IsNullOrWhiteSpace(testString))
            {
                throw new ArgumentException("Test string cannot be empty");
            }

            TestString = testString;
        }
    }
}
