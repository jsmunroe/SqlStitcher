namespace SqlStitcher.Forms.Views
{
    partial class OptionsStitching
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
            this._insertGoCommand = new System.Windows.Forms.CheckBox();
            this._prependScriptCommentLabel = new System.Windows.Forms.Label();
            this._prependScriptComment = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // _insertGoCommand
            // 
            this._insertGoCommand.AutoSize = true;
            this._insertGoCommand.Location = new System.Drawing.Point(11, 40);
            this._insertGoCommand.Name = "_insertGoCommand";
            this._insertGoCommand.Size = new System.Drawing.Size(250, 17);
            this._insertGoCommand.TabIndex = 0;
            this._insertGoCommand.Text = "Insert a GO command at the end of each script.";
            this._insertGoCommand.UseVisualStyleBackColor = true;
            // 
            // _prependScriptCommentLabel
            // 
            this._prependScriptCommentLabel.AutoSize = true;
            this._prependScriptCommentLabel.Location = new System.Drawing.Point(8, 11);
            this._prependScriptCommentLabel.Name = "_prependScriptCommentLabel";
            this._prependScriptCommentLabel.Size = new System.Drawing.Size(137, 13);
            this._prependScriptCommentLabel.TabIndex = 3;
            this._prependScriptCommentLabel.Text = "Script Description Comment";
            // 
            // _prependScriptComment
            // 
            this._prependScriptComment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._prependScriptComment.FormattingEnabled = true;
            this._prependScriptComment.Location = new System.Drawing.Point(151, 8);
            this._prependScriptComment.Name = "_prependScriptComment";
            this._prependScriptComment.Size = new System.Drawing.Size(148, 21);
            this._prependScriptComment.TabIndex = 4;
            // 
            // OptionsStitching
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._prependScriptComment);
            this.Controls.Add(this._prependScriptCommentLabel);
            this.Controls.Add(this._insertGoCommand);
            this.Name = "OptionsStitching";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Size = new System.Drawing.Size(518, 422);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox _insertGoCommand;
        private System.Windows.Forms.Label _prependScriptCommentLabel;
        private System.Windows.Forms.ComboBox _prependScriptComment;
    }
}
