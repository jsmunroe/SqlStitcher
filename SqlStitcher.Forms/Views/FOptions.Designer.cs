namespace SqlStitcher.Forms.Views
{
    partial class FOptions
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
            this._okButton = new System.Windows.Forms.Button();
            this._cancel = new System.Windows.Forms.Button();
            this._pages = new System.Windows.Forms.ListBox();
            this._page = new System.Windows.Forms.Panel();
            this._errorMessage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // _okButton
            // 
            this._okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._okButton.Location = new System.Drawing.Point(351, 321);
            this._okButton.Margin = new System.Windows.Forms.Padding(8);
            this._okButton.Name = "_okButton";
            this._okButton.Size = new System.Drawing.Size(75, 23);
            this._okButton.TabIndex = 5;
            this._okButton.Text = "Save";
            this._okButton.UseVisualStyleBackColor = true;
            this._okButton.Click += new System.EventHandler(this._okButton_Click);
            // 
            // _cancel
            // 
            this._cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._cancel.Location = new System.Drawing.Point(442, 321);
            this._cancel.Margin = new System.Windows.Forms.Padding(8);
            this._cancel.Name = "_cancel";
            this._cancel.Size = new System.Drawing.Size(75, 23);
            this._cancel.TabIndex = 4;
            this._cancel.Text = "Cancel";
            this._cancel.UseVisualStyleBackColor = true;
            // 
            // _pages
            // 
            this._pages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this._pages.FormattingEnabled = true;
            this._pages.Items.AddRange(new object[] {
            "General",
            "Stitching"});
            this._pages.Location = new System.Drawing.Point(12, 12);
            this._pages.Name = "_pages";
            this._pages.Size = new System.Drawing.Size(113, 290);
            this._pages.TabIndex = 6;
            this._pages.SelectedIndexChanged += new System.EventHandler(this._pages_SelectedIndexChanged);
            // 
            // _page
            // 
            this._page.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._page.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._page.Location = new System.Drawing.Point(131, 12);
            this._page.Name = "_page";
            this._page.Size = new System.Drawing.Size(391, 292);
            this._page.TabIndex = 7;
            // 
            // _errorMessage
            // 
            this._errorMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._errorMessage.AutoSize = true;
            this._errorMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._errorMessage.ForeColor = System.Drawing.Color.DarkRed;
            this._errorMessage.Location = new System.Drawing.Point(9, 307);
            this._errorMessage.Name = "_errorMessage";
            this._errorMessage.Size = new System.Drawing.Size(75, 13);
            this._errorMessage.TabIndex = 8;
            this._errorMessage.Text = "Error Message";
            // 
            // FOptions
            // 
            this.AcceptButton = this._okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._cancel;
            this.ClientSize = new System.Drawing.Size(534, 361);
            this.Controls.Add(this._errorMessage);
            this.Controls.Add(this._page);
            this.Controls.Add(this._pages);
            this.Controls.Add(this._okButton);
            this.Controls.Add(this._cancel);
            this.MinimumSize = new System.Drawing.Size(550, 400);
            this.Name = "FOptions";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Options";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _okButton;
        private System.Windows.Forms.Button _cancel;
        private System.Windows.Forms.ListBox _pages;
        private System.Windows.Forms.Panel _page;
        private System.Windows.Forms.Label _errorMessage;
    }
}