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

            var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            var versionString = $"v{version.Major}.{version.Minor}.{version.Revision}";
            toolStripStatusLabel1.Text = versionString;
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
