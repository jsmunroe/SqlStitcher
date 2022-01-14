namespace SqlStitcher.Forms.Views
{
    partial class FNewProject
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
            this._cancel = new System.Windows.Forms.Button();
            this._okButton = new System.Windows.Forms.Button();
            this._projectRootLabel = new System.Windows.Forms.Label();
            this._projectRootText = new System.Windows.Forms.TextBox();
            this._projectRootBrowse = new System.Windows.Forms.Button();
            this._errorMessage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // _cancel
            // 
            this._cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._cancel.Location = new System.Drawing.Point(542, 71);
            this._cancel.Margin = new System.Windows.Forms.Padding(8);
            this._cancel.Name = "_cancel";
            this._cancel.Size = new System.Drawing.Size(75, 23);
            this._cancel.TabIndex = 0;
            this._cancel.Text = "Cancel";
            this._cancel.UseVisualStyleBackColor = true;
            // 
            // _okButton
            // 
            this._okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._okButton.Location = new System.Drawing.Point(451, 71);
            this._okButton.Margin = new System.Windows.Forms.Padding(8);
            this._okButton.Name = "_okButton";
            this._okButton.Size = new System.Drawing.Size(75, 23);
            this._okButton.TabIndex = 1;
            this._okButton.Text = "Create";
            this._okButton.UseVisualStyleBackColor = true;
            this._okButton.Click += new System.EventHandler(this._okButton_Click);
            // 
            // _projectRootLabel
            // 
            this._projectRootLabel.AutoSize = true;
            this._projectRootLabel.Location = new System.Drawing.Point(12, 9);
            this._projectRootLabel.Name = "_projectRootLabel";
            this._projectRootLabel.Size = new System.Drawing.Size(30, 13);
            this._projectRootLabel.TabIndex = 2;
            this._projectRootLabel.Text = "Root";
            // 
            // _projectRootText
            // 
            this._projectRootText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._projectRootText.Location = new System.Drawing.Point(48, 6);
            this._projectRootText.Name = "_projectRootText";
            this._projectRootText.Size = new System.Drawing.Size(538, 20);
            this._projectRootText.TabIndex = 3;
            // 
            // _projectRootBrowse
            // 
            this._projectRootBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._projectRootBrowse.Location = new System.Drawing.Point(592, 4);
            this._projectRootBrowse.Name = "_projectRootBrowse";
            this._projectRootBrowse.Size = new System.Drawing.Size(25, 23);
            this._projectRootBrowse.TabIndex = 4;
            this._projectRootBrowse.Text = "...";
            this._projectRootBrowse.UseVisualStyleBackColor = true;
            this._projectRootBrowse.Click += new System.EventHandler(this._projectRootBrowse_Click);
            // 
            // _errorMessage
            // 
            this._errorMessage.AutoSize = true;
            this._errorMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._errorMessage.ForeColor = System.Drawing.Color.DarkRed;
            this._errorMessage.Location = new System.Drawing.Point(12, 39);
            this._errorMessage.Name = "_errorMessage";
            this._errorMessage.Size = new System.Drawing.Size(75, 13);
            this._errorMessage.TabIndex = 5;
            this._errorMessage.Text = "Error Message";
            // 
            // FNewProject
            // 
            this.AcceptButton = this._okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._cancel;
            this.ClientSize = new System.Drawing.Size(634, 111);
            this.Controls.Add(this._errorMessage);
            this.Controls.Add(this._projectRootBrowse);
            this.Controls.Add(this._projectRootText);
            this.Controls.Add(this._projectRootLabel);
            this.Controls.Add(this._okButton);
            this.Controls.Add(this._cancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MinimumSize = new System.Drawing.Size(400, 150);
            this.Name = "FNewProject";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "New Project";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _cancel;
        private System.Windows.Forms.Button _okButton;
        private System.Windows.Forms.Label _projectRootLabel;
        private System.Windows.Forms.TextBox _projectRootText;
        private System.Windows.Forms.Button _projectRootBrowse;
        private System.Windows.Forms.Label _errorMessage;
    }
}