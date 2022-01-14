namespace SqlStitcher.Forms.Views
{
    partial class FMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FMain));
            this._mainMenu = new System.Windows.Forms.MenuStrip();
            this._projectMenu = new System.Windows.Forms.ToolStripMenuItem();
            this._projectNewMenu = new System.Windows.Forms.ToolStripMenuItem();
            this._projectOpenMenu = new System.Windows.Forms.ToolStripMenuItem();
            this._projectSep0 = new System.Windows.Forms.ToolStripSeparator();
            this._projectCloseMenu = new System.Windows.Forms.ToolStripMenuItem();
            this._projectSep1 = new System.Windows.Forms.ToolStripSeparator();
            this._projectSaveMenu = new System.Windows.Forms.ToolStripMenuItem();
            this._projectSaveAsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this._projectSep2 = new System.Windows.Forms.ToolStripSeparator();
            this._projectRecentMenu = new System.Windows.Forms.ToolStripMenuItem();
            this._projectRecentNoneMenu = new System.Windows.Forms.ToolStripMenuItem();
            this._projectSep3 = new System.Windows.Forms.ToolStripSeparator();
            this._exitMenu = new System.Windows.Forms.ToolStripMenuItem();
            this._scriptsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this._scriptsIdentifiersMenu = new System.Windows.Forms.ToolStripMenuItem();
            this._scriptsSep0 = new System.Windows.Forms.ToolStripSeparator();
            this._scriptsCopyToClipboardMenu = new System.Windows.Forms.ToolStripMenuItem();
            this._scriptsViewScriptMenu = new System.Windows.Forms.ToolStripMenuItem();
            this._toolsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this._toolsOptionsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this._helpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this._helpAboutMenu = new System.Windows.Forms.ToolStripMenuItem();
            this._mainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // _mainMenu
            // 
            this._mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._projectMenu,
            this._scriptsMenu,
            this._toolsMenu,
            this._helpMenu});
            this._mainMenu.Location = new System.Drawing.Point(0, 0);
            this._mainMenu.Name = "_mainMenu";
            this._mainMenu.Size = new System.Drawing.Size(933, 24);
            this._mainMenu.TabIndex = 0;
            this._mainMenu.Text = "menuStrip1";
            // 
            // _projectMenu
            // 
            this._projectMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._projectNewMenu,
            this._projectOpenMenu,
            this._projectSep0,
            this._projectCloseMenu,
            this._projectSep1,
            this._projectSaveMenu,
            this._projectSaveAsMenu,
            this._projectSep2,
            this._projectRecentMenu,
            this._projectSep3,
            this._exitMenu});
            this._projectMenu.Name = "_projectMenu";
            this._projectMenu.Size = new System.Drawing.Size(56, 20);
            this._projectMenu.Text = "&Project";
            // 
            // _projectNewMenu
            // 
            this._projectNewMenu.Name = "_projectNewMenu";
            this._projectNewMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this._projectNewMenu.Size = new System.Drawing.Size(195, 22);
            this._projectNewMenu.Text = "&New...";
            this._projectNewMenu.Click += new System.EventHandler(this._projectNewMenu_Click);
            // 
            // _projectOpenMenu
            // 
            this._projectOpenMenu.Name = "_projectOpenMenu";
            this._projectOpenMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this._projectOpenMenu.Size = new System.Drawing.Size(195, 22);
            this._projectOpenMenu.Text = "&Open...";
            this._projectOpenMenu.Click += new System.EventHandler(this._projectOpenMenu_Click);
            // 
            // _projectSep0
            // 
            this._projectSep0.Name = "_projectSep0";
            this._projectSep0.Size = new System.Drawing.Size(192, 6);
            // 
            // _projectCloseMenu
            // 
            this._projectCloseMenu.Enabled = false;
            this._projectCloseMenu.Name = "_projectCloseMenu";
            this._projectCloseMenu.Size = new System.Drawing.Size(195, 22);
            this._projectCloseMenu.Text = "&Close Project";
            this._projectCloseMenu.Click += new System.EventHandler(this._projectCloseMenu_Click);
            // 
            // _projectSep1
            // 
            this._projectSep1.Name = "_projectSep1";
            this._projectSep1.Size = new System.Drawing.Size(192, 6);
            // 
            // _projectSaveMenu
            // 
            this._projectSaveMenu.Enabled = false;
            this._projectSaveMenu.Name = "_projectSaveMenu";
            this._projectSaveMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this._projectSaveMenu.Size = new System.Drawing.Size(195, 22);
            this._projectSaveMenu.Text = "&Save...";
            this._projectSaveMenu.Click += new System.EventHandler(this._projectSaveMenu_Click);
            // 
            // _projectSaveAsMenu
            // 
            this._projectSaveAsMenu.Enabled = false;
            this._projectSaveAsMenu.Name = "_projectSaveAsMenu";
            this._projectSaveAsMenu.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this._projectSaveAsMenu.Size = new System.Drawing.Size(195, 22);
            this._projectSaveAsMenu.Text = "Save &As...";
            this._projectSaveAsMenu.Click += new System.EventHandler(this._projectSaveAsMenu_Click);
            // 
            // _projectSep2
            // 
            this._projectSep2.Name = "_projectSep2";
            this._projectSep2.Size = new System.Drawing.Size(192, 6);
            // 
            // _projectRecentMenu
            // 
            this._projectRecentMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._projectRecentNoneMenu});
            this._projectRecentMenu.Name = "_projectRecentMenu";
            this._projectRecentMenu.Size = new System.Drawing.Size(195, 22);
            this._projectRecentMenu.Text = "&Recent Projects";
            // 
            // _projectRecentNoneMenu
            // 
            this._projectRecentNoneMenu.Enabled = false;
            this._projectRecentNoneMenu.Name = "_projectRecentNoneMenu";
            this._projectRecentNoneMenu.Size = new System.Drawing.Size(103, 22);
            this._projectRecentNoneMenu.Text = "None";
            // 
            // _projectSep3
            // 
            this._projectSep3.Name = "_projectSep3";
            this._projectSep3.Size = new System.Drawing.Size(192, 6);
            // 
            // _exitMenu
            // 
            this._exitMenu.Name = "_exitMenu";
            this._exitMenu.Size = new System.Drawing.Size(195, 22);
            this._exitMenu.Text = "E&xit";
            this._exitMenu.Click += new System.EventHandler(this._exitMenu_Click);
            // 
            // _scriptsMenu
            // 
            this._scriptsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._scriptsIdentifiersMenu,
            this._scriptsSep0,
            this._scriptsCopyToClipboardMenu,
            this._scriptsViewScriptMenu});
            this._scriptsMenu.Name = "_scriptsMenu";
            this._scriptsMenu.Size = new System.Drawing.Size(54, 20);
            this._scriptsMenu.Text = "&Scripts";
            // 
            // _scriptsIdentifiersMenu
            // 
            this._scriptsIdentifiersMenu.Enabled = false;
            this._scriptsIdentifiersMenu.Name = "_scriptsIdentifiersMenu";
            this._scriptsIdentifiersMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this._scriptsIdentifiersMenu.Size = new System.Drawing.Size(186, 22);
            this._scriptsIdentifiersMenu.Text = "&Identifiers...";
            this._scriptsIdentifiersMenu.Click += new System.EventHandler(this._scriptsIdentifiersMenu_Click);
            // 
            // _scriptsSep0
            // 
            this._scriptsSep0.Name = "_scriptsSep0";
            this._scriptsSep0.Size = new System.Drawing.Size(183, 6);
            // 
            // _scriptsCopyToClipboardMenu
            // 
            this._scriptsCopyToClipboardMenu.Enabled = false;
            this._scriptsCopyToClipboardMenu.Name = "_scriptsCopyToClipboardMenu";
            this._scriptsCopyToClipboardMenu.Size = new System.Drawing.Size(186, 22);
            this._scriptsCopyToClipboardMenu.Text = "Copy to &Clipboard";
            this._scriptsCopyToClipboardMenu.Click += new System.EventHandler(this._scriptsSendToClipboardMenu_Click);
            // 
            // _scriptsViewScriptMenu
            // 
            this._scriptsViewScriptMenu.Enabled = false;
            this._scriptsViewScriptMenu.Name = "_scriptsViewScriptMenu";
            this._scriptsViewScriptMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
            this._scriptsViewScriptMenu.Size = new System.Drawing.Size(186, 22);
            this._scriptsViewScriptMenu.Text = "&View Script...";
            this._scriptsViewScriptMenu.Click += new System.EventHandler(this._scriptsViewScriptMenu_Click);
            // 
            // _toolsMenu
            // 
            this._toolsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._toolsOptionsMenu});
            this._toolsMenu.Name = "_toolsMenu";
            this._toolsMenu.Size = new System.Drawing.Size(47, 20);
            this._toolsMenu.Text = "&Tools";
            // 
            // _toolsOptionsMenu
            // 
            this._toolsOptionsMenu.Name = "_toolsOptionsMenu";
            this._toolsOptionsMenu.Size = new System.Drawing.Size(125, 22);
            this._toolsOptionsMenu.Text = "&Options...";
            this._toolsOptionsMenu.Click += new System.EventHandler(this._toolsOptionsMenu_Click);
            // 
            // _helpMenu
            // 
            this._helpMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._helpAboutMenu});
            this._helpMenu.Name = "_helpMenu";
            this._helpMenu.Size = new System.Drawing.Size(44, 20);
            this._helpMenu.Text = "&Help";
            // 
            // _helpAboutMenu
            // 
            this._helpAboutMenu.Name = "_helpAboutMenu";
            this._helpAboutMenu.Size = new System.Drawing.Size(116, 22);
            this._helpAboutMenu.Text = "&About...";
            this._helpAboutMenu.Click += new System.EventHandler(this._helpAboutMenu_Click);
            // 
            // FMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(933, 591);
            this.Controls.Add(this._mainMenu);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this._mainMenu;
            this.Name = "FMain";
            this.Text = "SQL Stitcher";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FMain_FormClosing);
            this._mainMenu.ResumeLayout(false);
            this._mainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip _mainMenu;
        private System.Windows.Forms.ToolStripMenuItem _projectMenu;
        private System.Windows.Forms.ToolStripMenuItem _projectNewMenu;
        private System.Windows.Forms.ToolStripSeparator _projectSep0;
        private System.Windows.Forms.ToolStripMenuItem _exitMenu;
        private System.Windows.Forms.ToolStripMenuItem _scriptsMenu;
        private System.Windows.Forms.ToolStripMenuItem _scriptsCopyToClipboardMenu;
        private System.Windows.Forms.ToolStripSeparator _scriptsSep0;
        private System.Windows.Forms.ToolStripMenuItem _scriptsIdentifiersMenu;
        private System.Windows.Forms.ToolStripMenuItem _scriptsViewScriptMenu;
        private System.Windows.Forms.ToolStripMenuItem _projectOpenMenu;
        private System.Windows.Forms.ToolStripMenuItem _projectSaveMenu;
        private System.Windows.Forms.ToolStripMenuItem _projectSaveAsMenu;
        private System.Windows.Forms.ToolStripSeparator _projectSep1;
        private System.Windows.Forms.ToolStripMenuItem _projectRecentMenu;
        private System.Windows.Forms.ToolStripMenuItem _projectRecentNoneMenu;
        private System.Windows.Forms.ToolStripSeparator _projectSep2;
        private System.Windows.Forms.ToolStripMenuItem _projectCloseMenu;
        private System.Windows.Forms.ToolStripSeparator _projectSep3;
        private System.Windows.Forms.ToolStripMenuItem _toolsMenu;
        private System.Windows.Forms.ToolStripMenuItem _toolsOptionsMenu;
        private System.Windows.Forms.ToolStripMenuItem _helpMenu;
        private System.Windows.Forms.ToolStripMenuItem _helpAboutMenu;

    }
}

