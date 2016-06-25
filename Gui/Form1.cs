using Core.BinaryFskAnalysis;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Gui
{
    public partial class Form1 : Form
    {
        private IList<AnalysisResultEventArgs> _analysisResults;

        public Form1()
        {
            InitializeComponent();

            mainDataGrid.AutoGenerateColumns = false;
            mainDataGrid.ColumnCount = 8;
            mainDataGrid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            mainDataGrid.Columns[0].Name = "Boost (Hz)";
            mainDataGrid.Columns[0].HeaderText = "Boost (Hz)";
            mainDataGrid.Columns[0].DataPropertyName = "BoostFrequencyAmount";
            mainDataGrid.Columns[0].Frozen = false;
            mainDataGrid.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            mainDataGrid.Columns[0].ToolTipText = "Amount (in Hz) that the original space and mark frequencies were increased by";
            mainDataGrid.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;

            mainDataGrid.Columns[1].Name = "Avg diff (Hz)";
            mainDataGrid.Columns[1].HeaderText = "Avg diff (Hz)";
            mainDataGrid.Columns[1].DataPropertyName = "AverageFrequencyDifference";
            mainDataGrid.Columns[1].Frozen = false;
            mainDataGrid.Columns[1].DefaultCellStyle.Format = "0.0";
            mainDataGrid.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            mainDataGrid.Columns[1].ToolTipText = "Average difference (in Hz) of expected space or mark frequency and actual detected frequency";
            mainDataGrid.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;

            mainDataGrid.Columns[2].Name = "Min diff (Hz)";
            mainDataGrid.Columns[2].HeaderText = "Min diff (Hz)";
            mainDataGrid.Columns[2].DataPropertyName = "MinimumFrequencyDifference";
            mainDataGrid.Columns[2].Frozen = false;
            mainDataGrid.Columns[2].DefaultCellStyle.Format = "0.0";
            mainDataGrid.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            mainDataGrid.Columns[2].ToolTipText = "Minimum difference (in Hz) of expected space or mark frequency and actual detected frequency";
            mainDataGrid.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;

            mainDataGrid.Columns[3].Name = "Max diff (Hz)";
            mainDataGrid.Columns[3].HeaderText = "Max diff (Hz)";
            mainDataGrid.Columns[3].DataPropertyName = "MaximumFrequencyDifference";
            mainDataGrid.Columns[3].Frozen = false;
            mainDataGrid.Columns[3].DefaultCellStyle.Format = "0.0";
            mainDataGrid.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            mainDataGrid.Columns[3].ToolTipText = "Maximum difference (in Hz) of expected space or mark frequency and actual detected frequency";
            mainDataGrid.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;

            mainDataGrid.Columns[4].Name = "# > tolerance";
            mainDataGrid.Columns[4].HeaderText = "# > tolerance";
            mainDataGrid.Columns[4].DataPropertyName = "NumberOfMissedFrequencies";
            mainDataGrid.Columns[4].Frozen = false;
            mainDataGrid.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            mainDataGrid.Columns[4].ToolTipText = "Number of times that the detected frequency was outside of the supplied frequency deviation tolerance";
            mainDataGrid.Columns[4].SortMode = DataGridViewColumnSortMode.NotSortable;

            mainDataGrid.Columns[5].Name = "# zero freq";
            mainDataGrid.Columns[5].HeaderText = "# zero freq";
            mainDataGrid.Columns[5].DataPropertyName = "NumberOfZeroFrequencies";
            mainDataGrid.Columns[5].Frozen = false;
            mainDataGrid.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            mainDataGrid.Columns[5].ToolTipText = "Number of times that the detected frequency was zero";
            mainDataGrid.Columns[5].SortMode = DataGridViewColumnSortMode.NotSortable;

            mainDataGrid.Columns[6].Name = "Output";
            mainDataGrid.Columns[6].HeaderText = "Output";
            mainDataGrid.Columns[6].DataPropertyName = "ResultingString";
            mainDataGrid.Columns[6].Frozen = false;
            mainDataGrid.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            mainDataGrid.Columns[6].ToolTipText = "The string that resulted from decoding the encoded signal";
            mainDataGrid.Columns[6].SortMode = DataGridViewColumnSortMode.NotSortable;

            mainDataGrid.Columns[7].Name = "Match?";
            mainDataGrid.Columns[7].HeaderText = "Match?";
            mainDataGrid.Columns[7].DataPropertyName = "Matched";
            mainDataGrid.Columns[7].Frozen = false;
            mainDataGrid.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            mainDataGrid.Columns[7].ToolTipText = "Whether or not the decoded signal matched the encoded signal";
            mainDataGrid.Columns[7].SortMode = DataGridViewColumnSortMode.NotSortable;

            _analysisResults = new BindingList<AnalysisResultEventArgs>();
            mainDataGrid.DataSource = _analysisResults;

            backgroundWorker1.ProgressChanged += BackgroundWorker1_ProgressChanged;

            startButton.Select();

            toolStripStatusLabel1.Text = $"Build {System.Reflection.Assembly.GetExecutingAssembly().GetName().Version}";
            statusStrip1.Refresh();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            startButton.Enabled = false;
            exportToCsvButton.Enabled = false;

            _analysisResults.Clear();

            numberOfBitsLabel.Enabled = false;
            numberOfBits.Enabled = false;
            audioLengthMillisecondsLabel.Enabled = false;
            audioLengthMilliseconds.Enabled = false;
            numberOfBits.Text = string.Empty;
            audioLengthMilliseconds.Text = string.Empty;

            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            var spaceFrequencyStringValue = spaceFrequency.Text;
            var markFrequencyStringValue = markFrequency.Text;
            var baudRateStringValue = baudRate.Text;

            var boostStartStringValue = boostStart.Text;
            var boostIncrementStringValue = boostIncrement.Text;
            var boostEndStringValue = boostEnd.Text;

            var toleranceStringValue = tolerance.Text;

            if (string.IsNullOrWhiteSpace(spaceFrequencyStringValue))
            {
                MessageBox.Show("Space frequency cannot be empty", "Input required");
                return;
            }

            if (string.IsNullOrWhiteSpace(markFrequencyStringValue))
            {
                MessageBox.Show("Mark frequency cannot be empty", "Input required");
                return;
            }

            if (string.IsNullOrWhiteSpace(baudRateStringValue))
            {
                MessageBox.Show("Baud rate cannot be empty", "Input required");
                return;
            }

            if (string.IsNullOrWhiteSpace(boostStartStringValue))
            {
                MessageBox.Show("Boost start cannot be empty", "Input required");
                return;
            }

            if (string.IsNullOrWhiteSpace(boostIncrementStringValue))
            {
                MessageBox.Show("Boost increment cannot be empty", "Input required");
                return;
            }

            if (string.IsNullOrWhiteSpace(boostEndStringValue))
            {
                MessageBox.Show("Boost end cannot be empty", "Input required");
                return;
            }

            if (string.IsNullOrWhiteSpace(toleranceStringValue))
            {
                MessageBox.Show("Tolerance cannot be empty", "Input required");
                return;
            }

            var spaceFrequencyDoubleValue = double.Parse(spaceFrequencyStringValue);
            var markFrequencyDoubleValue = double.Parse(markFrequencyStringValue);
            var baudRateIntValue = int.Parse(baudRateStringValue);

            double boostStartDoubleValue = double.Parse(boostStartStringValue);
            double boostIncrementDoubleValue = double.Parse(boostIncrementStringValue);
            double boostEndDoubleValue = double.Parse(boostEndStringValue);

            double toleranceDoubleValue = double.Parse(toleranceStringValue);

            var testRunner = new TestRunner();
            testRunner.FskAnalyzer.AnalysisCompleted += AnalysisCompletedHandler;
            testRunner.SignalGenerationCompleted += SignalGenerationCompletedHandler;
            testRunner.Run(spaceFrequencyDoubleValue, markFrequencyDoubleValue, baudRateIntValue,
                boostStartDoubleValue, boostIncrementDoubleValue, boostEndDoubleValue, toleranceDoubleValue);
        }

        private void BackgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState is AnalysisResultEventArgs)
            {
                var analysisResult = (AnalysisResultEventArgs)e.UserState;
                _analysisResults.Add(analysisResult);

                if (analysisResult.Matched == true)
                {
                    mainDataGrid.Rows[mainDataGrid.RowCount - 1].Cells[7].Style.BackColor = System.Drawing.Color.Green;
                }
                else
                {
                    mainDataGrid.Rows[mainDataGrid.RowCount - 1].Cells[7].Style.BackColor = System.Drawing.Color.Red;
                }

                mainDataGrid.FirstDisplayedScrollingRowIndex = mainDataGrid.RowCount - 1;
            }
            else
            {
                var signalGenerationResult = (SignalGenerationResultEventArgs)e.UserState;
                numberOfBits.Text = signalGenerationResult.NumberOfBits.ToString();
                audioLengthMilliseconds.Text = (signalGenerationResult.AudioLengthInMilliseconds / 1000.0).ToString();
                numberOfBitsLabel.Enabled = true;
                numberOfBits.Enabled = true;
                audioLengthMillisecondsLabel.Enabled = true;
                audioLengthMilliseconds.Enabled = true;
            }
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
            var csvStringBuilder = new StringBuilder();

            var headers = mainDataGrid.Columns.Cast<DataGridViewColumn>();
            csvStringBuilder.AppendLine(string.Join(",", headers.Select(column => "\"" + column.HeaderText + "\"").ToArray()));

            foreach (DataGridViewRow row in mainDataGrid.Rows)
            {
                var cells = row.Cells.Cast<DataGridViewCell>();
                csvStringBuilder.AppendLine(string.Join(",", cells.Select(cell => "\"" + cell.Value.ToString().Replace("\"", "") + "\"").ToArray()));
            }

            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV|*.csv";
            saveFileDialog.Title = "Save CSV file";
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            saveFileDialog.ShowDialog();

            if (!string.IsNullOrEmpty(saveFileDialog.FileName))
            {
                var fileStream = (System.IO.FileStream)saveFileDialog.OpenFile();
                fileStream.Write(Encoding.ASCII.GetBytes(csvStringBuilder.ToString()), 0, csvStringBuilder.ToString().Count());
                fileStream.Close();
            }
        }

        private void mainDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e) {}
        private void form1BindingSource_CurrentChanged(object sender, EventArgs e) {}
        private void Form1_Load(object sender, EventArgs e) {}
    }
}
