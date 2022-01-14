namespace SqlStitcher.Forms.Views
{
    partial class OptionsGeneral
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
            this._recentFilesListLengthLabel = new System.Windows.Forms.Label();
            this._recentFilesListLength = new System.Windows.Forms.ComboBox();
            this._projectsRootLabel = new System.Windows.Forms.Label();
            this._projectsRoot = new System.Windows.Forms.TextBox();
            this._projectsRootBrowse = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _recentFilesListLengthLabel
            // 
            this._recentFilesListLengthLabel.AutoSize = true;
            this._recentFilesListLengthLabel.Location = new System.Drawing.Point(16, 11);
            this._recentFilesListLengthLabel.Name = "_recentFilesListLengthLabel";
            this._recentFilesListLengthLabel.Size = new System.Drawing.Size(119, 13);
            this._recentFilesListLengthLabel.TabIndex = 0;
            this._recentFilesListLengthLabel.Text = "Recent Projects Length";
            // 
            // _recentFilesListLength
            // 
            this._recentFilesListLength.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._recentFilesListLength.FormattingEnabled = true;
            this._recentFilesListLength.Items.AddRange(new object[] {
            "5",
            "10",
            "15",
            "20",
            "25"});
            this._recentFilesListLength.Location = new System.Drawing.Point(141, 8);
            this._recentFilesListLength.Name = "_recentFilesListLength";
            this._recentFilesListLength.Size = new System.Drawing.Size(44, 21);
            this._recentFilesListLength.TabIndex = 1;
            // 
            // _projectsRootLabel
            // 
            this._projectsRootLabel.AutoSize = true;
            this._projectsRootLabel.Location = new System.Drawing.Point(8, 38);
            this._projectsRootLabel.Name = "_projectsRootLabel";
            this._projectsRootLabel.Size = new System.Drawing.Size(127, 13);
            this._projectsRootLabel.TabIndex = 2;
            this._projectsRootLabel.Text = "Default Projects Directory";
            // 
            // _projectsRoot
            // 
            this._projectsRoot.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._projectsRoot.Location = new System.Drawing.Point(141, 35);
            this._projectsRoot.Name = "_projectsRoot";
            this._projectsRoot.Size = new System.Drawing.Size(334, 20);
            this._projectsRoot.TabIndex = 3;
            // 
            // _projectsRootBrowse
            // 
            this._projectsRootBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._projectsRootBrowse.Location = new System.Drawing.Point(481, 33);
            this._projectsRootBrowse.Name = "_projectsRootBrowse";
            this._projectsRootBrowse.Size = new System.Drawing.Size(29, 23);
            this._projectsRootBrowse.TabIndex = 4;
            this._projectsRootBrowse.Text = "...";
            this._projectsRootBrowse.UseVisualStyleBackColor = true;
            this._projectsRootBrowse.Click += new System.EventHandler(this._projectsRootBrowse_Click);
            // 
            // OptionsGeneral
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._projectsRootBrowse);
            this.Controls.Add(this._projectsRoot);
            this.Controls.Add(this._projectsRootLabel);
            this.Controls.Add(this._recentFilesListLength);
            this.Controls.Add(this._recentFilesListLengthLabel);
            this.Name = "OptionsGeneral";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Size = new System.Drawing.Size(518, 422);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label _recentFilesListLengthLabel;
        private System.Windows.Forms.ComboBox _recentFilesListLength;
        private System.Windows.Forms.Label _projectsRootLabel;
        private System.Windows.Forms.TextBox _projectsRoot;
        private System.Windows.Forms.Button _projectsRootBrowse;
    }
}
