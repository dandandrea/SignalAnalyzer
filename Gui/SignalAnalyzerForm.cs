using System;
using System.Windows.Forms;

namespace Gui
{
    public partial class SignalAnalyzerForm : Form
    {
        public SignalAnalyzerForm()
        {
            InitializeComponent();

            multipleSignalsToolStripMenuItem.Checked = false;
            multipleSignalAnalyzerControl1.Hide();

            singleSignalToolStripMenuItem.Checked = true;
            singleSignalAnalyzerControl1.Show();

            var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            var versionString = $"v{version.Major}.{version.Minor}.{version.Build}";
            toolStripStatusLabel1.Text = versionString;
            statusStrip1.Refresh();
        }

        private void multipleSignalsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            singleSignalAnalyzerControl1.Hide();
            multipleSignalAnalyzerControl1.Show();
            singleSignalToolStripMenuItem.Checked = false;
        }

        private void singleSignalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            multipleSignalAnalyzerControl1.Hide();
            singleSignalAnalyzerControl1.Show();
            multipleSignalsToolStripMenuItem.Checked = false;
        }
    }
}
