using System;
using System.Windows.Forms;

namespace Gui
{
    public partial class SignalAnalyzerForm : Form
    {
        private MultipleSignalAnalyzerControl _multipleSignalAnalyzerControl;

        public SignalAnalyzerForm()
        {
            InitializeComponent();

            _multipleSignalAnalyzerControl = new MultipleSignalAnalyzerControl();
            _multipleSignalAnalyzerControl.Dock = DockStyle.Fill;
            this.Controls.Add(_multipleSignalAnalyzerControl);

            toolStripStatusLabel1.Text = $"v{System.Reflection.Assembly.GetExecutingAssembly().GetName().Version}";
            statusStrip1.Refresh();
        }

        private void multipleSignalsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _multipleSignalAnalyzerControl.Show();
            singleSignalToolStripMenuItem.Checked = false;
        }

        private void singleSignalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _multipleSignalAnalyzerControl.Hide();
            multipleSignalsToolStripMenuItem.Checked = false;
        }
    }
}
