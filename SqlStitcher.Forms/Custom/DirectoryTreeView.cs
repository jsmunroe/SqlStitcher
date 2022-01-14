using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using SqlStitcher.Forms.Helpers;

namespace SqlStitcher.Forms.Custom
{
    public class DirectoryTreeView : TreeView
    {
        private FlaggedState _autoChecking = false;
        public event TreeViewEventHandler AfterCheckAll;

        public DirectoryTreeView()
        {
            DrawMode = TreeViewDrawMode.OwnerDrawText;

            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        public TreeNode[] CheckedNodes
        {
            get { return GetCheckedNodes(Nodes).ToArray(); }
        }

        public TreeNode[] AllNodes
        {
            get { return GetAllNodes(Nodes).ToArray(); }
        }

        public void RefreshNodes()
        {
            ShouldCheckNode(Nodes);
        }

        private bool ShouldCheckNode(TreeNodeCollection nodes)
        {
            var shouldCheckNode = true;

            foreach (TreeNode node in nodes)
            {
                if (node.Nodes.Count <= 0) // Node is leaf.
                {
                    if (!node.Checked)
                    {
                        shouldCheckNode = false;
                        continue;
                    }
                }

                if (ShouldCheckNode(node.Nodes))
                {
                    using (_autoChecking.IsTrueOver())
                    {
                        node.Checked = true;
                    }
                }
                else
                {
                    shouldCheckNode = false;
                }
            }

            return shouldCheckNode;
        }

        protected override void OnDrawNode(DrawTreeNodeEventArgs e)
        {
            base.OnDrawNode(e);

            e.DrawDefault = true;

            if (!e.Node.Checked && AnyCheckedBelow(e.Node))
            {
                var bounds = e.Bounds;
                bounds.Offset(-31, 1);

                CheckBoxRenderer.DrawCheckBox(e.Graphics, new Point(bounds.X, bounds.Y), CheckBoxState.MixedNormal);
            }
        }

        protected override void OnAfterCheck(TreeViewEventArgs e)
        {
            base.OnAfterCheck(e);

            if (_autoChecking)
                return;

            Enabled = false;

            CheckChildren(e.Node, e.Node.Checked);
            if (e.Node.Parent != null)
                CheckParents(e.Node.Parent);

            Enabled = true;

            Invalidate();

            if (AfterCheckAll != null) 
                AfterCheckAll.Invoke(this, new TreeViewEventArgs(e.Node));
        }


        private void CheckChildren(TreeNode node, bool check)
        {
            foreach (TreeNode childNode in node.Nodes)
            {
                using (_autoChecking.IsTrueOver())
                {
                    childNode.Checked = check;
                }

                CheckChildren(childNode, check);
            }
        }

        private void CheckParents(TreeNode node)
        {
            using (_autoChecking.IsTrueOver())
            {
                node.Checked = node.Nodes.OfType<TreeNode>().All(p => p.Checked);
            }

            if (node.Parent != null)
                CheckParents(node.Parent);
        }

        private bool AnyCheckedBelow(TreeNode node)
        {
            if (node.Checked)
                return true;

            return node.Nodes.OfType<TreeNode>().Any(AnyCheckedBelow);
        }

        private IEnumerable<TreeNode> GetVisibleNodes(TreeNodeCollection nodeCollection)
        {
            var nodes = new List<TreeNode>();

            foreach (TreeNode node in nodeCollection)
            {
                nodes.Add(node);

                if (node.IsExpanded)
                    nodes.AddRange(GetVisibleNodes(node.Nodes));
            }

            return nodes;
        }

        private IEnumerable<TreeNode> GetAllNodes(TreeNodeCollection nodeCollection)
        {
            var nodes = new List<TreeNode>();

            foreach (TreeNode node in nodeCollection)
            {
                nodes.Add(node);

                nodes.AddRange(GetCheckedNodes(node.Nodes));
            }

            return nodes;
        }

        private IEnumerable<TreeNode> GetCheckedNodes(TreeNodeCollection nodeCollection)
        {
            var nodes = new List<TreeNode>();

            foreach (TreeNode node in nodeCollection)
            {
                if (node.Checked)
                    nodes.Add(node);

                nodes.AddRange(GetCheckedNodes(node.Nodes));
            }

            return nodes;
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x203) // identified double click
            {
                var localPos = PointToClient(Cursor.Position);
                var hitTestInfo = HitTest(localPos);
                if (hitTestInfo.Location == TreeViewHitTestLocations.StateImage)
                    m.Result = IntPtr.Zero;
                else
                    base.WndProc(ref m);
            }
            else base.WndProc(ref m);
        }
    }
}
