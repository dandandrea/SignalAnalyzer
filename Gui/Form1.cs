using Core.BinaryFskAnalysis;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace Gui
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            backgroundWorker1.ProgressChanged += BackgroundWorker1_ProgressChanged;
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            startButton.Enabled = false;
            mainOutput.Clear();
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
            mainOutput.AppendText($"Boost amount: {analysisResult.BoostFrequencyAmount} Hz, ");
            mainOutput.AppendText($"avg. freq. diff.: {analysisResult.AverageFrequencyDifference:N1}, ");
            mainOutput.AppendText($"# miss. freq.: {analysisResult.NumberOfMissedFrequencies}");

            if (analysisResult.ResultingString != null)
            {
                mainOutput.AppendText($", resulting string: [{analysisResult.ResultingString}]");

                if (analysisResult.Matched == true)
                {
                    mainOutput.AppendText("   MATCHED");
                }
            }

            mainOutput.AppendText(Environment.NewLine);
        }

        private void AnalysisCompletedHandler(object sender, AnalysisResultEventArgs e)
        {
            backgroundWorker1.ReportProgress(0, e);
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            mainOutput.SelectionStart = 0;
            mainOutput.ScrollToCaret();
            startButton.Enabled = true;
        }

        private void Form1_Load(object sender, EventArgs e) { }
    }
}
