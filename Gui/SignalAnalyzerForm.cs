using System;
using System.Windows.Forms;

namespace Gui
{
    public partial class SignalAnalyzerForm : Form
    {
        private MultipleSignalAnalyzerControl _multipleSignalAnalyzerControl;
        private SingleSignalAnalyzerControl _singleSignalAnalyzerControl;

        public SignalAnalyzerForm()
        {
            InitializeComponent();

            _multipleSignalAnalyzerControl = new MultipleSignalAnalyzerControl();
            _multipleSignalAnalyzerControl.Dock = DockStyle.Fill;
            this.Controls.Add(_multipleSignalAnalyzerControl);

            _singleSignalAnalyzerControl = new SingleSignalAnalyzerControl();
            _singleSignalAnalyzerControl.Dock = DockStyle.Fill;
            this.Controls.Add(_singleSignalAnalyzerControl);

            multipleSignalsToolStripMenuItem.Checked = false;
            _multipleSignalAnalyzerControl.Hide();

            singleSignalToolStripMenuItem.Checked = true;
            _singleSignalAnalyzerControl.Show();

            var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            var versionString = $"v{version.Major}.{version.Minor}.{version.Build}";
            toolStripStatusLabel1.Text = versionString;
            statusStrip1.Refresh();
        }

        private void multipleSignalsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _singleSignalAnalyzerControl.Hide();
            _multipleSignalAnalyzerControl.Show();
            singleSignalToolStripMenuItem.Checked = false;
        }

        private void singleSignalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _multipleSignalAnalyzerControl.Hide();
            _singleSignalAnalyzerControl.Show();
            multipleSignalsToolStripMenuItem.Checked = false;
        }
    }
}
