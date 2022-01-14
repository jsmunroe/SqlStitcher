namespace SqlStitcher.Forms.Views
{
    partial class Identifier
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
            this._identifierName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // _identifierName
            // 
            this._identifierName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._identifierName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._identifierName.Location = new System.Drawing.Point(3, 3);
            this._identifierName.Name = "_identifierName";
            this._identifierName.Size = new System.Drawing.Size(295, 13);
            this._identifierName.TabIndex = 0;
            this._identifierName.TextChanged += new System.EventHandler(this._identifierName_TextChanged);
            // 
            // Identifier
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this._identifierName);
            this.DoubleBuffered = true;
            this.Name = "Identifier";
            this.Size = new System.Drawing.Size(301, 20);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox _identifierName;
    }
}
