using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Practices.ServiceLocation;
using SqlStitcher.Forms.Contracts;
using SqlStitcher.Forms.Custom;
using SqlStitcher.Forms.Helpers;
using SqlStitcher.Forms.ViewModels;
using SqlStitcher.Models;

namespace SqlStitcher.Forms.Views
{
    public partial class ProjectOverview : UserControl, IUserControl<ProjectOverviewViewModel>
    {
        private readonly FlaggedState _isUpdatingNodeCheck = false;

        public ProjectOverview(ProjectOverviewViewModel viewModel)
        {
            ViewModel = viewModel;

            InitializeComponent();

            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            PopulateScripts();

            Disposed += ProjectOverview_Disposed;
        }

        public ProjectOverviewViewModel ViewModel { get; set; }

        /// <summary>
        /// Populate the scripts from the view model into <see cref="_scriptTree"/>.
        /// </summary>
        private void PopulateScripts()
        {
            var treeRoot = ViewModel.BuildScriptTree();

            _scriptTree.Nodes.Clear();
            _scriptTree.Nodes.Add(CreateTreeNode(treeRoot));
            _scriptTree.RefreshNodes();
            ExpandToChecked(_scriptTree.Nodes);
            LoadBatchScripts();
        }

        private bool ExpandToChecked(TreeNodeCollection nodes)
        {
            var expand = false;

            foreach (TreeNode node in nodes)
            {
                if (ExpandToChecked(node.Nodes) || node.Checked)
                {
                    node.Expand();
                    expand = true;
                }
            }

            return expand;
        }

        private TreeNode CreateTreeNode(TreeNode<ScriptEntry> dataNode)
        {
            var node = new TreeNode
            {
                Text = dataNode.Description,
                Tag = dataNode.Value
            };

            if (node.Tag == null)
            {
                node.ImageIndex = 0;
                node.SelectedImageIndex = 0;
                node.StateImageIndex = 1;
            }
            else
            {
                node.Checked = dataNode.Value.IsSelected;
                node.ImageIndex = 2;
                node.SelectedImageIndex = 2;
            }

            node.Nodes.AddRange(dataNode.Children.Select(CreateTreeNode).ToArray());

            return node;
        }

        private void UpdateNodeCheck()
        {
            using (_isUpdatingNodeCheck.IsTrueOver())
            {
                var nodes = _scriptTree.AllNodes.Where(p => p.Tag is ScriptEntry);

                foreach (var node in nodes)
                {
                    var scriptEntry = (ScriptEntry) node.Tag;

                    node.Checked = scriptEntry.IsSelected;
                }
            }
        }

        private void UpdateNodeSelection(TreeNode node)
        {
            var scriptEntry = node.Tag as ScriptEntry;
            if (scriptEntry != null)
            {
                scriptEntry.IsSelected = node.Checked;
                ViewModel.UpdateEntrySelection(scriptEntry);
            }

            foreach (TreeNode childNode in node.Nodes)
            {
                UpdateNodeSelection(childNode);
            }
        }

        /// <summary>
        /// Get all of the script entries are selected within the batch.
        /// </summary>
        /// <returns>Script entries.</returns>
        private IEnumerable<ScriptEntry> GetSelectedBatchEntries()
        {
            return _scriptBatch.SelectedItems.OfType<ListViewItem>()
                                             .Select(p => p.Tag as ScriptEntry);
        }

        /// <summary>
        /// Load the batch scripts.
        /// </summary>
        private void LoadBatchScripts()
        {
            if (_isUpdatingNodeCheck) // Don't update if operation originated with script batch operation.
                return;

            _scriptBatch.Items.Clear();

            foreach (var entry in ViewModel.BatchEntries)
            {
                var listViewItem = new ListViewItem(entry.Name, 2)
                {
                    Tag = entry
                };

                listViewItem.SubItems.Add(entry.File.Path);
                _scriptBatch.Items.Add(listViewItem);
            }
        }

        private void RemoveEntities(IEnumerable<ScriptEntry> entries)
        {
            ViewModel.RemoveFromBatch(entries);
            LoadBatchScripts();
            UpdateNodeCheck();
        }

        private void _scriptTree_AfterCheckAll(object sender, TreeViewEventArgs e)
        {
            UpdateNodeSelection(e.Node);
            LoadBatchScripts();

            ViewModel.InvalidateProjectSave();
        }

        private void _scriptBatchCopyToClipboardMenu_Click(object sender, EventArgs e)
        {
            ViewModel.CopyToClipboard(GetSelectedBatchEntries());
        }

        private void _scriptBatchViewScript_Click(object sender, EventArgs e)
        {
            ViewModel.ViewScript(GetSelectedBatchEntries());
        }

        private void _scriptBatchRemove_Click(object sender, EventArgs e)
        {
            RemoveEntities(GetSelectedBatchEntries());
        }

        private void ProjectOverview_Disposed(object sender, EventArgs e)
        {
            ViewModel.Dispose();
        }

        private void _scriptBatchContext_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !GetSelectedBatchEntries().Any();
        }

        private void _scriptBatch_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (_scriptBatch.FocusedItem != null)
                ViewModel.ViewScript(new[] {_scriptBatch.FocusedItem.Tag as ScriptEntry});
        }

        private void _scriptBatch_ItemMoved(object sender, ItemMovedEventArgs e)
        {
            var entry = e.Item.Tag as ScriptEntry;

            if (entry == null)
                return;

            ViewModel.MoveInBatch(entry, e.Index);
        }
    }
}
