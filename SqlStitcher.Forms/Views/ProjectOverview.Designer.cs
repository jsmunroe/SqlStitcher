namespace SqlStitcher.Forms.Views
{
    partial class ProjectOverview
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProjectOverview));
            this._icons = new System.Windows.Forms.ImageList(this.components);
            this._split = new System.Windows.Forms.SplitContainer();
            this._scriptTreeLabel = new System.Windows.Forms.Label();
            this._scriptBatchLabel = new System.Windows.Forms.Label();
            this._scriptBatchContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this._scriptBatchViewScript = new System.Windows.Forms.ToolStripMenuItem();
            this._scriptBatchCopyToClipboardMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this._scriptBatchRemove = new System.Windows.Forms.ToolStripMenuItem();
            this._scriptTree = new SqlStitcher.Forms.Custom.DirectoryTreeView();
            this._scriptBatch = new SqlStitcher.Forms.Custom.DraggableListView();
            this._scriptName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._scriptPath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ((System.ComponentModel.ISupportInitialize)(this._split)).BeginInit();
            this._split.Panel1.SuspendLayout();
            this._split.Panel2.SuspendLayout();
            this._split.SuspendLayout();
            this._scriptBatchContext.SuspendLayout();
            this.SuspendLayout();
            // 
            // _icons
            // 
            this._icons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("_icons.ImageStream")));
            this._icons.TransparentColor = System.Drawing.Color.Transparent;
            this._icons.Images.SetKeyName(0, "FolderClosed.png");
            this._icons.Images.SetKeyName(1, "FolderOpen.png");
            this._icons.Images.SetKeyName(2, "File.png");
            // 
            // _split
            // 
            this._split.Dock = System.Windows.Forms.DockStyle.Fill;
            this._split.Location = new System.Drawing.Point(0, 0);
            this._split.Name = "_split";
            // 
            // _split.Panel1
            // 
            this._split.Panel1.Controls.Add(this._scriptTreeLabel);
            this._split.Panel1.Controls.Add(this._scriptTree);
            this._split.Panel1MinSize = 300;
            // 
            // _split.Panel2
            // 
            this._split.Panel2.Controls.Add(this._scriptBatchLabel);
            this._split.Panel2.Controls.Add(this._scriptBatch);
            this._split.Size = new System.Drawing.Size(861, 446);
            this._split.SplitterDistance = 348;
            this._split.TabIndex = 2;
            // 
            // _scriptTreeLabel
            // 
            this._scriptTreeLabel.AutoSize = true;
            this._scriptTreeLabel.Location = new System.Drawing.Point(7, 8);
            this._scriptTreeLabel.Name = "_scriptTreeLabel";
            this._scriptTreeLabel.Size = new System.Drawing.Size(75, 13);
            this._scriptTreeLabel.TabIndex = 2;
            this._scriptTreeLabel.Text = "Project Scripts";
            // 
            // _scriptBatchLabel
            // 
            this._scriptBatchLabel.AutoSize = true;
            this._scriptBatchLabel.Location = new System.Drawing.Point(4, 8);
            this._scriptBatchLabel.Name = "_scriptBatchLabel";
            this._scriptBatchLabel.Size = new System.Drawing.Size(65, 13);
            this._scriptBatchLabel.TabIndex = 1;
            this._scriptBatchLabel.Text = "Script Batch";
            // 
            // _scriptBatchContext
            // 
            this._scriptBatchContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._scriptBatchViewScript,
            this._scriptBatchCopyToClipboardMenu,
            this.toolStripMenuItem1,
            this._scriptBatchRemove});
            this._scriptBatchContext.Name = "_scriptBatchContext";
            this._scriptBatchContext.Size = new System.Drawing.Size(223, 76);
            this._scriptBatchContext.Opening += new System.ComponentModel.CancelEventHandler(this._scriptBatchContext_Opening);
            // 
            // _scriptBatchViewScript
            // 
            this._scriptBatchViewScript.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._scriptBatchViewScript.Name = "_scriptBatchViewScript";
            this._scriptBatchViewScript.Size = new System.Drawing.Size(222, 22);
            this._scriptBatchViewScript.Text = "View &Script";
            this._scriptBatchViewScript.Click += new System.EventHandler(this._scriptBatchViewScript_Click);
            // 
            // _scriptBatchCopyToClipboardMenu
            // 
            this._scriptBatchCopyToClipboardMenu.Name = "_scriptBatchCopyToClipboardMenu";
            this._scriptBatchCopyToClipboardMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this._scriptBatchCopyToClipboardMenu.Size = new System.Drawing.Size(222, 22);
            this._scriptBatchCopyToClipboardMenu.Text = "&Copy to Clipboard...";
            this._scriptBatchCopyToClipboardMenu.Click += new System.EventHandler(this._scriptBatchCopyToClipboardMenu_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(219, 6);
            // 
            // _scriptBatchRemove
            // 
            this._scriptBatchRemove.Name = "_scriptBatchRemove";
            this._scriptBatchRemove.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this._scriptBatchRemove.Size = new System.Drawing.Size(222, 22);
            this._scriptBatchRemove.Text = "&Remove";
            this._scriptBatchRemove.Click += new System.EventHandler(this._scriptBatchRemove_Click);
            // 
            // _scriptTree
            // 
            this._scriptTree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._scriptTree.CheckBoxes = true;
            this._scriptTree.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
            this._scriptTree.ImageIndex = 0;
            this._scriptTree.ImageList = this._icons;
            this._scriptTree.Location = new System.Drawing.Point(10, 24);
            this._scriptTree.Margin = new System.Windows.Forms.Padding(0);
            this._scriptTree.Name = "_scriptTree";
            this._scriptTree.SelectedImageIndex = 0;
            this._scriptTree.Size = new System.Drawing.Size(335, 414);
            this._scriptTree.TabIndex = 1;
            this._scriptTree.AfterCheckAll += new System.Windows.Forms.TreeViewEventHandler(this._scriptTree_AfterCheckAll);
            // 
            // _scriptBatch
            // 
            this._scriptBatch.AllowDrop = true;
            this._scriptBatch.AllowRowReorder = true;
            this._scriptBatch.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._scriptBatch.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this._scriptName,
            this._scriptPath});
            this._scriptBatch.ContextMenuStrip = this._scriptBatchContext;
            this._scriptBatch.FullRowSelect = true;
            this._scriptBatch.Location = new System.Drawing.Point(3, 24);
            this._scriptBatch.Name = "_scriptBatch";
            this._scriptBatch.Size = new System.Drawing.Size(496, 414);
            this._scriptBatch.TabIndex = 0;
            this._scriptBatch.UseCompatibleStateImageBehavior = false;
            this._scriptBatch.View = System.Windows.Forms.View.Details;
            this._scriptBatch.ItemMoved += new SqlStitcher.Forms.Custom.ItemMovedEventHandler(this._scriptBatch_ItemMoved);
            this._scriptBatch.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this._scriptBatch_MouseDoubleClick);
            // 
            // _scriptName
            // 
            this._scriptName.Text = "Name";
            this._scriptName.Width = 200;
            // 
            // _scriptPath
            // 
            this._scriptPath.Text = "Path";
            this._scriptPath.Width = 400;
            // 
            // ProjectOverview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._split);
            this.Name = "ProjectOverview";
            this.Size = new System.Drawing.Size(861, 446);
            this._split.Panel1.ResumeLayout(false);
            this._split.Panel1.PerformLayout();
            this._split.Panel2.ResumeLayout(false);
            this._split.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._split)).EndInit();
            this._split.ResumeLayout(false);
            this._scriptBatchContext.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList _icons;
        private System.Windows.Forms.SplitContainer _split;
        private Custom.DirectoryTreeView _scriptTree;
        private SqlStitcher.Forms.Custom.DraggableListView _scriptBatch;
        private System.Windows.Forms.ColumnHeader _scriptName;
        private System.Windows.Forms.ColumnHeader _scriptPath;
        private System.Windows.Forms.Label _scriptTreeLabel;
        private System.Windows.Forms.Label _scriptBatchLabel;
        private System.Windows.Forms.ContextMenuStrip _scriptBatchContext;
        private System.Windows.Forms.ToolStripMenuItem _scriptBatchCopyToClipboardMenu;
        private System.Windows.Forms.ToolStripMenuItem _scriptBatchViewScript;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem _scriptBatchRemove;
    }
}
