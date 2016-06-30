using Core.BinaryFskAnalysis;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Gui
{
    public partial class SingleSignalAnalyzerControl : UserControl
    {
        private IList<AnalysisResultEventArgs> _analysisResults;

        public SingleSignalAnalyzerControl()
        {
            InitializeComponent();

            _analysisResults = new BindingList<AnalysisResultEventArgs>();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy == true)
            {
                return;
            }

            startButton.Enabled = false;

            _analysisResults.Clear();

            numberOfBitsLabel.Enabled = false;
            numberOfBits.Enabled = false;
            audioLengthMicrosecondsLabel.Enabled = false;
            audioLengthMicroseconds.Enabled = false;
            numberOfBits.Text = string.Empty;
            audioLengthMicroseconds.Text = string.Empty;

            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
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
            testRunner.FskAnalyzer.SamplingCompleted += SamplingCompletedHandler;
            testRunner.SignalGenerationCompleted += SignalGenerationCompletedHandler;
            testRunner.Run(testRunnerArguments);
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Done");
            startButton.Enabled = true;
        }

        private void AnalysisCompletedHandler(object sender, AnalysisResultEventArgs e)
        {
            backgroundWorker1.ReportProgress(0, e);
        }

        private void SamplingCompletedHandler(object sender, SamplingResultEventArgs e)
        {
            backgroundWorker1.ReportProgress(0, e);
        }

        private void SignalGenerationCompletedHandler(object sender, SignalGenerationResultEventArgs e)
        {
            backgroundWorker1.ReportProgress(0, e);
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
