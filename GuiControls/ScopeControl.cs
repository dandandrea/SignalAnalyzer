using Core.AudioGeneration;
using Core.BinaryFskAnalysis;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace GuiControls
{
    public partial class ScopeControl : UserControl
    {
        public AnalysisResult AnalysisResult { get; private set; }

        // TODO: Is there a better way to set this?
        private static double _imageHeightFactor = 0.8;

        private int _pictureBoxOriginalWidth;
        private int _pictureBoxOriginalHeight;

        public ScopeControl()
        {
            InitializeComponent();

            _pictureBoxOriginalWidth = scopePictureBox.Width;
            _pictureBoxOriginalHeight = scopePictureBox.Height;
        }

        public void Clear()
        {
            var image = new Bitmap(scopePictureBox.Width, scopePictureBox.Height);

            using (Graphics g = Graphics.FromImage(image))
            {
                g.Clear(Color.Black);
            }

            scopePictureBox.Image = image;
            scopePictureBox.Refresh();
        }

        public void DrawScope(float[] samples, AnalysisResult analysisResult, int sampleRate, int baudRate, int numberOfSymbols, int desiredSamplesPerSymbol = 100)
        {
            AnalysisResult = analysisResult;

            var samplesPerSymbol = sampleRate / baudRate;

            // Debug.WriteLine($"[ScopeControl] Samples per symbol: {samplesPerSymbol}, desired samples per symbol: {desiredSamplesPerSymbol}");

            var imageWidth = (int)(desiredSamplesPerSymbol * numberOfSymbols);
            var imageHeight = (int)Math.Floor(_pictureBoxOriginalHeight * _imageHeightFactor);

            var audioScaler = (IAudioScaler)new AudioScaler();
            samples = audioScaler.Scale(samples, sampleRate, baudRate, numberOfSymbols, imageWidth, imageHeight);

            var image = new Bitmap(imageWidth, imageHeight);

            using (Graphics g = Graphics.FromImage(image))
            {
                g.Clear(Color.Black);

                g.DrawLine(new Pen(Color.LightGreen), new Point(0, imageHeight / 2), new Point(imageWidth, imageHeight / 2));

                var greenPen = new Pen(Color.Green);
                var yellowPen = new Pen(Color.Yellow);

                var sampleFramePositions = new List<int>();
                for (var n = 1; n < numberOfSymbols; n++)
                {
                    sampleFramePositions.Add((int)(n * audioScaler.SamplesPerSymbol));
                }

                for (int x = 0; x < imageWidth - 1; x++)
                {
                    g.DrawLine(greenPen, new Point(x, (int)samples[x]), new Point(x + 1, (int)samples[x + 1]));

                    if (sampleFramePositions.Contains(x))
                    {
                        // Debug.WriteLine($"[ScopeControl] Drawing symbol frame at {x} ({imageWidth - 1})");

                        g.DrawLine(yellowPen, new Point(x, 0), new Point(x, imageHeight));
                    }
                }

                var textBrush = new SolidBrush(Color.White);
                var mediumFont = new Font("Arial", 10);
                var smallFont = new Font("Arial", 8);

                if (analysisResult != null && analysisResult.AnalysisFrames != null)
                {
                    var analysisFrames = new AnalysisFrame[analysisResult.AnalysisFrames.Count];
                    analysisResult.AnalysisFrames.CopyTo(analysisFrames, 0);

                    var frame = 0;
                    for (int x = 0; x < imageWidth - 1; x++)
                    {
                        if (x == 0 || sampleFramePositions.Contains(x))
                        {
                            var frequency = analysisFrames[frame].Frequency;
                            var frequencyDifference = analysisFrames[frame].DifferenceFromExpectedFrequencies;
                            var symbol = analysisFrames[frame].Bit.HasValue ? (analysisFrames[frame].Bit.Value == true ? "1" : "0") : "-";
                            var currentTimeMicroseconds = analysisFrames[frame].TimeOffsetMicroseconds;

                            var line1 = $"{frequency} Hz";
                            var line2 = $"({frequencyDifference} Hz dev)";
                            var line3 = $"@ {currentTimeMicroseconds:N1} μs";
                            var line4 = $"{symbol}";

                            g.DrawString(line1, mediumFont, textBrush, new PointF(x + 2, imageHeight - 65));
                            g.DrawString(line2, smallFont, textBrush, new PointF(x + 2, imageHeight - 50));
                            g.DrawString(line3, mediumFont, textBrush, new PointF(x + 2, imageHeight - 35));
                            g.DrawString(line4, mediumFont, textBrush, new PointF(x + 2, imageHeight - 18));

                            frame++;
                        }
                    }
                }
            }

            scopePictureBox.Image = image;
            scopePictureBox.Refresh();
        }
    }
}
