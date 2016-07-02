using Core.AudioGeneration;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace GuiControls
{
    public partial class ScopeControl : UserControl
    {
        public ScopeControl()
        {
            InitializeComponent();
        }

        public void DrawScope(float[] samples, int sampleRate, int baudRate, int numberOfSymbols)
        {
            var audioScaler = (IAudioScaler)new AudioScaler();
            samples = audioScaler.Scale(samples, sampleRate, baudRate, numberOfSymbols, scopePictureBox.Width, scopePictureBox.Height);

            Bitmap bmp;
            if (scopePictureBox.Image == null)
            {
                bmp = new Bitmap(scopePictureBox.Width, scopePictureBox.Height);
            }
            else
            {
                bmp = (Bitmap)scopePictureBox.Image;
            }

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.Black);

                g.DrawLine(new Pen(Color.LightGreen), new Point(0, scopePictureBox.Height / 2), new Point(scopePictureBox.Width, scopePictureBox.Height / 2));

                var greenPen = new Pen(Color.Green);
                var yellowPen = new Pen(Color.Yellow);

                var sampleFramePositions = new List<int>();
                for (var n = 1; n < numberOfSymbols; n++)
                {
                    sampleFramePositions.Add((int)(n * audioScaler.SamplesPerSymbol));
                }

                for (int x = 0; x < scopePictureBox.Width - 1; x++)
                {
                    g.DrawLine(greenPen, new Point(x, (int)samples[x]), new Point(x + 1, (int)samples[x + 1]));

                    if (sampleFramePositions.Contains(x))
                    {
                        Debug.WriteLine($"Drawing symbol frame at {x} ({scopePictureBox.Width - 1})");
                        g.DrawLine(yellowPen, new Point(x, 0), new Point(x, scopePictureBox.Height));
                    }
                }
            }

            scopePictureBox.Image = bmp;
            scopePictureBox.Refresh();
        }
    }
}
