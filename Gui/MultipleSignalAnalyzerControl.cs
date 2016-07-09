using Core.BinaryFskAnalysis;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Gui
{
    public partial class MultipleSignalAnalyzerControl : UserControl
    {
        private IList<AnalysisResultEventArgs> _analysisResults;

        public MultipleSignalAnalyzerControl()
        {
            InitializeComponent();

            DataGridColumnInitializer.InitializeColumns(mainDataGrid);

            SetBelowDataGridToolTipText();

            _analysisResults = new BindingList<AnalysisResultEventArgs>();
            mainDataGrid.DataSource = _analysisResults;

            backgroundWorker1.ProgressChanged += BackgroundWorker1_ProgressChanged;

            spaceFrequency.Focus();
            spaceFrequency.SelectAll();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy == true)
            {
                return;
            }

            startButton.Enabled = false;
            exportToCsvButton.Enabled = false;

            _analysisResults.Clear();

            numberOfBitsLabel.Enabled = false;
            numberOfBits.Enabled = false;
            audioLengthMicrosecondsLabel.Enabled = false;
            audioLengthMicroseconds.Enabled = false;
            numberOfBits.Text = string.Empty;
            audioLengthMicroseconds.Text = string.Empty;

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
                        BaudStart = baudStart.Text,
                        BaudIncrement = baudIncrement.Text,
                        BaudEnd = baudEnd.Text,
                        BoostStart = boostStart.Text,
                        BoostIncrement = boostIncrement.Text,
                        BoostEnd = boostEnd.Text,
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
                var analysisResult = (AnalysisResultEventArgs)e.UserState;
                _analysisResults.Add(analysisResult);

                if (analysisResult.Matched == true)
                {
                    mainDataGrid.Rows[mainDataGrid.RowCount - 1].Cells[mainDataGrid.ColumnCount - 1].Style.BackColor = Color.Green;
                }
                else
                {
                    mainDataGrid.Rows[mainDataGrid.RowCount - 1].Cells[mainDataGrid.ColumnCount - 1].Style.BackColor = Color.Red;
                }

                mainDataGrid.FirstDisplayedScrollingRowIndex = mainDataGrid.RowCount - 1;

                return;
            }

            var signalGenerationResult = (SignalGenerationResultEventArgs)e.UserState;
            numberOfBits.Text = signalGenerationResult.NumberOfBits.ToString();
            audioLengthMicroseconds.Text = (signalGenerationResult.AudioLengthInMicroseconds / signalGenerationResult.NumberOfBits).ToString();
            numberOfBitsLabel.Enabled = true;
            numberOfBits.Enabled = true;
            audioLengthMicrosecondsLabel.Enabled = true;
            audioLengthMicroseconds.Enabled = true;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (mainDataGrid.RowCount > 0)
            {
                mainDataGrid.FirstDisplayedScrollingRowIndex = 0;
                exportToCsvButton.Enabled = true;
            }

            startButton.Enabled = true;
        }

        private void AnalysisCompletedHandler(object sender, AnalysisResultEventArgs e)
        {
            backgroundWorker1.ReportProgress(0, e);
        }

        private void SignalGenerationCompletedHandler(object sender, SignalGenerationResultEventArgs e)
        {
            backgroundWorker1.ReportProgress(0, e);
        }

        private void exportToCsvButton_Click(object sender, EventArgs e)
        {
            SaveCsvFile();
        }

        private void SaveCsvFile()
        {
            var csv = CsvExporter.ExportToCsv(mainDataGrid);

            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV|*.csv";
            saveFileDialog.Title = "Save CSV file";
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            saveFileDialog.ShowDialog();

            if (!string.IsNullOrEmpty(saveFileDialog.FileName))
            {
                var fileStream = (System.IO.FileStream)saveFileDialog.OpenFile();
                fileStream.Write(Encoding.ASCII.GetBytes(csv), 0, csv.Count());
                fileStream.Close();
            }
        }

        private void SetBelowDataGridToolTipText()
        {
            // TODO: Centralize tool tip text to Resource strings

            var spaceFrequencyToolTipText = "FSK space (binary 0) frequency in Hz";
            var markFrequencyToolTipText = "FSK mark (binary 1) frequency in Hz";
            var toleranceToolTipText = "Maximum amount (in Hz) that a detected frequency can deviate from the space and mark frequencies and still be considered valid";

            var baudStartToolTipText = "Starting baud rate (symbols per second)";
            var baudIncrementToolTipText = "Optional baud rate (symbols per second) increment";
            var baudEndToolTipText = "Optional ending baud rate (symbols per second)";

            var boostStartToolTipText = "Optional \"boost\" starting frequency (in Hz)";
            var boostIncrementToolTipText = "Optional \"boost\" increment frequency (in Hz)";
            var boostEndToolTipText = "Optional \"boost\" ending frequency (in Hz)";

            var startButtonToolTipText = "Begin analyzing FSK encoded signal";
            var exportToCsvButtonToolTipText = "Export results to CSV";

            var writeWavFilesCheckboxToolTipText = "Save a WAV file for each iteration";
            var playAudioCheckboxToolTipText = "Play generated signal audio for each iteration";
            var testStringToolTipText = "Test string for encoding/decoding";

            toolTip1.SetToolTip(spaceFrequency, spaceFrequencyToolTipText);
            toolTip1.SetToolTip(spaceFrequencyLabel, spaceFrequencyToolTipText);

            toolTip1.SetToolTip(markFrequency, markFrequencyToolTipText);
            toolTip1.SetToolTip(markFrequencyLabel, markFrequencyToolTipText);

            toolTip1.SetToolTip(tolerance, toleranceToolTipText);
            toolTip1.SetToolTip(toleranceLabel, toleranceToolTipText);

            toolTip1.SetToolTip(baudStart, baudStartToolTipText);
            toolTip1.SetToolTip(baudStartLabel, baudStartToolTipText);

            toolTip1.SetToolTip(baudIncrement, baudIncrementToolTipText);
            toolTip1.SetToolTip(baudIncrementLabel, baudIncrementToolTipText);

            toolTip1.SetToolTip(baudEnd, baudEndToolTipText);
            toolTip1.SetToolTip(baudEndLabel, baudEndToolTipText);

            toolTip1.SetToolTip(boostStart, boostStartToolTipText);
            toolTip1.SetToolTip(boostStartLabel, boostStartToolTipText);

            toolTip1.SetToolTip(boostIncrement, boostIncrementToolTipText);
            toolTip1.SetToolTip(boostIncrementLabel, boostIncrementToolTipText);

            toolTip1.SetToolTip(boostEnd, boostEndToolTipText);
            toolTip1.SetToolTip(boostEndLabel, boostEndToolTipText);

            toolTip1.SetToolTip(startButton, startButtonToolTipText);
            toolTip1.SetToolTip(exportToCsvButton, exportToCsvButtonToolTipText);

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

        private void baudStart_Enter(object sender, EventArgs e)
        {
            baudStart.SelectAll();
        }

        private void baudIncrement_Enter(object sender, EventArgs e)
        {
            baudIncrement.SelectAll();
        }

        private void baudEnd_Enter(object sender, EventArgs e)
        {
            baudEnd.SelectAll();
        }

        private void boostStart_Enter(object sender, EventArgs e)
        {
            boostStart.SelectAll();
        }

        private void boostIncrement_Enter(object sender, EventArgs e)
        {
            boostIncrement.SelectAll();
        }

        private void boostEnd_Enter(object sender, EventArgs e)
        {
            boostEnd.SelectAll();
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
