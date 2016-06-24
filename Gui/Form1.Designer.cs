namespace Gui
{
    partial class Form1
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.markFrequency = new System.Windows.Forms.TextBox();
            this.baudRate = new System.Windows.Forms.TextBox();
            this.spaceFrequency = new System.Windows.Forms.TextBox();
            this.baudRateLabel = new System.Windows.Forms.Label();
            this.spaceFrequencyLabel = new System.Windows.Forms.Label();
            this.markFrequencyLabel = new System.Windows.Forms.Label();
            this.boostStartLabel = new System.Windows.Forms.Label();
            this.boostStart = new System.Windows.Forms.TextBox();
            this.boostIncrementLabel = new System.Windows.Forms.Label();
            this.boostIncrement = new System.Windows.Forms.TextBox();
            this.boostEndLabel = new System.Windows.Forms.Label();
            this.boostEnd = new System.Windows.Forms.TextBox();
            this.startButton = new System.Windows.Forms.Button();
            this.mainDataGrid = new System.Windows.Forms.DataGridView();
            this.exportToCsvButton = new System.Windows.Forms.Button();
            this.toleranceLabel = new System.Windows.Forms.Label();
            this.tolerance = new System.Windows.Forms.TextBox();
            this.numberOfBitsLabel = new System.Windows.Forms.Label();
            this.numberOfBits = new System.Windows.Forms.TextBox();
            this.audioLengthMillisecondsLabel = new System.Windows.Forms.Label();
            this.audioLengthMilliseconds = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.mainDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // markFrequency
            // 
            this.markFrequency.Location = new System.Drawing.Point(283, 383);
            this.markFrequency.Name = "markFrequency";
            this.markFrequency.Size = new System.Drawing.Size(100, 20);
            this.markFrequency.TabIndex = 2;
            this.markFrequency.Text = "1270";
            // 
            // baudRate
            // 
            this.baudRate.Location = new System.Drawing.Point(480, 383);
            this.baudRate.Name = "baudRate";
            this.baudRate.Size = new System.Drawing.Size(100, 20);
            this.baudRate.TabIndex = 3;
            this.baudRate.Text = "50";
            // 
            // spaceFrequency
            // 
            this.spaceFrequency.Location = new System.Drawing.Point(80, 383);
            this.spaceFrequency.Name = "spaceFrequency";
            this.spaceFrequency.Size = new System.Drawing.Size(100, 20);
            this.spaceFrequency.TabIndex = 1;
            this.spaceFrequency.Text = "1070";
            // 
            // baudRateLabel
            // 
            this.baudRateLabel.AutoSize = true;
            this.baudRateLabel.Location = new System.Drawing.Point(421, 386);
            this.baudRateLabel.Name = "baudRateLabel";
            this.baudRateLabel.Size = new System.Drawing.Size(53, 13);
            this.baudRateLabel.TabIndex = 99;
            this.baudRateLabel.Text = "Baud rate";
            // 
            // spaceFrequencyLabel
            // 
            this.spaceFrequencyLabel.AutoSize = true;
            this.spaceFrequencyLabel.Location = new System.Drawing.Point(12, 386);
            this.spaceFrequencyLabel.Name = "spaceFrequencyLabel";
            this.spaceFrequencyLabel.Size = new System.Drawing.Size(62, 13);
            this.spaceFrequencyLabel.TabIndex = 99;
            this.spaceFrequencyLabel.Text = "Space freq.";
            // 
            // markFrequencyLabel
            // 
            this.markFrequencyLabel.AutoSize = true;
            this.markFrequencyLabel.Location = new System.Drawing.Point(222, 386);
            this.markFrequencyLabel.Name = "markFrequencyLabel";
            this.markFrequencyLabel.Size = new System.Drawing.Size(55, 13);
            this.markFrequencyLabel.TabIndex = 99;
            this.markFrequencyLabel.Text = "Mark freq.";
            // 
            // boostStartLabel
            // 
            this.boostStartLabel.AutoSize = true;
            this.boostStartLabel.Location = new System.Drawing.Point(12, 424);
            this.boostStartLabel.Name = "boostStartLabel";
            this.boostStartLabel.Size = new System.Drawing.Size(57, 13);
            this.boostStartLabel.TabIndex = 99;
            this.boostStartLabel.Text = "Boost start";
            // 
            // boostStart
            // 
            this.boostStart.Location = new System.Drawing.Point(80, 421);
            this.boostStart.Name = "boostStart";
            this.boostStart.Size = new System.Drawing.Size(100, 20);
            this.boostStart.TabIndex = 4;
            this.boostStart.Text = "0";
            // 
            // boostIncrementLabel
            // 
            this.boostIncrementLabel.AutoSize = true;
            this.boostIncrementLabel.Location = new System.Drawing.Point(222, 424);
            this.boostIncrementLabel.Name = "boostIncrementLabel";
            this.boostIncrementLabel.Size = new System.Drawing.Size(54, 13);
            this.boostIncrementLabel.TabIndex = 99;
            this.boostIncrementLabel.Text = "Boost inc.";
            // 
            // boostIncrement
            // 
            this.boostIncrement.Location = new System.Drawing.Point(283, 421);
            this.boostIncrement.Name = "boostIncrement";
            this.boostIncrement.Size = new System.Drawing.Size(100, 20);
            this.boostIncrement.TabIndex = 5;
            this.boostIncrement.Text = "100";
            // 
            // boostEndLabel
            // 
            this.boostEndLabel.AutoSize = true;
            this.boostEndLabel.Location = new System.Drawing.Point(421, 424);
            this.boostEndLabel.Name = "boostEndLabel";
            this.boostEndLabel.Size = new System.Drawing.Size(55, 13);
            this.boostEndLabel.TabIndex = 99;
            this.boostEndLabel.Text = "Boost end";
            // 
            // boostEnd
            // 
            this.boostEnd.Location = new System.Drawing.Point(480, 421);
            this.boostEnd.Name = "boostEnd";
            this.boostEnd.Size = new System.Drawing.Size(100, 20);
            this.boostEnd.TabIndex = 6;
            this.boostEnd.Text = "5000";
            // 
            // startButton
            // 
            this.startButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.startButton.Location = new System.Drawing.Point(853, 386);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(111, 51);
            this.startButton.TabIndex = 8;
            this.startButton.Text = "Analyze";
            this.startButton.UseVisualStyleBackColor = false;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // mainDataGrid
            // 
            this.mainDataGrid.AllowUserToAddRows = false;
            this.mainDataGrid.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.mainDataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.mainDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.mainDataGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.mainDataGrid.Location = new System.Drawing.Point(12, 12);
            this.mainDataGrid.Name = "mainDataGrid";
            this.mainDataGrid.ReadOnly = true;
            this.mainDataGrid.RowHeadersVisible = false;
            this.mainDataGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.mainDataGrid.Size = new System.Drawing.Size(820, 350);
            this.mainDataGrid.TabIndex = 100;
            this.mainDataGrid.TabStop = false;
            this.mainDataGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.mainDataGrid_CellContentClick);
            // 
            // exportToCsvButton
            // 
            this.exportToCsvButton.Enabled = false;
            this.exportToCsvButton.Location = new System.Drawing.Point(853, 311);
            this.exportToCsvButton.Name = "exportToCsvButton";
            this.exportToCsvButton.Size = new System.Drawing.Size(111, 51);
            this.exportToCsvButton.TabIndex = 9;
            this.exportToCsvButton.Text = "Export to CSV";
            this.exportToCsvButton.UseVisualStyleBackColor = true;
            this.exportToCsvButton.Click += new System.EventHandler(this.exportToCsvButton_Click);
            // 
            // toleranceLabel
            // 
            this.toleranceLabel.AutoSize = true;
            this.toleranceLabel.Location = new System.Drawing.Point(619, 386);
            this.toleranceLabel.Name = "toleranceLabel";
            this.toleranceLabel.Size = new System.Drawing.Size(55, 13);
            this.toleranceLabel.TabIndex = 103;
            this.toleranceLabel.Text = "Tolerance";
            // 
            // tolerance
            // 
            this.tolerance.Location = new System.Drawing.Point(678, 383);
            this.tolerance.Name = "tolerance";
            this.tolerance.Size = new System.Drawing.Size(100, 20);
            this.tolerance.TabIndex = 7;
            this.tolerance.Text = "25";
            // 
            // numberOfBitsLabel
            // 
            this.numberOfBitsLabel.AutoSize = true;
            this.numberOfBitsLabel.Enabled = false;
            this.numberOfBitsLabel.Location = new System.Drawing.Point(872, 26);
            this.numberOfBitsLabel.Name = "numberOfBitsLabel";
            this.numberOfBitsLabel.Size = new System.Drawing.Size(75, 13);
            this.numberOfBitsLabel.TabIndex = 105;
            this.numberOfBitsLabel.Text = "Number of bits";
            // 
            // numberOfBits
            // 
            this.numberOfBits.Enabled = false;
            this.numberOfBits.Location = new System.Drawing.Point(861, 42);
            this.numberOfBits.Name = "numberOfBits";
            this.numberOfBits.ReadOnly = true;
            this.numberOfBits.Size = new System.Drawing.Size(100, 20);
            this.numberOfBits.TabIndex = 104;
            this.numberOfBits.TabStop = false;
            this.numberOfBits.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // audioLengthMillisecondsLabel
            // 
            this.audioLengthMillisecondsLabel.AutoSize = true;
            this.audioLengthMillisecondsLabel.Enabled = false;
            this.audioLengthMillisecondsLabel.Location = new System.Drawing.Point(863, 80);
            this.audioLengthMillisecondsLabel.Name = "audioLengthMillisecondsLabel";
            this.audioLengthMillisecondsLabel.Size = new System.Drawing.Size(96, 13);
            this.audioLengthMillisecondsLabel.TabIndex = 107;
            this.audioLengthMillisecondsLabel.Text = "Interval length (ms)";
            // 
            // audioLengthMilliseconds
            // 
            this.audioLengthMilliseconds.Enabled = false;
            this.audioLengthMilliseconds.Location = new System.Drawing.Point(861, 96);
            this.audioLengthMilliseconds.Name = "audioLengthMilliseconds";
            this.audioLengthMilliseconds.ReadOnly = true;
            this.audioLengthMilliseconds.Size = new System.Drawing.Size(100, 20);
            this.audioLengthMilliseconds.TabIndex = 106;
            this.audioLengthMilliseconds.TabStop = false;
            this.audioLengthMilliseconds.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 461);
            this.Controls.Add(this.audioLengthMillisecondsLabel);
            this.Controls.Add(this.audioLengthMilliseconds);
            this.Controls.Add(this.numberOfBitsLabel);
            this.Controls.Add(this.numberOfBits);
            this.Controls.Add(this.toleranceLabel);
            this.Controls.Add(this.tolerance);
            this.Controls.Add(this.exportToCsvButton);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.boostEnd);
            this.Controls.Add(this.boostEndLabel);
            this.Controls.Add(this.boostIncrement);
            this.Controls.Add(this.boostIncrementLabel);
            this.Controls.Add(this.boostStart);
            this.Controls.Add(this.boostStartLabel);
            this.Controls.Add(this.markFrequencyLabel);
            this.Controls.Add(this.spaceFrequencyLabel);
            this.Controls.Add(this.baudRateLabel);
            this.Controls.Add(this.spaceFrequency);
            this.Controls.Add(this.baudRate);
            this.Controls.Add(this.markFrequency);
            this.Controls.Add(this.mainDataGrid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Signal Analyzer";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.mainDataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.TextBox markFrequency;
        private System.Windows.Forms.TextBox baudRate;
        private System.Windows.Forms.TextBox spaceFrequency;
        private System.Windows.Forms.Label baudRateLabel;
        private System.Windows.Forms.Label spaceFrequencyLabel;
        private System.Windows.Forms.Label markFrequencyLabel;
        private System.Windows.Forms.Label boostStartLabel;
        private System.Windows.Forms.TextBox boostStart;
        private System.Windows.Forms.Label boostIncrementLabel;
        private System.Windows.Forms.TextBox boostIncrement;
        private System.Windows.Forms.Label boostEndLabel;
        private System.Windows.Forms.TextBox boostEnd;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.DataGridView mainDataGrid;
        private System.Windows.Forms.Button exportToCsvButton;
        private System.Windows.Forms.Label toleranceLabel;
        private System.Windows.Forms.TextBox tolerance;
        private System.Windows.Forms.Label numberOfBitsLabel;
        private System.Windows.Forms.TextBox numberOfBits;
        private System.Windows.Forms.Label audioLengthMillisecondsLabel;
        private System.Windows.Forms.TextBox audioLengthMilliseconds;
    }
}

