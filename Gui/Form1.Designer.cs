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
            this.startButton.Location = new System.Drawing.Point(623, 386);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(111, 51);
            this.startButton.TabIndex = 7;
            this.startButton.Text = "Analyze";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // mainDataGrid
            // 
            this.mainDataGrid.AllowUserToAddRows = false;
            this.mainDataGrid.AllowUserToDeleteRows = false;
            this.mainDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.mainDataGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.mainDataGrid.Location = new System.Drawing.Point(12, 12);
            this.mainDataGrid.Name = "mainDataGrid";
            this.mainDataGrid.ReadOnly = true;
            this.mainDataGrid.RowHeadersVisible = false;
            this.mainDataGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.mainDataGrid.Size = new System.Drawing.Size(760, 350);
            this.mainDataGrid.TabIndex = 100;
            this.mainDataGrid.TabStop = false;
            this.mainDataGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.mainDataGrid_CellContentClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.mainDataGrid);
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
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
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
    }
}

