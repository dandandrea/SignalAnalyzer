namespace GuiControls
{
    partial class ScopeControl
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
            this.scopePictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.scopePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // scopePictureBox
            // 
            this.scopePictureBox.BackColor = System.Drawing.SystemColors.WindowText;
            this.scopePictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.scopePictureBox.Location = new System.Drawing.Point(0, 0);
            this.scopePictureBox.Margin = new System.Windows.Forms.Padding(0);
            this.scopePictureBox.Name = "scopePictureBox";
            this.scopePictureBox.Size = new System.Drawing.Size(943, 401);
            this.scopePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.scopePictureBox.TabIndex = 142;
            this.scopePictureBox.TabStop = false;
            // 
            // ScopeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.scopePictureBox);
            this.Name = "ScopeControl";
            this.Size = new System.Drawing.Size(944, 401);
            ((System.ComponentModel.ISupportInitialize)(this.scopePictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox scopePictureBox;
    }
}
