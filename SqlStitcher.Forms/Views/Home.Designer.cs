namespace SqlStitcher.Forms.Views
{
    partial class Home
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
            this._recentProjectsLabel = new System.Windows.Forms.Label();
            this._recentProjects = new System.Windows.Forms.Panel();
            this._linkPrototype = new System.Windows.Forms.LinkLabel();
            this._toolTip = new System.Windows.Forms.ToolTip(this.components);
            this._recentProjectsPanel = new System.Windows.Forms.Panel();
            this._recentProjects.SuspendLayout();
            this._recentProjectsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // _recentProjectsLabel
            // 
            this._recentProjectsLabel.AutoSize = true;
            this._recentProjectsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._recentProjectsLabel.Location = new System.Drawing.Point(22, 14);
            this._recentProjectsLabel.Name = "_recentProjectsLabel";
            this._recentProjectsLabel.Size = new System.Drawing.Size(108, 17);
            this._recentProjectsLabel.TabIndex = 0;
            this._recentProjectsLabel.Text = "Recent Projects";
            // 
            // _recentProjects
            // 
            this._recentProjects.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._recentProjects.AutoSize = true;
            this._recentProjects.Controls.Add(this._linkPrototype);
            this._recentProjects.Location = new System.Drawing.Point(18, 30);
            this._recentProjects.Name = "_recentProjects";
            this._recentProjects.Size = new System.Drawing.Size(142, 28);
            this._recentProjects.TabIndex = 1;
            // 
            // _linkPrototype
            // 
            this._linkPrototype.AutoSize = true;
            this._linkPrototype.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._linkPrototype.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this._linkPrototype.Location = new System.Drawing.Point(4, 4);
            this._linkPrototype.Name = "_linkPrototype";
            this._linkPrototype.Size = new System.Drawing.Size(99, 17);
            this._linkPrototype.TabIndex = 0;
            this._linkPrototype.TabStop = true;
            this._linkPrototype.Text = "Link Prototype";
            // 
            // _recentProjectsPanel
            // 
            this._recentProjectsPanel.AutoSize = true;
            this._recentProjectsPanel.BackColor = System.Drawing.SystemColors.Control;
            this._recentProjectsPanel.Controls.Add(this._recentProjectsLabel);
            this._recentProjectsPanel.Controls.Add(this._recentProjects);
            this._recentProjectsPanel.Location = new System.Drawing.Point(13, 13);
            this._recentProjectsPanel.Name = "_recentProjectsPanel";
            this._recentProjectsPanel.Padding = new System.Windows.Forms.Padding(15);
            this._recentProjectsPanel.Size = new System.Drawing.Size(200, 100);
            this._recentProjectsPanel.TabIndex = 2;
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.Controls.Add(this._recentProjectsPanel);
            this.DoubleBuffered = true;
            this.Name = "Home";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Size = new System.Drawing.Size(676, 474);
            this._recentProjects.ResumeLayout(false);
            this._recentProjects.PerformLayout();
            this._recentProjectsPanel.ResumeLayout(false);
            this._recentProjectsPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label _recentProjectsLabel;
        private System.Windows.Forms.Panel _recentProjects;
        private System.Windows.Forms.LinkLabel _linkPrototype;
        private System.Windows.Forms.ToolTip _toolTip;
        private System.Windows.Forms.Panel _recentProjectsPanel;
    }
}
