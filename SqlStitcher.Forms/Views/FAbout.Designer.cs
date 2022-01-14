namespace SqlStitcher.Forms.Views
{
    partial class FAbout
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
            this._versionLabel = new System.Windows.Forms.Label();
            this._version = new System.Windows.Forms.Label();
            this._okButton = new System.Windows.Forms.Button();
            this._name = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // _versionLabel
            // 
            this._versionLabel.AutoSize = true;
            this._versionLabel.Location = new System.Drawing.Point(24, 29);
            this._versionLabel.Name = "_versionLabel";
            this._versionLabel.Size = new System.Drawing.Size(45, 13);
            this._versionLabel.TabIndex = 0;
            this._versionLabel.Text = "Version:";
            // 
            // _version
            // 
            this._version.AutoSize = true;
            this._version.Location = new System.Drawing.Point(66, 29);
            this._version.Name = "_version";
            this._version.Size = new System.Drawing.Size(22, 13);
            this._version.TabIndex = 1;
            this._version.Text = "0.0";
            // 
            // _okButton
            // 
            this._okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._okButton.Location = new System.Drawing.Point(265, 54);
            this._okButton.Margin = new System.Windows.Forms.Padding(8);
            this._okButton.Name = "_okButton";
            this._okButton.Size = new System.Drawing.Size(75, 23);
            this._okButton.TabIndex = 2;
            this._okButton.Text = "OK";
            this._okButton.UseVisualStyleBackColor = true;
            // 
            // _name
            // 
            this._name.AutoSize = true;
            this._name.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._name.Location = new System.Drawing.Point(13, 9);
            this._name.Name = "_name";
            this._name.Size = new System.Drawing.Size(88, 17);
            this._name.TabIndex = 3;
            this._name.Text = "SQL Stitcher";
            // 
            // FAbout
            // 
            this.AcceptButton = this._okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(357, 94);
            this.Controls.Add(this._name);
            this.Controls.Add(this._okButton);
            this.Controls.Add(this._version);
            this.Controls.Add(this._versionLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FAbout";
            this.Text = "About SQL Stitcher";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label _versionLabel;
        private System.Windows.Forms.Label _version;
        private System.Windows.Forms.Button _okButton;
        private System.Windows.Forms.Label _name;
    }
}