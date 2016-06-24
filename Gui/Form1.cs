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
            mainDataGrid.ColumnCount = 5;
            mainDataGrid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            mainDataGrid.Columns[0].Name = "Boost (Hz)";
            mainDataGrid.Columns[0].HeaderText = "Boost (Hz)";
            mainDataGrid.Columns[0].DataPropertyName = "BoostFrequencyAmount";
            mainDataGrid.Columns[0].Frozen = false;
            mainDataGrid.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            mainDataGrid.Columns[1].Name = "Freq. diff. (Hz)";
            mainDataGrid.Columns[1].HeaderText = "Freq. diff. (Hz)";
            mainDataGrid.Columns[1].DataPropertyName = "AverageFrequencyDifference";
            mainDataGrid.Columns[1].Frozen = false;
            mainDataGrid.Columns[1].DefaultCellStyle.Format = "0.0";
            mainDataGrid.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            mainDataGrid.Columns[2].Name = "# miss";
            mainDataGrid.Columns[2].HeaderText = "# miss";
            mainDataGrid.Columns[2].DataPropertyName = "NumberOfMissedFrequencies";
            mainDataGrid.Columns[2].Frozen = false;
            mainDataGrid.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            mainDataGrid.Columns[3].Name = "Output";
            mainDataGrid.Columns[3].HeaderText = "Output";
            mainDataGrid.Columns[3].DataPropertyName = "ResultingString";
            mainDataGrid.Columns[3].Frozen = false;
            mainDataGrid.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            mainDataGrid.Columns[4].Name = "Match?";
            mainDataGrid.Columns[4].HeaderText = "Match?";
            mainDataGrid.Columns[4].DataPropertyName = "Matched";
            mainDataGrid.Columns[4].Frozen = false;
            mainDataGrid.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            _analysisResults = new BindingList<AnalysisResultEventArgs>();
            mainDataGrid.DataSource = _analysisResults;

            backgroundWorker1.ProgressChanged += BackgroundWorker1_ProgressChanged;
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            startButton.Enabled = false;
            exportToCsvButton.Enabled = false;
            _analysisResults.Clear();
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

            var spaceFrequencyDoubleValue = double.Parse(spaceFrequencyStringValue);
            var markFrequencyDoubleValue = double.Parse(markFrequencyStringValue);
            var baudRateIntValue = int.Parse(baudRateStringValue);

            double boostStartDoubleValue = double.Parse(boostStartStringValue);
            double boostIncrementDoubleValue = double.Parse(boostIncrementStringValue);
            double boostEndDoubleValue = double.Parse(boostEndStringValue);

            var testRunner = new TestRunner();
            testRunner.FskAnalyzer.AnalysisCompleted += AnalysisCompletedHandler;
            testRunner.Run(spaceFrequencyDoubleValue, markFrequencyDoubleValue, baudRateIntValue,
                boostStartDoubleValue, boostIncrementDoubleValue, boostEndDoubleValue);
        }

        private void BackgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var analysisResult = (AnalysisResultEventArgs)e.UserState;
            _analysisResults.Add(analysisResult);

            if (analysisResult.Matched == true)
            {
                mainDataGrid.Rows[mainDataGrid.RowCount - 1].Cells[4].Style.BackColor = System.Drawing.Color.Green;
            }
            else
            {
                mainDataGrid.Rows[mainDataGrid.RowCount - 1].Cells[4].Style.BackColor = System.Drawing.Color.Red;
            }

            mainDataGrid.FirstDisplayedScrollingRowIndex = mainDataGrid.RowCount - 1;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            mainDataGrid.FirstDisplayedScrollingRowIndex = 0;
            startButton.Enabled = true;
            exportToCsvButton.Enabled = true;
        }

        private void AnalysisCompletedHandler(object sender, AnalysisResultEventArgs e)
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
