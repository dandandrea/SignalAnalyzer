using Core.BinaryFskAnalysis;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Gui
{
    public partial class SingleSignalAnalyzerControl : UserControl
    {
        private Color _defaultMatchLabelForeColor;
        private Color _defaultMatchLabelBackColor;
        private string _defaultStartButtonText;

        private static readonly string _defaultFilenameText = "File name";

        public SingleSignalAnalyzerControl()
        {
            InitializeComponent();

            filenameTextBox.Text = _defaultFilenameText;
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

            DisableControls();

            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            string filename = null;
            if (radioExistingFile.Checked == true)
            {
                if (string.IsNullOrEmpty(filenameTextBox.Text) || filenameTextBox.Text == _defaultFilenameText)
                {
                    MessageBox.Show("Please select a file", "No file specified");
                    return;
                }

                try
                {
                    using (var fileStream = File.Open(filenameTextBox.Text, FileMode.Open, FileAccess.Read, FileShare.None))
                    {
                        filename = filenameTextBox.Text;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error opening file ({ex.Message})", "File error");
                    return;
                }
            }

            if (radioExistingFile.Checked == true)
            {
                FileBasedFormInput formInput = null;
                try
                {
                    formInput = new FileBasedFormInput(spaceFrequency.Text, markFrequency.Text, tolerance.Text, baudRate.Text, "1", baudRate.Text,
                        string.Empty, string.Empty, string.Empty, filename, playAudio.Checked);
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show(ex.Message, "Input validation");
                    return;
                }

                var testRunner = new TestRunner();
                var analysisResult = testRunner.Run(formInput);
                backgroundWorker1.ReportProgress(0, analysisResult);
            }
            else
            {
                SignalGenerationBasedFormInput formInput = null;
                try
                {
                    formInput = new SignalGenerationBasedFormInput(spaceFrequency.Text, markFrequency.Text, tolerance.Text, baudRate.Text, "1", baudRate.Text,
                        string.Empty, string.Empty, string.Empty, writeWavFiles.Checked, playAudio.Checked, testString.Text);
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show(ex.Message, "Input validation");
                    return;
                }

                var testRunner = new TestRunner();
                var analysisResult = testRunner.Run(formInput);
                backgroundWorker1.ReportProgress(0, analysisResult);
            }
        }

        private void BackgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var analysisResult = (AnalysisResult)e.UserState;

            UpdateScope(analysisResult);
            UpdateAnalysisResults(analysisResult);
            UpdateMatchIndicator(analysisResult);

            if (analysisResult.SignalGenerationInformation != null)
            {
                Debug.WriteLine("Updating signal generation information");

                // TODO: Update signal generation information
                Debug.WriteLine("TODO: UPDATE SIGNAL GENERATION INFORMATION");
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            EnableControls();
        }

        private void DisableControls()
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

        public void EnableControls()
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

        private void UpdateScope(AnalysisResult analysisResult)
        {
            Debug.WriteLine("Updating scope");

            numberOfSymbols.Text = analysisResult.SignalGenerationInformation.NumberOfBits.ToString();
            audioLengthMicroseconds.Text =
                (analysisResult.SignalGenerationInformation.AudioLengthInMicroseconds / analysisResult.SignalGenerationInformation.NumberOfBits).ToString();
            numberOfSymbolsLabel.Enabled = true;
            numberOfSymbols.Enabled = true;
            audioLengthMicrosecondsLabel.Enabled = true;
            audioLengthMicroseconds.Enabled = true;
            float[] samples = new float[analysisResult.SignalGenerationInformation.Samples.Count];
            analysisResult.SignalGenerationInformation.Samples.CopyTo(samples, 0);
            scopeControl1.DrawScope(samples, analysisResult, analysisResult.SignalGenerationInformation.SampleRate, int.Parse(baudRate.Text),
                int.Parse(numberOfSymbols.Text), zoom.Value);

        }

        private void UpdateAnalysisResults(AnalysisResult analysisResult)
        {
            numberOfFrequencyDifferencesLabel.Enabled = true;
            numberOfFrequencyDifferences.Enabled = true;
            numberOfFrequencyDifferences.Text = analysisResult.NumberOfFrequencyDifferences.ToString();

            numberOfZeroFrequenciesLabel.Enabled = true;
            numberOfZeroFrequencies.Enabled = true;
            numberOfZeroFrequencies.Text = analysisResult.NumberOfZeroFrequencies.ToString();

            averageFrequencyDifferenceLabel.Enabled = true;
            averageFrequencyDifference.Enabled = true;
            averageFrequencyDifference.Text = $"{analysisResult.AverageFrequencyDifference:N1}";

            minimumFrequencyDifferenceLabel.Enabled = true;
            minimumFrequencyDifference.Enabled = true;
            minimumFrequencyDifference.Text = $"{analysisResult.MinimumFrequencyDifference:N1}";

            maximumFrequencyDifferenceLabel.Enabled = true;
            maximumFrequencyDifference.Enabled = true;
            maximumFrequencyDifference.Text = $"{analysisResult.MaximumFrequencyDifference:N1}";

            resultStringLabel.Enabled = true;
            resultString.Enabled = true;
            resultString.Text = analysisResult.ResultingString;
        }

        private void UpdateMatchIndicator(AnalysisResult analysisResult)
        {
            matchLabel.Enabled = true;
            matchLabel.ForeColor = Color.White;

            if (analysisResult.Match.HasValue == false)
            {
                matchLabel.Visible = false;
            }
            else if (analysisResult.Match == true)
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

        private void zoom_ValueChanged(object sender, EventArgs e)
        {
            float[] samples = new float[scopeControl1.AnalysisResult.SignalGenerationInformation.Samples.Count];
            scopeControl1.AnalysisResult.SignalGenerationInformation.Samples.CopyTo(samples, 0);
            scopeControl1.DrawScope(samples, scopeControl1.AnalysisResult, scopeControl1.AnalysisResult.SignalGenerationInformation.SampleRate,
                int.Parse(baudRate.Text), int.Parse(numberOfSymbols.Text), zoom.Value);
        }

        private void SingleSignalAnalyzerControl_Load(object sender, EventArgs e)
        {
            startButton_Click(sender, e);
        }

        private void radioGenerate_Click(object sender, EventArgs e)
        {
            existingFilePanel.Visible = false;
            generationPanel.Visible = true;
            openFileButton.Enabled = false;
            openFileButton.Visible = false;
        }

        private void radioExistingFile_Click(object sender, EventArgs e)
        {
            generationPanel.Visible = false;
            existingFilePanel.Visible = true;
            openFileButton.Enabled = true;
            openFileButton.Visible = true;
        }

        private void openFileButton_Click(object sender, EventArgs e)
        {
            var result = openFileDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                filenameTextBox.Text = openFileDialog1.FileName;
            }
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
            var testStringToolTipText = "String to use for signal generation";
            var resultStringToolTipText = "Decoded string";

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
