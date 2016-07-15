using System;

namespace Gui
{
    public class FileBasedFormInput : FormInput
    {
        public string Filename { get; private set; }

        public FileBasedFormInput(string spaceFrequency, string markFrequency, string tolerance, string baudStart, string baudIncrement,
            string baudEnd, string boostStart, string boostIncrement, string boostEnd, string filename, bool playAudio)
            : base(spaceFrequency, markFrequency, tolerance, baudStart, baudIncrement, baudEnd, boostStart, boostIncrement, boostEnd, playAudio)
        {
            if (string.IsNullOrWhiteSpace(filename))
            {
                throw new ArgumentException("Filename cannot be empty");
            }

            Filename = filename;
        }
    }
}