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

                UpdateResultString();
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
                numberOfBitsLabel.Enabled = false;
                numberOfBits.Enabled = false;
                audioLengthMicrosecondsLabel.Enabled = false;
                audioLengthMicroseconds.Enabled = false;
                numberOfBits.Text = string.Empty;
                audioLengthMicroseconds.Text = string.Empty;
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
                boost.ReadOnly = true;
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
                boost.ReadOnly = false;
                testString.ReadOnly = false;
                writeWavFiles.Enabled = true;
                playAudio.Enabled = true;
            }
        }

        private void UpdateResultString()
        {
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
            numberOfBits.Text = signalGenerationResult.NumberOfBits.ToString();
            audioLengthMicroseconds.Text = (signalGenerationResult.AudioLengthInMicroseconds / signalGenerationResult.NumberOfBits).ToString();
            numberOfBitsLabel.Enabled = true;
            numberOfBits.Enabled = true;
            audioLengthMicrosecondsLabel.Enabled = true;
            audioLengthMicroseconds.Enabled = true;
            scopeControl1.DrawScope(signalGenerationResult.Samples, signalGenerationResult.SampleRate,
                int.Parse(baudRate.Text), int.Parse(numberOfBits.Text));
        }

        private void SetBelowDataGridToolTipText()
        {
            var spaceFrequencyToolTipText = "FSK space (binary 0) frequency in Hz";
            var markFrequencyToolTipText = "FSK mark (binary 1) frequency in Hz";
            var toleranceToolTipText = "Maximum amount (in Hz) that a detected frequency can deviate from the space and mark frequencies and still be considered valid";

            var baudRateToolTipText = "Baud rate (symbols per second)";
            var boostToolTipText = "Optional \"boost\" frequency (in Hz)";

            var startButtonToolTipText = "Begin analyzing FSK encoded signal";

            var writeWavFilesCheckboxToolTipText = "Save a WAV file";
            var playAudioCheckboxToolTipText = "Play generated signal audio";
            var testStringToolTipText = "Test string for encoding/decoding";

            toolTip1.SetToolTip(spaceFrequency, spaceFrequencyToolTipText);
            toolTip1.SetToolTip(spaceFrequencyLabel, spaceFrequencyToolTipText);

            toolTip1.SetToolTip(markFrequency, markFrequencyToolTipText);
            toolTip1.SetToolTip(markFrequencyLabel, markFrequencyToolTipText);

            toolTip1.SetToolTip(tolerance, toleranceToolTipText);
            toolTip1.SetToolTip(toleranceLabel, toleranceToolTipText);

            toolTip1.SetToolTip(baudRate, baudRateToolTipText);
            toolTip1.SetToolTip(baudRateLabel, baudRateToolTipText);

            toolTip1.SetToolTip(boost, boostToolTipText);
            toolTip1.SetToolTip(boostLabel, boostToolTipText);

            toolTip1.SetToolTip(startButton, startButtonToolTipText);

            toolTip1.SetToolTip(writeWavFiles, writeWavFilesCheckboxToolTipText);
            toolTip1.SetToolTip(playAudio, playAudioCheckboxToolTipText);

            toolTip1.SetToolTip(testString, testStringToolTipText);
            toolTip1.SetToolTip(testStringLabel, testStringToolTipText);
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

        private void boost_Enter(object sender, EventArgs e)
        {
            boost.SelectAll();
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
