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

        public void DrawScope(float[] samples, AnalysisResultEventArgs analysisResult, int sampleRate, int baudRate, int numberOfSymbols, int desiredSamplesPerSymbol = 100)
        {
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
                var textBrush = new SolidBrush(Color.White);
                var font = new Font("Arial", 12);

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

                if (analysisResult != null && analysisResult.AnalysisResult != null && analysisResult.AnalysisResult.AnalysisFrames != null)
                {
                    var analysisFrames = new AnalysisFrame[analysisResult.AnalysisResult.AnalysisFrames.Count];
                    analysisResult.AnalysisResult.AnalysisFrames.CopyTo(analysisFrames, 0);

                    var frame = 0;
                    for (int x = 0; x < imageWidth - 1; x++)
                    {
                        if (x == 0 || sampleFramePositions.Contains(x))
                        {
                            var line1 = $"{analysisFrames[frame].Frequency} Hz";

                            g.DrawString(line1, font, textBrush, new PointF(x + 2, imageHeight - 20));

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
