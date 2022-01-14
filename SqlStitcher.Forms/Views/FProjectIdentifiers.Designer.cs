namespace SqlStitcher.Forms.Views
{
    partial class FProjectIdentifiers
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
            this._identifiers = new System.Windows.Forms.Panel();
            this._okButton = new System.Windows.Forms.Button();
            this._cancel = new System.Windows.Forms.Button();
            this._revert = new System.Windows.Forms.Button();
            this._searchLabel = new System.Windows.Forms.Label();
            this._searchText = new System.Windows.Forms.TextBox();
            this._search = new System.Windows.Forms.Button();
            this._identifiersOuter = new System.Windows.Forms.Panel();
            this._identifiersOuter.SuspendLayout();
            this.SuspendLayout();
            // 
            // _identifiers
            // 
            this._identifiers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._identifiers.AutoSize = true;
            this._identifiers.BackColor = System.Drawing.SystemColors.Window;
            this._identifiers.Location = new System.Drawing.Point(8, 6);
            this._identifiers.Name = "_identifiers";
            this._identifiers.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this._identifiers.Size = new System.Drawing.Size(296, 102);
            this._identifiers.TabIndex = 0;
            // 
            // _okButton
            // 
            this._okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._okButton.Location = new System.Drawing.Point(155, 311);
            this._okButton.Margin = new System.Windows.Forms.Padding(8);
            this._okButton.Name = "_okButton";
            this._okButton.Size = new System.Drawing.Size(75, 23);
            this._okButton.TabIndex = 3;
            this._okButton.Text = "Save";
            this._okButton.UseVisualStyleBackColor = true;
            this._okButton.Click += new System.EventHandler(this._okButton_Click);
            // 
            // _cancel
            // 
            this._cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._cancel.Location = new System.Drawing.Point(246, 311);
            this._cancel.Margin = new System.Windows.Forms.Padding(8);
            this._cancel.Name = "_cancel";
            this._cancel.Size = new System.Drawing.Size(75, 23);
            this._cancel.TabIndex = 2;
            this._cancel.Text = "Cancel";
            this._cancel.UseVisualStyleBackColor = true;
            // 
            // _revert
            // 
            this._revert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._revert.Location = new System.Drawing.Point(17, 311);
            this._revert.Margin = new System.Windows.Forms.Padding(8);
            this._revert.Name = "_revert";
            this._revert.Size = new System.Drawing.Size(75, 23);
            this._revert.TabIndex = 4;
            this._revert.Text = "Revert All";
            this._revert.UseVisualStyleBackColor = true;
            this._revert.Click += new System.EventHandler(this._revert_Click);
            // 
            // _searchLabel
            // 
            this._searchLabel.AutoSize = true;
            this._searchLabel.Location = new System.Drawing.Point(12, 13);
            this._searchLabel.Name = "_searchLabel";
            this._searchLabel.Size = new System.Drawing.Size(44, 13);
            this._searchLabel.TabIndex = 5;
            this._searchLabel.Text = "Search:";
            // 
            // _searchText
            // 
            this._searchText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._searchText.Location = new System.Drawing.Point(62, 10);
            this._searchText.Name = "_searchText";
            this._searchText.Size = new System.Drawing.Size(224, 20);
            this._searchText.TabIndex = 6;
            // 
            // _search
            // 
            this._search.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._search.Location = new System.Drawing.Point(292, 10);
            this._search.Name = "_search";
            this._search.Size = new System.Drawing.Size(34, 20);
            this._search.TabIndex = 7;
            this._search.Text = "Go";
            this._search.UseVisualStyleBackColor = true;
            this._search.Click += new System.EventHandler(this._search_Click);
            // 
            // _identifiersOuter
            // 
            this._identifiersOuter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._identifiersOuter.AutoScroll = true;
            this._identifiersOuter.BackColor = System.Drawing.SystemColors.Window;
            this._identifiersOuter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._identifiersOuter.Controls.Add(this._identifiers);
            this._identifiersOuter.Location = new System.Drawing.Point(12, 36);
            this._identifiersOuter.Name = "_identifiersOuter";
            this._identifiersOuter.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this._identifiersOuter.Size = new System.Drawing.Size(314, 264);
            this._identifiersOuter.TabIndex = 1;
            // 
            // FProjectIdentifiers
            // 
            this.AcceptButton = this._okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._cancel;
            this.ClientSize = new System.Drawing.Size(338, 351);
            this.Controls.Add(this._identifiersOuter);
            this.Controls.Add(this._search);
            this.Controls.Add(this._searchText);
            this.Controls.Add(this._searchLabel);
            this.Controls.Add(this._revert);
            this.Controls.Add(this._okButton);
            this.Controls.Add(this._cancel);
            this.Name = "FProjectIdentifiers";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Project Identifiers";
            this._identifiersOuter.ResumeLayout(false);
            this._identifiersOuter.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel _identifiers;
        private System.Windows.Forms.Button _okButton;
        private System.Windows.Forms.Button _cancel;
        private System.Windows.Forms.Button _revert;
        private System.Windows.Forms.Label _searchLabel;
        private System.Windows.Forms.TextBox _searchText;
        private System.Windows.Forms.Button _search;
        private System.Windows.Forms.Panel _identifiersOuter;
    }
}