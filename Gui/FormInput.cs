using System;

namespace Gui
{
    public class FormInput
    {
        public int SpaceFrequency { get; private set; }
        public int MarkFrequency { get; private set; }
        public int Tolerance { get; private set; }

        public int BaudStart { get; private set; }
        public int BaudIncrement { get; private set; }
        public int BaudEnd { get; private set; }

        public double? BoostStart { get; private set; }
        public double? BoostIncrement { get; private set; }
        public double? BoostEnd { get; private set; }

        public bool PlayAudio { get; private set; }

        public FormInput(string spaceFrequency, string markFrequency, string tolerance, string baudStart, string baudIncrement,
            string baudEnd, string boostStart, string boostIncrement, string boostEnd, bool playAudio)
        {
            if (string.IsNullOrWhiteSpace(spaceFrequency))
            {
                throw new ArgumentException("Space frequency cannot be empty");
            }

            if (string.IsNullOrWhiteSpace(markFrequency))
            {
                throw new ArgumentException("Mark frequency cannot be empty");
            }

            if (string.IsNullOrWhiteSpace(tolerance))
            {
                throw new ArgumentException("Tolerance cannot be empty");
            }

            if (string.IsNullOrWhiteSpace(baudStart))
            {
                throw new ArgumentException("Baud start cannot be empty");
            }

            SpaceFrequency = int.Parse(spaceFrequency);
            MarkFrequency = int.Parse(markFrequency);
            Tolerance = int.Parse(tolerance);

            BaudStart = int.Parse(baudStart);

            if (!string.IsNullOrEmpty(baudIncrement))
            {
                BaudIncrement = int.Parse(baudIncrement);
            }

            if (!string.IsNullOrEmpty(baudEnd))
            {
                BaudEnd = int.Parse(baudEnd);
            }

            if (!string.IsNullOrEmpty(boostStart))
            {
                BoostStart = int.Parse(boostStart);
            }

            if (!string.IsNullOrEmpty(boostIncrement))
            {
                BoostIncrement = int.Parse(boostIncrement);
            }

            if (!string.IsNullOrEmpty(boostEnd))
            {
                BoostEnd = int.Parse(boostEnd);
            }

            if ((BaudIncrement != 0 || BaudEnd != 0) && (BaudIncrement == 0 || BaudEnd == 0))
            {
                throw new ArgumentException("You must specify both baud increment and baud end if either are specified");
            }

            if ((BoostStart != null || BoostIncrement != null || BoostEnd != null) &&
                (BoostStart == null || BoostIncrement == null || BoostEnd == null))
            {
                throw new ArgumentException(
                    "You must specify boost start, boost increment, and boost end if any boost parameters are specified"
                );
            }

            BaudIncrement = BaudIncrement != 0 ? BaudIncrement : 1;
            BaudEnd = BaudEnd != 0 ? BaudEnd : BaudStart;

            BoostStart = BoostStart != null ? BoostStart : 0;
            BoostIncrement = BoostIncrement != null ? BoostIncrement : 1;
            BoostEnd = BoostEnd != null ? BoostEnd : BoostStart;

            PlayAudio = playAudio;
        }
    }
}
