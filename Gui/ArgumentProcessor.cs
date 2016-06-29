using System;

namespace Gui
{
    public class ArgumentProcessor
    {
        public static TestRunnerArguments ProcessArguments(FormInput formInput)
        {
            if (string.IsNullOrWhiteSpace(formInput.SpaceFrequency))
            {
                throw new ArgumentException("Space frequency cannot be empty");
            }

            if (string.IsNullOrWhiteSpace(formInput.MarkFrequency))
            {
                throw new ArgumentException("Mark frequency cannot be empty");
            }

            if (string.IsNullOrWhiteSpace(formInput.Tolerance))
            {
                throw new ArgumentException("Tolerance cannot be empty");
            }

            if (string.IsNullOrWhiteSpace(formInput.BaudStart))
            {
                throw new ArgumentException("Baud start cannot be empty");
            }

            int spaceFrequencyParsed = int.Parse(formInput.SpaceFrequency);
            int markFrequencyParsed = int.Parse(formInput.MarkFrequency);
            int toleranceParsed = int.Parse(formInput.Tolerance);

            int baudStartParsed = int.Parse(formInput.BaudStart);
            int? baudIncrementParsed = null;
            int? baudEndParsed = null;

            double? boostStartParsed = null;
            double? boostIncrementParsed = null;
            double? boostEndParsed = null;

            bool writeWavFiles = formInput.WriteWavFiles;
            bool playAudio = formInput.PlayAudio;
            string testString = formInput.TestString;

            if (!string.IsNullOrEmpty(formInput.BaudIncrement))
            {
                baudIncrementParsed = int.Parse(formInput.BaudIncrement);
            }

            if (!string.IsNullOrEmpty(formInput.BaudEnd))
            {
                baudEndParsed = int.Parse(formInput.BaudEnd);
            }

            if (!string.IsNullOrEmpty(formInput.BoostStart))
            {
                boostStartParsed = int.Parse(formInput.BoostStart);
            }

            if (!string.IsNullOrEmpty(formInput.BoostIncrement))
            {
                boostIncrementParsed = int.Parse(formInput.BoostIncrement);
            }

            if (!string.IsNullOrEmpty(formInput.BoostEnd))
            {
                boostEndParsed = int.Parse(formInput.BoostEnd);
            }

            if ((baudIncrementParsed != null || baudEndParsed != null) && (baudIncrementParsed == null || baudEndParsed == null))
            {
                throw new ArgumentException("You must specify both baud increment and baud end if either are specified");
            }

            if ((boostStartParsed != null || boostIncrementParsed != null || boostEndParsed != null) &&
                (boostStartParsed == null || boostIncrementParsed == null || boostEndParsed == null))
            {
                throw new ArgumentException(
                    "You must specify boost start, boost increment, and boost end if any boost parameters are specified"
                );
            }

            if (string.IsNullOrEmpty(testString))
            {
                throw new ArgumentException(
                    "You must specify a test string"
                );
            }

            baudIncrementParsed = baudIncrementParsed != null ? baudIncrementParsed : 1;
            baudEndParsed = baudEndParsed != null ? baudEndParsed : baudStartParsed;

            boostStartParsed = boostStartParsed != null ? boostStartParsed : 0;
            boostIncrementParsed = boostIncrementParsed != null ? boostIncrementParsed : 1;
            boostEndParsed = boostEndParsed != null ? boostEndParsed : boostStartParsed;

            return new TestRunnerArguments
            {
                SpaceFrequency = spaceFrequencyParsed,
                MarkFrequency = markFrequencyParsed,
                Tolerance = toleranceParsed,
                BaudStart = baudStartParsed,
                BaudIncrement = baudIncrementParsed.Value,
                BaudEnd = baudEndParsed.Value,
                BoostStart = boostStartParsed.Value,
                BoostIncrement = boostIncrementParsed.Value,
                BoostEnd = boostEndParsed.Value,
                WriteFaveFiles = writeWavFiles,
                PlayAudio = playAudio,
                TestString = testString
            };
        }
    }
}
