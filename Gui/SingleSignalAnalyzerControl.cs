using Core.BinaryFskAnalysis;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Gui
{
    public partial class SingleSignalAnalyzerControl : UserControl
    {
        private AnalysisResultEventArgs _analysisResult;
        private SignalGenerationResultEventArgs _signalGenerationResult;
        private Color _defaultMatchLabelForeColor;
        private Color _defaultMatchLabelBackColor;
        private string _defaultStartButtonText;

        public SingleSignalAnalyzerControl()
        {
            InitializeComponent();

            matchLabel.Text = string.Empty;

            SetBelowDataGridToolTipText();

            _defaultMatchLabelForeColor = matchLabel.ForeColor;
            _defaultMatchLabelBackColor = matchLabel.BackColor;
            _defaultStartButtonText = startButton.Text;

            backgroundWorker1.ProgressChanged += BackgroundWorker1_ProgressChanged;
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy == true)
            {
                return;
            }

            UpdateControls(true);

            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            TestRunnerArguments testRunnerArguments = null;
            try
            {
                testRunnerArguments = ArgumentProcessor.ProcessArguments(
                    new FormInput
                    {
                        SpaceFrequency = spaceFrequency.Text,
                        MarkFrequency = markFrequency.Text,
                        Tolerance = tolerance.Text,
                        BaudStart = baudRate.Text,
                        BaudIncrement = "1",
                        BaudEnd = baudRate.Text,
                        BoostStart = string.Empty,
                        BoostIncrement = string.Empty,
                        BoostEnd = string.Empty,
                        WriteWavFiles = writeWavFiles.Checked,
                        PlayAudio = playAudio.Checked,
                        TestString = testString.Text
                    }
                );
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Input validation");
                return;
            }

            var testRunner = new TestRunner();
            testRunner.FskAnalyzer.AnalysisCompleted += AnalysisCompletedHandler;
            testRunner.SignalGenerationCompleted += SignalGenerationCompletedHandler;
            testRunner.Run(testRunnerArguments);
        }

        private void BackgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState is AnalysisResultEventArgs)
            {
                _analysisResult = (AnalysisResultEventArgs)e.UserState;

                UpdateAnalysisResults();
                UpdateMatchIndicator();
            }

            if (e.UserState is SignalGenerationResultEventArgs)
            {
                var signalGenerationResult = (SignalGenerationResultEventArgs)e.UserState;

                UpdateSignalGenerationInformation(signalGenerationResult);
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            UpdateControls(false);
        }

        private void AnalysisCompletedHandler(object sender, AnalysisResultEventArgs e)
        {
            backgroundWorker1.ReportProgress(0, e);
        }

       private void SignalGenerationCompletedHandler(object sender, SignalGenerationResultEventArgs e)
        {
            backgroundWorker1.ReportProgress(0, e);
        }

        private void UpdateControls(bool running)
        {
            if (running == true)
            {
                startButton.Enabled = false;
                startButton.Text = "Analyzing...";
                numberOfSymbolsLabel.Enabled = false;
                numberOfSymbols.Enabled = false;
                audioLengthMicrosecondsLabel.Enabled = false;
                audioLengthMicroseconds.Enabled = false;
                numberOfSymbols.Text = string.Empty;
                audioLengthMicroseconds.Text = string.Empty;
                numberOfZeroFrequencies.Text = string.Empty;
                numberOfFrequencyDifferences.Text = string.Empty;
                averageFrequencyDifference.Text = string.Empty;
                minimumFrequencyDifference.Text = string.Empty;
                maximumFrequencyDifference.Text = string.Empty;
                resultStringLabel.Enabled = false;
                resultString.Enabled = false;
                resultString.Text = string.Empty;
                matchLabel.Enabled = false;
                matchLabel.Text = string.Empty;
                matchLabel.ForeColor = _defaultMatchLabelForeColor;
                matchLabel.BackColor = _defaultMatchLabelBackColor;
                spaceFrequency.ReadOnly = true;
                markFrequency.ReadOnly = true;
                tolerance.ReadOnly = true;
                baudRate.ReadOnly = true;
                testString.ReadOnly = true;
                writeWavFiles.Enabled = false;
                playAudio.Enabled = false;
            }
            else
            {
                startButton.Enabled = true;
                startButton.Text = _defaultStartButtonText;
                spaceFrequency.ReadOnly = false;
                markFrequency.ReadOnly = false;
                tolerance.ReadOnly = false;
                baudRate.ReadOnly = false;
                testString.ReadOnly = false;
                writeWavFiles.Enabled = true;
                playAudio.Enabled = true;
            }
        }

        private void UpdateAnalysisResults()
        {
            numberOfFrequencyDifferencesLabel.Enabled = true;
            numberOfFrequencyDifferences.Enabled = true;
            numberOfFrequencyDifferences.Text = _analysisResult.NumberOfFrequencyDifferences.ToString();

            numberOfZeroFrequenciesLabel.Enabled = true;
            numberOfZeroFrequencies.Enabled = true;
            numberOfZeroFrequencies.Text = _analysisResult.NumberOfZeroFrequencies.ToString();

            averageFrequencyDifferenceLabel.Enabled = true;
            averageFrequencyDifference.Enabled = true;
            averageFrequencyDifference.Text = $"{_analysisResult.AverageFrequencyDifference:N1}";

            minimumFrequencyDifferenceLabel.Enabled = true;
            minimumFrequencyDifference.Enabled = true;
            minimumFrequencyDifference.Text = $"{_analysisResult.MinimumFrequencyDifference:N1}";

            maximumFrequencyDifferenceLabel.Enabled = true;
            maximumFrequencyDifference.Enabled = true;
            maximumFrequencyDifference.Text = $"{_analysisResult.MaximumFrequencyDifference:N1}";

            resultStringLabel.Enabled = true;
            resultString.Enabled = true;
            resultString.Text = _analysisResult.ResultingString;
        }

        private void UpdateMatchIndicator()
        {
            matchLabel.Enabled = true;
            matchLabel.ForeColor = Color.White;

            if (_analysisResult.Matched == true)
            {
                matchLabel.Text = "Matched";
                matchLabel.BackColor = Color.Green;
            }
            else
            {
                matchLabel.Text = "Did not match";
                matchLabel.BackColor = Color.Red;
            }
        }

        private void UpdateSignalGenerationInformation(SignalGenerationResultEventArgs signalGenerationResult)
        {
            numberOfSymbols.Text = signalGenerationResult.NumberOfBits.ToString();
            audioLengthMicroseconds.Text = (signalGenerationResult.AudioLengthInMicroseconds / signalGenerationResult.NumberOfBits).ToString();
            numberOfSymbolsLabel.Enabled = true;
            numberOfSymbols.Enabled = true;
            audioLengthMicrosecondsLabel.Enabled = true;
            audioLengthMicroseconds.Enabled = true;
            scopeControl1.DrawScope(signalGenerationResult.Samples, signalGenerationResult.SampleRate,
                int.Parse(baudRate.Text), int.Parse(numberOfSymbols.Text), zoom.Value);
            _signalGenerationResult = signalGenerationResult;
        }

        private void zoom_ValueChanged(object sender, EventArgs e)
        {
            scopeControl1.DrawScope(_signalGenerationResult.Samples, _signalGenerationResult.SampleRate,
                int.Parse(baudRate.Text), int.Parse(numberOfSymbols.Text), zoom.Value);
        }

        private void SingleSignalAnalyzerControl_Load(object sender, EventArgs e)
        {
            startButton_Click(sender, e);
        }

        private void SetBelowDataGridToolTipText()
        {
            // TODO: Centralize tool tip text to Resource strings

            var spaceFrequencyToolTipText = "FSK space (binary 0) frequency in Hz";
            var markFrequencyToolTipText = "FSK mark (binary 1) frequency in Hz";
            var toleranceToolTipText = "Maximum amount (in Hz) that a detected frequency can deviate from the space and mark frequencies and still be considered valid";

            var baudRateToolTipText = "Baud rate (symbols per second)";

            var startButtonToolTipText = "Begin analyzing FSK encoded signal";

            var writeWavFilesCheckboxToolTipText = "Save a WAV file";
            var playAudioCheckboxToolTipText = "Play generated signal audio";
            var testStringToolTipText = "Test string to encode/decode";
            var resultStringToolTipText = "Encoded/decoded test string";

            var numberOfSymbolsToolTipText = "Number of symbols (typically bits)";
            var symbolLengthToolTipText = "Length of each symbol (in microseconds)";
            var numberOfFrequencyDifferencesToolTipText = "Number of times that the detected frequency was outside of the supplied frequency deviation tolerance";
            var numberOfZeroFrequenciesToolTipText = "Number of times that the detected frequency was zero";
            var averageFrequencyDifferenceToolTipText = "Average difference (in Hz) of expected space or mark frequency and actual detected frequency";
            var minimumFrequencyDifferenceToolTipText = "Minimum difference (in Hz) of expected space or mark frequency and actual detected frequency";
            var maximumFrequencyDifferenceToolTipText = "Maximum difference (in Hz) of expected space or mark frequency and actual detected frequency";

            toolTip1.SetToolTip(spaceFrequency, spaceFrequencyToolTipText);
            toolTip1.SetToolTip(spaceFrequencyLabel, spaceFrequencyToolTipText);

            toolTip1.SetToolTip(markFrequency, markFrequencyToolTipText);
            toolTip1.SetToolTip(markFrequencyLabel, markFrequencyToolTipText);

            toolTip1.SetToolTip(tolerance, toleranceToolTipText);
            toolTip1.SetToolTip(toleranceLabel, toleranceToolTipText);

            toolTip1.SetToolTip(baudRate, baudRateToolTipText);
            toolTip1.SetToolTip(baudRateLabel, baudRateToolTipText);

            toolTip1.SetToolTip(startButton, startButtonToolTipText);

            toolTip1.SetToolTip(writeWavFiles, writeWavFilesCheckboxToolTipText);
            toolTip1.SetToolTip(playAudio, playAudioCheckboxToolTipText);

            toolTip1.SetToolTip(testString, testStringToolTipText);
            toolTip1.SetToolTip(testStringLabel, testStringToolTipText);

            toolTip1.SetToolTip(resultString, resultStringToolTipText);
            toolTip1.SetToolTip(resultStringLabel, resultStringToolTipText);

            toolTip1.SetToolTip(numberOfFrequencyDifferences, numberOfFrequencyDifferencesToolTipText);
            toolTip1.SetToolTip(numberOfFrequencyDifferencesLabel, numberOfFrequencyDifferencesToolTipText);

            toolTip1.SetToolTip(numberOfZeroFrequencies, numberOfZeroFrequenciesToolTipText);
            toolTip1.SetToolTip(numberOfZeroFrequenciesLabel, numberOfZeroFrequenciesToolTipText);

            toolTip1.SetToolTip(numberOfSymbols, numberOfSymbolsToolTipText);
            toolTip1.SetToolTip(numberOfSymbolsLabel, numberOfSymbolsToolTipText);

            toolTip1.SetToolTip(audioLengthMicroseconds, symbolLengthToolTipText);
            toolTip1.SetToolTip(audioLengthMicrosecondsLabel, symbolLengthToolTipText);

            toolTip1.SetToolTip(averageFrequencyDifference, averageFrequencyDifferenceToolTipText);
            toolTip1.SetToolTip(averageFrequencyDifferenceLabel, averageFrequencyDifferenceToolTipText);

            toolTip1.SetToolTip(minimumFrequencyDifference, minimumFrequencyDifferenceToolTipText);
            toolTip1.SetToolTip(minimumFrequencyDifferenceLabel, minimumFrequencyDifferenceToolTipText);

            toolTip1.SetToolTip(maximumFrequencyDifference, maximumFrequencyDifferenceToolTipText);
            toolTip1.SetToolTip(maximumFrequencyDifferenceLabel, maximumFrequencyDifferenceToolTipText);
        }

        private void spaceFrequency_Enter(object sender, EventArgs e)
        {
            spaceFrequency.SelectAll();
        }

        private void markFrequency_Enter(object sender, EventArgs e)
        {
            markFrequency.SelectAll();
        }

        private void tolerance_Enter(object sender, EventArgs e)
        {
            tolerance.SelectAll();
        }

        private void baudRate_Enter(object sender, EventArgs e)
        {
            baudRate.SelectAll();
        }

        private void testString_Enter(object sender, EventArgs e)
        {
            testString.SelectAll();
        }

        private void textboxKeyUp(object sender, KeyEventArgs e)
        {
            e.Handled = true;

            if (e.KeyCode == Keys.Enter)
            {
                startButton_Click(sender, e);
            }
        }
    }
}
