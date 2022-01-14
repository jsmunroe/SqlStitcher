namespace SqlStitcher.Forms.Views
{
    partial class FScript
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
            this._script = new System.Windows.Forms.TextBox();
            this._copyToClipboard = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _script
            // 
            this._script.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._script.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._script.Location = new System.Drawing.Point(5, 5);
            this._script.Multiline = true;
            this._script.Name = "_script";
            this._script.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this._script.Size = new System.Drawing.Size(617, 486);
            this._script.TabIndex = 0;
            this._script.WordWrap = false;
            this._script.TextChanged += new System.EventHandler(this._script_TextChanged);
            // 
            // _copyToClipboard
            // 
            this._copyToClipboard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._copyToClipboard.Location = new System.Drawing.Point(501, 497);
            this._copyToClipboard.Name = "_copyToClipboard";
            this._copyToClipboard.Size = new System.Drawing.Size(121, 23);
            this._copyToClipboard.TabIndex = 1;
            this._copyToClipboard.Text = "Copy to Clipboard";
            this._copyToClipboard.UseVisualStyleBackColor = true;
            this._copyToClipboard.Click += new System.EventHandler(this._sendToClipboard_Click);
            // 
            // FScript
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(626, 523);
            this.Controls.Add(this._copyToClipboard);
            this.Controls.Add(this._script);
            this.Name = "FScript";
            this.ShowIcon = false;
            this.Text = "Script Viewer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox _script;
        private System.Windows.Forms.Button _copyToClipboard;
    }
}