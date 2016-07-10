namespace Gui
{
    partial class SignalAnalyzerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SignalAnalyzerForm));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.analyzeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.multipleSignalsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.singleSignalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.multipleSignalAnalyzerControl1 = new Gui.MultipleSignalAnalyzerControl();
            this.singleSignalAnalyzerControl1 = new Gui.SingleSignalAnalyzerControl();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 662);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1023, 22);
            this.statusStrip1.TabIndex = 109;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(287, 17);
            this.toolStripStatusLabel2.Text = "http://www.GitHub.com/dandandrea/SignalAnalyzer";
            this.toolStripStatusLabel2.VisitedLinkColor = System.Drawing.Color.Blue;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.analyzeToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1023, 24);
            this.menuStrip1.TabIndex = 110;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // analyzeToolStripMenuItem
            // 
            this.analyzeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.multipleSignalsToolStripMenuItem,
            this.singleSignalToolStripMenuItem});
            this.analyzeToolStripMenuItem.Name = "analyzeToolStripMenuItem";
            this.analyzeToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.analyzeToolStripMenuItem.Text = "Mode";
            // 
            // multipleSignalsToolStripMenuItem
            // 
            this.multipleSignalsToolStripMenuItem.CheckOnClick = true;
            this.multipleSignalsToolStripMenuItem.Name = "multipleSignalsToolStripMenuItem";
            this.multipleSignalsToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.multipleSignalsToolStripMenuItem.Text = "Multiple signals";
            this.multipleSignalsToolStripMenuItem.Click += new System.EventHandler(this.multipleSignalsToolStripMenuItem_Click);
            // 
            // singleSignalToolStripMenuItem
            // 
            this.singleSignalToolStripMenuItem.CheckOnClick = true;
            this.singleSignalToolStripMenuItem.Name = "singleSignalToolStripMenuItem";
            this.singleSignalToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.singleSignalToolStripMenuItem.Text = "Single signal";
            this.singleSignalToolStripMenuItem.Click += new System.EventHandler(this.singleSignalToolStripMenuItem_Click);
            // 
            // multipleSignalAnalyzerControl1
            // 
            this.multipleSignalAnalyzerControl1.Location = new System.Drawing.Point(44, 26);
            this.multipleSignalAnalyzerControl1.Name = "multipleSignalAnalyzerControl1";
            this.multipleSignalAnalyzerControl1.Size = new System.Drawing.Size(935, 571);
            this.multipleSignalAnalyzerControl1.TabIndex = 112;
            // 
            // singleSignalAnalyzerControl1
            // 
            this.singleSignalAnalyzerControl1.Location = new System.Drawing.Point(37, 24);
            this.singleSignalAnalyzerControl1.Name = "singleSignalAnalyzerControl1";
            this.singleSignalAnalyzerControl1.Size = new System.Drawing.Size(949, 620);
            this.singleSignalAnalyzerControl1.TabIndex = 111;
            // 
            // SignalAnalyzerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1023, 684);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.multipleSignalAnalyzerControl1);
            this.Controls.Add(this.singleSignalAnalyzerControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "SignalAnalyzerForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Signal Analyzer";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem analyzeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem multipleSignalsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem singleSignalToolStripMenuItem;
        private SingleSignalAnalyzerControl singleSignalAnalyzerControl1;
        private MultipleSignalAnalyzerControl multipleSignalAnalyzerControl1;
    }
}