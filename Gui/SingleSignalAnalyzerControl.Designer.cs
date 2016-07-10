namespace Gui
{
    partial class SingleSignalAnalyzerControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.testString = new System.Windows.Forms.TextBox();
            this.testStringLabel = new System.Windows.Forms.Label();
            this.playAudio = new System.Windows.Forms.CheckBox();
            this.writeWavFiles = new System.Windows.Forms.CheckBox();
            this.baudRate = new System.Windows.Forms.TextBox();
            this.baudRateLabel = new System.Windows.Forms.Label();
            this.audioLengthMicrosecondsLabel = new System.Windows.Forms.Label();
            this.audioLengthMicroseconds = new System.Windows.Forms.TextBox();
            this.numberOfSymbolsLabel = new System.Windows.Forms.Label();
            this.numberOfSymbols = new System.Windows.Forms.TextBox();
            this.toleranceLabel = new System.Windows.Forms.Label();
            this.tolerance = new System.Windows.Forms.TextBox();
            this.markFrequencyLabel = new System.Windows.Forms.Label();
            this.spaceFrequencyLabel = new System.Windows.Forms.Label();
            this.spaceFrequency = new System.Windows.Forms.TextBox();
            this.markFrequency = new System.Windows.Forms.TextBox();
            this.startButton = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.resultString = new System.Windows.Forms.TextBox();
            this.resultStringLabel = new System.Windows.Forms.Label();
            this.matchLabel = new System.Windows.Forms.Label();
            this.zoomLabel = new System.Windows.Forms.Label();
            this.zoom = new System.Windows.Forms.TrackBar();
            this.scopeControl1 = new GuiControls.ScopeControl();
            this.numberOfFrequencyDifferencesLabel = new System.Windows.Forms.Label();
            this.numberOfFrequencyDifferences = new System.Windows.Forms.TextBox();
            this.numberOfZeroFrequenciesLabel = new System.Windows.Forms.Label();
            this.numberOfZeroFrequencies = new System.Windows.Forms.TextBox();
            this.averageFrequencyDifferenceLabel = new System.Windows.Forms.Label();
            this.averageFrequencyDifference = new System.Windows.Forms.TextBox();
            this.minimumFrequencyDifferenceLabel = new System.Windows.Forms.Label();
            this.minimumFrequencyDifference = new System.Windows.Forms.TextBox();
            this.maximumFrequencyDifferenceLabel = new System.Windows.Forms.Label();
            this.maximumFrequencyDifference = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.openFileButton = new System.Windows.Forms.Button();
            this.radioGenerate = new System.Windows.Forms.RadioButton();
            this.radioExistingFile = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.generationPanel = new System.Windows.Forms.Panel();
            this.existingFilePanel = new System.Windows.Forms.Panel();
            this.filenameTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.zoom)).BeginInit();
            this.panel1.SuspendLayout();
            this.generationPanel.SuspendLayout();
            this.existingFilePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // testString
            // 
            this.testString.Location = new System.Drawing.Point(74, 12);
            this.testString.Name = "testString";
            this.testString.Size = new System.Drawing.Size(164, 20);
            this.testString.TabIndex = 6;
            this.testString.Text = "A";
            this.testString.Enter += new System.EventHandler(this.testString_Enter);
            this.testString.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textboxKeyUp);
            // 
            // testStringLabel
            // 
            this.testStringLabel.AutoSize = true;
            this.testStringLabel.Location = new System.Drawing.Point(6, 15);
            this.testStringLabel.Name = "testStringLabel";
            this.testStringLabel.Size = new System.Drawing.Size(56, 13);
            this.testStringLabel.TabIndex = 135;
            this.testStringLabel.Text = "Test string";
            // 
            // playAudio
            // 
            this.playAudio.AutoSize = true;
            this.playAudio.Location = new System.Drawing.Point(635, 573);
            this.playAudio.Name = "playAudio";
            this.playAudio.Size = new System.Drawing.Size(75, 17);
            this.playAudio.TabIndex = 8;
            this.playAudio.Text = "Play audio";
            this.playAudio.UseVisualStyleBackColor = true;
            // 
            // writeWavFiles
            // 
            this.writeWavFiles.AutoSize = true;
            this.writeWavFiles.Location = new System.Drawing.Point(250, 14);
            this.writeWavFiles.Name = "writeWavFiles";
            this.writeWavFiles.Size = new System.Drawing.Size(95, 17);
            this.writeWavFiles.TabIndex = 7;
            this.writeWavFiles.Text = "Write WAV file";
            this.writeWavFiles.UseVisualStyleBackColor = true;
            // 
            // baudRate
            // 
            this.baudRate.Location = new System.Drawing.Point(168, 484);
            this.baudRate.MaxLength = 5;
            this.baudRate.Name = "baudRate";
            this.baudRate.Size = new System.Drawing.Size(44, 20);
            this.baudRate.TabIndex = 4;
            this.baudRate.Text = "500";
            this.baudRate.Enter += new System.EventHandler(this.baudRate_Enter);
            this.baudRate.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textboxKeyUp);
            // 
            // baudRateLabel
            // 
            this.baudRateLabel.AutoSize = true;
            this.baudRateLabel.Location = new System.Drawing.Point(100, 487);
            this.baudRateLabel.Name = "baudRateLabel";
            this.baudRateLabel.Size = new System.Drawing.Size(53, 13);
            this.baudRateLabel.TabIndex = 134;
            this.baudRateLabel.Text = "Baud rate";
            // 
            // audioLengthMicrosecondsLabel
            // 
            this.audioLengthMicrosecondsLabel.AutoSize = true;
            this.audioLengthMicrosecondsLabel.Enabled = false;
            this.audioLengthMicrosecondsLabel.Location = new System.Drawing.Point(503, 449);
            this.audioLengthMicrosecondsLabel.Name = "audioLengthMicrosecondsLabel";
            this.audioLengthMicrosecondsLabel.Size = new System.Drawing.Size(83, 13);
            this.audioLengthMicrosecondsLabel.TabIndex = 133;
            this.audioLengthMicrosecondsLabel.Text = "Symbol len in μs";
            // 
            // audioLengthMicroseconds
            // 
            this.audioLengthMicroseconds.Enabled = false;
            this.audioLengthMicroseconds.Location = new System.Drawing.Point(592, 446);
            this.audioLengthMicroseconds.Name = "audioLengthMicroseconds";
            this.audioLengthMicroseconds.ReadOnly = true;
            this.audioLengthMicroseconds.Size = new System.Drawing.Size(65, 20);
            this.audioLengthMicroseconds.TabIndex = 132;
            this.audioLengthMicroseconds.TabStop = false;
            this.audioLengthMicroseconds.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // numberOfSymbolsLabel
            // 
            this.numberOfSymbolsLabel.AutoSize = true;
            this.numberOfSymbolsLabel.Enabled = false;
            this.numberOfSymbolsLabel.Location = new System.Drawing.Point(354, 449);
            this.numberOfSymbolsLabel.Name = "numberOfSymbolsLabel";
            this.numberOfSymbolsLabel.Size = new System.Drawing.Size(54, 13);
            this.numberOfSymbolsLabel.TabIndex = 131;
            this.numberOfSymbolsLabel.Text = "# symbols";
            // 
            // numberOfSymbols
            // 
            this.numberOfSymbols.Enabled = false;
            this.numberOfSymbols.Location = new System.Drawing.Point(414, 446);
            this.numberOfSymbols.Name = "numberOfSymbols";
            this.numberOfSymbols.ReadOnly = true;
            this.numberOfSymbols.Size = new System.Drawing.Size(65, 20);
            this.numberOfSymbols.TabIndex = 130;
            this.numberOfSymbols.TabStop = false;
            this.numberOfSymbols.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // toleranceLabel
            // 
            this.toleranceLabel.AutoSize = true;
            this.toleranceLabel.Location = new System.Drawing.Point(227, 487);
            this.toleranceLabel.Name = "toleranceLabel";
            this.toleranceLabel.Size = new System.Drawing.Size(55, 13);
            this.toleranceLabel.TabIndex = 129;
            this.toleranceLabel.Text = "Tolerance";
            // 
            // tolerance
            // 
            this.tolerance.Location = new System.Drawing.Point(286, 484);
            this.tolerance.MaxLength = 5;
            this.tolerance.Name = "tolerance";
            this.tolerance.Size = new System.Drawing.Size(44, 20);
            this.tolerance.TabIndex = 3;
            this.tolerance.Text = "20";
            this.tolerance.Enter += new System.EventHandler(this.tolerance_Enter);
            this.tolerance.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textboxKeyUp);
            // 
            // markFrequencyLabel
            // 
            this.markFrequencyLabel.AutoSize = true;
            this.markFrequencyLabel.Location = new System.Drawing.Point(227, 449);
            this.markFrequencyLabel.Name = "markFrequencyLabel";
            this.markFrequencyLabel.Size = new System.Drawing.Size(55, 13);
            this.markFrequencyLabel.TabIndex = 127;
            this.markFrequencyLabel.Text = "Mark freq.";
            // 
            // spaceFrequencyLabel
            // 
            this.spaceFrequencyLabel.AutoSize = true;
            this.spaceFrequencyLabel.Location = new System.Drawing.Point(100, 449);
            this.spaceFrequencyLabel.Name = "spaceFrequencyLabel";
            this.spaceFrequencyLabel.Size = new System.Drawing.Size(62, 13);
            this.spaceFrequencyLabel.TabIndex = 128;
            this.spaceFrequencyLabel.Text = "Space freq.";
            // 
            // spaceFrequency
            // 
            this.spaceFrequency.Location = new System.Drawing.Point(168, 446);
            this.spaceFrequency.MaxLength = 5;
            this.spaceFrequency.Name = "spaceFrequency";
            this.spaceFrequency.Size = new System.Drawing.Size(44, 20);
            this.spaceFrequency.TabIndex = 1;
            this.spaceFrequency.Text = "1000";
            this.spaceFrequency.Enter += new System.EventHandler(this.spaceFrequency_Enter);
            this.spaceFrequency.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textboxKeyUp);
            // 
            // markFrequency
            // 
            this.markFrequency.Location = new System.Drawing.Point(286, 446);
            this.markFrequency.MaxLength = 5;
            this.markFrequency.Name = "markFrequency";
            this.markFrequency.Size = new System.Drawing.Size(44, 20);
            this.markFrequency.TabIndex = 2;
            this.markFrequency.Text = "3000";
            this.markFrequency.Enter += new System.EventHandler(this.markFrequency_Enter);
            this.markFrequency.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textboxKeyUp);
            // 
            // startButton
            // 
            this.startButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.startButton.Location = new System.Drawing.Point(728, 555);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(111, 51);
            this.startButton.TabIndex = 9;
            this.startButton.Text = "Analyze";
            this.startButton.UseVisualStyleBackColor = false;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // resultString
            // 
            this.resultString.BackColor = System.Drawing.SystemColors.Control;
            this.resultString.Enabled = false;
            this.resultString.Location = new System.Drawing.Point(168, 414);
            this.resultString.Name = "resultString";
            this.resultString.ReadOnly = true;
            this.resultString.Size = new System.Drawing.Size(164, 20);
            this.resultString.TabIndex = 137;
            this.resultString.TabStop = false;
            // 
            // resultStringLabel
            // 
            this.resultStringLabel.AutoSize = true;
            this.resultStringLabel.Enabled = false;
            this.resultStringLabel.Location = new System.Drawing.Point(100, 417);
            this.resultStringLabel.Name = "resultStringLabel";
            this.resultStringLabel.Size = new System.Drawing.Size(65, 13);
            this.resultStringLabel.TabIndex = 138;
            this.resultStringLabel.Text = "Result string";
            // 
            // matchLabel
            // 
            this.matchLabel.AutoSize = true;
            this.matchLabel.Enabled = false;
            this.matchLabel.Location = new System.Drawing.Point(338, 417);
            this.matchLabel.Name = "matchLabel";
            this.matchLabel.Size = new System.Drawing.Size(62, 13);
            this.matchLabel.TabIndex = 139;
            this.matchLabel.Text = "Match label";
            // 
            // zoomLabel
            // 
            this.zoomLabel.AutoSize = true;
            this.zoomLabel.Location = new System.Drawing.Point(895, 35);
            this.zoomLabel.Name = "zoomLabel";
            this.zoomLabel.Size = new System.Drawing.Size(34, 13);
            this.zoomLabel.TabIndex = 142;
            this.zoomLabel.Text = "Zoom";
            // 
            // zoom
            // 
            this.zoom.LargeChange = 25;
            this.zoom.Location = new System.Drawing.Point(892, 50);
            this.zoom.Maximum = 300;
            this.zoom.Minimum = 100;
            this.zoom.Name = "zoom";
            this.zoom.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.zoom.Size = new System.Drawing.Size(45, 324);
            this.zoom.SmallChange = 5;
            this.zoom.TabIndex = 10;
            this.zoom.TickFrequency = 25;
            this.zoom.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.zoom.Value = 100;
            this.zoom.Scroll += new System.EventHandler(this.zoom_ValueChanged);
            // 
            // scopeControl1
            // 
            this.scopeControl1.AutoScroll = true;
            this.scopeControl1.Location = new System.Drawing.Point(32, 28);
            this.scopeControl1.Name = "scopeControl1";
            this.scopeControl1.Size = new System.Drawing.Size(839, 346);
            this.scopeControl1.TabIndex = 140;
            // 
            // numberOfFrequencyDifferencesLabel
            // 
            this.numberOfFrequencyDifferencesLabel.AutoSize = true;
            this.numberOfFrequencyDifferencesLabel.Enabled = false;
            this.numberOfFrequencyDifferencesLabel.Location = new System.Drawing.Point(354, 487);
            this.numberOfFrequencyDifferencesLabel.Name = "numberOfFrequencyDifferencesLabel";
            this.numberOfFrequencyDifferencesLabel.Size = new System.Drawing.Size(52, 13);
            this.numberOfFrequencyDifferencesLabel.TabIndex = 144;
            this.numberOfFrequencyDifferencesLabel.Text = "# freq diff";
            // 
            // numberOfFrequencyDifferences
            // 
            this.numberOfFrequencyDifferences.Enabled = false;
            this.numberOfFrequencyDifferences.Location = new System.Drawing.Point(414, 484);
            this.numberOfFrequencyDifferences.Name = "numberOfFrequencyDifferences";
            this.numberOfFrequencyDifferences.ReadOnly = true;
            this.numberOfFrequencyDifferences.Size = new System.Drawing.Size(65, 20);
            this.numberOfFrequencyDifferences.TabIndex = 143;
            this.numberOfFrequencyDifferences.TabStop = false;
            this.numberOfFrequencyDifferences.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // numberOfZeroFrequenciesLabel
            // 
            this.numberOfZeroFrequenciesLabel.AutoSize = true;
            this.numberOfZeroFrequenciesLabel.Enabled = false;
            this.numberOfZeroFrequenciesLabel.Location = new System.Drawing.Point(503, 487);
            this.numberOfZeroFrequenciesLabel.Name = "numberOfZeroFrequenciesLabel";
            this.numberOfZeroFrequenciesLabel.Size = new System.Drawing.Size(63, 13);
            this.numberOfZeroFrequenciesLabel.TabIndex = 146;
            this.numberOfZeroFrequenciesLabel.Text = "# zero freqs";
            // 
            // numberOfZeroFrequencies
            // 
            this.numberOfZeroFrequencies.Enabled = false;
            this.numberOfZeroFrequencies.Location = new System.Drawing.Point(592, 484);
            this.numberOfZeroFrequencies.Name = "numberOfZeroFrequencies";
            this.numberOfZeroFrequencies.ReadOnly = true;
            this.numberOfZeroFrequencies.Size = new System.Drawing.Size(65, 20);
            this.numberOfZeroFrequencies.TabIndex = 145;
            this.numberOfZeroFrequencies.TabStop = false;
            this.numberOfZeroFrequencies.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // averageFrequencyDifferenceLabel
            // 
            this.averageFrequencyDifferenceLabel.AutoSize = true;
            this.averageFrequencyDifferenceLabel.Enabled = false;
            this.averageFrequencyDifferenceLabel.Location = new System.Drawing.Point(685, 449);
            this.averageFrequencyDifferenceLabel.Name = "averageFrequencyDifferenceLabel";
            this.averageFrequencyDifferenceLabel.Size = new System.Drawing.Size(64, 13);
            this.averageFrequencyDifferenceLabel.TabIndex = 148;
            this.averageFrequencyDifferenceLabel.Text = "Avg freq diff";
            // 
            // averageFrequencyDifference
            // 
            this.averageFrequencyDifference.Enabled = false;
            this.averageFrequencyDifference.Location = new System.Drawing.Point(774, 446);
            this.averageFrequencyDifference.Name = "averageFrequencyDifference";
            this.averageFrequencyDifference.ReadOnly = true;
            this.averageFrequencyDifference.Size = new System.Drawing.Size(65, 20);
            this.averageFrequencyDifference.TabIndex = 147;
            this.averageFrequencyDifference.TabStop = false;
            this.averageFrequencyDifference.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // minimumFrequencyDifferenceLabel
            // 
            this.minimumFrequencyDifferenceLabel.AutoSize = true;
            this.minimumFrequencyDifferenceLabel.Enabled = false;
            this.minimumFrequencyDifferenceLabel.Location = new System.Drawing.Point(685, 487);
            this.minimumFrequencyDifferenceLabel.Name = "minimumFrequencyDifferenceLabel";
            this.minimumFrequencyDifferenceLabel.Size = new System.Drawing.Size(62, 13);
            this.minimumFrequencyDifferenceLabel.TabIndex = 150;
            this.minimumFrequencyDifferenceLabel.Text = "Min freq diff";
            // 
            // minimumFrequencyDifference
            // 
            this.minimumFrequencyDifference.Enabled = false;
            this.minimumFrequencyDifference.Location = new System.Drawing.Point(774, 484);
            this.minimumFrequencyDifference.Name = "minimumFrequencyDifference";
            this.minimumFrequencyDifference.ReadOnly = true;
            this.minimumFrequencyDifference.Size = new System.Drawing.Size(65, 20);
            this.minimumFrequencyDifference.TabIndex = 149;
            this.minimumFrequencyDifference.TabStop = false;
            this.minimumFrequencyDifference.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // maximumFrequencyDifferenceLabel
            // 
            this.maximumFrequencyDifferenceLabel.AutoSize = true;
            this.maximumFrequencyDifferenceLabel.Enabled = false;
            this.maximumFrequencyDifferenceLabel.Location = new System.Drawing.Point(685, 524);
            this.maximumFrequencyDifferenceLabel.Name = "maximumFrequencyDifferenceLabel";
            this.maximumFrequencyDifferenceLabel.Size = new System.Drawing.Size(65, 13);
            this.maximumFrequencyDifferenceLabel.TabIndex = 152;
            this.maximumFrequencyDifferenceLabel.Text = "Max freq diff";
            // 
            // maximumFrequencyDifference
            // 
            this.maximumFrequencyDifference.Enabled = false;
            this.maximumFrequencyDifference.Location = new System.Drawing.Point(774, 521);
            this.maximumFrequencyDifference.Name = "maximumFrequencyDifference";
            this.maximumFrequencyDifference.ReadOnly = true;
            this.maximumFrequencyDifference.Size = new System.Drawing.Size(65, 20);
            this.maximumFrequencyDifference.TabIndex = 151;
            this.maximumFrequencyDifference.TabStop = false;
            this.maximumFrequencyDifference.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "WAV Files (*.wav)|*.wav";
            // 
            // openFileButton
            // 
            this.openFileButton.Location = new System.Drawing.Point(3, 4);
            this.openFileButton.Name = "openFileButton";
            this.openFileButton.Size = new System.Drawing.Size(82, 23);
            this.openFileButton.TabIndex = 153;
            this.openFileButton.Text = "Browse";
            this.openFileButton.UseVisualStyleBackColor = true;
            this.openFileButton.Click += new System.EventHandler(this.openFileButton_Click);
            // 
            // radioGenerate
            // 
            this.radioGenerate.AutoSize = true;
            this.radioGenerate.Checked = true;
            this.radioGenerate.Location = new System.Drawing.Point(8, 8);
            this.radioGenerate.Name = "radioGenerate";
            this.radioGenerate.Size = new System.Drawing.Size(99, 17);
            this.radioGenerate.TabIndex = 154;
            this.radioGenerate.TabStop = true;
            this.radioGenerate.Text = "Generate signal";
            this.radioGenerate.UseVisualStyleBackColor = true;
            this.radioGenerate.Click += new System.EventHandler(this.radioGenerate_Click);
            // 
            // radioExistingFile
            // 
            this.radioExistingFile.AutoSize = true;
            this.radioExistingFile.Location = new System.Drawing.Point(112, 8);
            this.radioExistingFile.Name = "radioExistingFile";
            this.radioExistingFile.Size = new System.Drawing.Size(110, 17);
            this.radioExistingFile.TabIndex = 155;
            this.radioExistingFile.Text = "Use existing WAV";
            this.radioExistingFile.UseVisualStyleBackColor = true;
            this.radioExistingFile.Click += new System.EventHandler(this.radioExistingFile_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radioExistingFile);
            this.panel1.Controls.Add(this.radioGenerate);
            this.panel1.Location = new System.Drawing.Point(96, 378);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(224, 31);
            this.panel1.TabIndex = 156;
            // 
            // generationPanel
            // 
            this.generationPanel.Controls.Add(this.testString);
            this.generationPanel.Controls.Add(this.writeWavFiles);
            this.generationPanel.Controls.Add(this.testStringLabel);
            this.generationPanel.Location = new System.Drawing.Point(324, 372);
            this.generationPanel.Name = "generationPanel";
            this.generationPanel.Size = new System.Drawing.Size(354, 38);
            this.generationPanel.TabIndex = 157;
            // 
            // existingFilePanel
            // 
            this.existingFilePanel.Controls.Add(this.filenameTextBox);
            this.existingFilePanel.Controls.Add(this.openFileButton);
            this.existingFilePanel.Location = new System.Drawing.Point(324, 378);
            this.existingFilePanel.Name = "existingFilePanel";
            this.existingFilePanel.Size = new System.Drawing.Size(547, 32);
            this.existingFilePanel.TabIndex = 158;
            this.existingFilePanel.Visible = false;
            // 
            // filenameTextBox
            // 
            this.filenameTextBox.Enabled = false;
            this.filenameTextBox.Location = new System.Drawing.Point(91, 5);
            this.filenameTextBox.Name = "filenameTextBox";
            this.filenameTextBox.ReadOnly = true;
            this.filenameTextBox.Size = new System.Drawing.Size(438, 20);
            this.filenameTextBox.TabIndex = 159;
            this.filenameTextBox.TabStop = false;
            this.filenameTextBox.Text = "File name";
            // 
            // SingleSignalAnalyzerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.generationPanel);
            this.Controls.Add(this.existingFilePanel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.maximumFrequencyDifferenceLabel);
            this.Controls.Add(this.maximumFrequencyDifference);
            this.Controls.Add(this.minimumFrequencyDifferenceLabel);
            this.Controls.Add(this.minimumFrequencyDifference);
            this.Controls.Add(this.averageFrequencyDifferenceLabel);
            this.Controls.Add(this.averageFrequencyDifference);
            this.Controls.Add(this.numberOfZeroFrequenciesLabel);
            this.Controls.Add(this.numberOfZeroFrequencies);
            this.Controls.Add(this.numberOfFrequencyDifferencesLabel);
            this.Controls.Add(this.numberOfFrequencyDifferences);
            this.Controls.Add(this.zoom);
            this.Controls.Add(this.zoomLabel);
            this.Controls.Add(this.matchLabel);
            this.Controls.Add(this.resultString);
            this.Controls.Add(this.resultStringLabel);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.playAudio);
            this.Controls.Add(this.baudRate);
            this.Controls.Add(this.baudRateLabel);
            this.Controls.Add(this.audioLengthMicrosecondsLabel);
            this.Controls.Add(this.audioLengthMicroseconds);
            this.Controls.Add(this.numberOfSymbolsLabel);
            this.Controls.Add(this.numberOfSymbols);
            this.Controls.Add(this.toleranceLabel);
            this.Controls.Add(this.tolerance);
            this.Controls.Add(this.markFrequencyLabel);
            this.Controls.Add(this.spaceFrequencyLabel);
            this.Controls.Add(this.spaceFrequency);
            this.Controls.Add(this.markFrequency);
            this.Controls.Add(this.scopeControl1);
            this.Name = "SingleSignalAnalyzerControl";
            this.Size = new System.Drawing.Size(943, 629);
            this.Load += new System.EventHandler(this.SingleSignalAnalyzerControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.zoom)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.generationPanel.ResumeLayout(false);
            this.generationPanel.PerformLayout();
            this.existingFilePanel.ResumeLayout(false);
            this.existingFilePanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox testString;
        private System.Windows.Forms.Label testStringLabel;
        private System.Windows.Forms.CheckBox playAudio;
        private System.Windows.Forms.CheckBox writeWavFiles;
        private System.Windows.Forms.TextBox baudRate;
        private System.Windows.Forms.Label baudRateLabel;
        private System.Windows.Forms.Label audioLengthMicrosecondsLabel;
        private System.Windows.Forms.TextBox audioLengthMicroseconds;
        private System.Windows.Forms.Label numberOfSymbolsLabel;
        private System.Windows.Forms.TextBox numberOfSymbols;
        private System.Windows.Forms.Label toleranceLabel;
        private System.Windows.Forms.TextBox tolerance;
        private System.Windows.Forms.Label markFrequencyLabel;
        private System.Windows.Forms.Label spaceFrequencyLabel;
        private System.Windows.Forms.TextBox spaceFrequency;
        private System.Windows.Forms.TextBox markFrequency;
        private System.Windows.Forms.Button startButton;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TextBox resultString;
        private System.Windows.Forms.Label resultStringLabel;
        private System.Windows.Forms.Label matchLabel;
        private GuiControls.ScopeControl scopeControl1;
        private System.Windows.Forms.Label zoomLabel;
        private System.Windows.Forms.TrackBar zoom;
        private System.Windows.Forms.Label numberOfFrequencyDifferencesLabel;
        private System.Windows.Forms.TextBox numberOfFrequencyDifferences;
        private System.Windows.Forms.Label numberOfZeroFrequenciesLabel;
        private System.Windows.Forms.TextBox numberOfZeroFrequencies;
        private System.Windows.Forms.Label averageFrequencyDifferenceLabel;
        private System.Windows.Forms.TextBox averageFrequencyDifference;
        private System.Windows.Forms.Label minimumFrequencyDifferenceLabel;
        private System.Windows.Forms.TextBox minimumFrequencyDifference;
        private System.Windows.Forms.Label maximumFrequencyDifferenceLabel;
        private System.Windows.Forms.TextBox maximumFrequencyDifference;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button openFileButton;
        private System.Windows.Forms.RadioButton radioGenerate;
        private System.Windows.Forms.RadioButton radioExistingFile;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel generationPanel;
        private System.Windows.Forms.Panel existingFilePanel;
        private System.Windows.Forms.TextBox filenameTextBox;
    }
}
