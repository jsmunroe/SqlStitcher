using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SqlStitcher.Forms.Custom
{
    public class DraggableListView : ListView
    {
        private const string Reorder = "Reorder";
        private bool _allowRowReorder = true;

        public event ItemMovedEventHandler ItemMoved;

        public bool AllowRowReorder
        {
            get
            {
                return _allowRowReorder;
            }
            set
            {
                _allowRowReorder = value;
                AllowDrop = value;
            }
        }

        public new SortOrder Sorting
        {
            get { return SortOrder.None; }
            set { base.Sorting = SortOrder.None; }
        }

        public DraggableListView()
        {
            AllowRowReorder = true;
        }

        protected override void OnDragDrop(DragEventArgs e)
        {
            base.OnDragDrop(e);
            if (!AllowRowReorder)
            {
                return;
            }
            if (SelectedItems.Count == 0)
            {
                return;
            }

            var cp = PointToClient(new Point(e.X, e.Y));
            var dragToItem = GetItemAt(cp.X, cp.Y);
            if (dragToItem == null)
            {
                return;
            }

            var dropIndex = dragToItem.Index;
            if (dropIndex > SelectedItems[0].Index)
            {
                dropIndex++;
            }

            var insertItems = new List<ListViewItem>(SelectedItems.Count);
            foreach (ListViewItem item in SelectedItems)
            {
                insertItems.Add((ListViewItem)item.Clone());
            }

            for (var i = insertItems.Count - 1; i >= 0; i--)
            {
                var insertItem = insertItems[i];
                Items.Insert(dropIndex, insertItem);

                if (ItemMoved != null)
                    ItemMoved.Invoke(this, new ItemMovedEventArgs(insertItem, dropIndex));
            }

            foreach (ListViewItem removeItem in SelectedItems)
            {
                Items.Remove(removeItem);
            }
        }

        protected override void OnDragOver(DragEventArgs e)
        {
            if (!AllowRowReorder)
            {
                e.Effect = DragDropEffects.None;
                return;
            }

            if (!e.Data.GetDataPresent(DataFormats.Text))
            {
                e.Effect = DragDropEffects.None;
                return;
            }

            var cp = PointToClient(new Point(e.X, e.Y));
            var hoverItem = GetItemAt(cp.X, cp.Y);
            if (hoverItem == null)
            {
                e.Effect = DragDropEffects.None;
                return;
            }

            foreach (ListViewItem moveItem in SelectedItems)
            {
                if (moveItem.Index == hoverItem.Index)
                {
                    e.Effect = DragDropEffects.None;
                    hoverItem.EnsureVisible();
                    return;
                }
            }

            base.OnDragOver(e);
            var text = (String)e.Data.GetData(Reorder.GetType());

            if (text == Reorder)
            {
                e.Effect = DragDropEffects.Move;
                hoverItem.EnsureVisible();
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        protected override void OnDragEnter(DragEventArgs e)
        {
            base.OnDragEnter(e);
            if (!AllowRowReorder)
            {
                e.Effect = DragDropEffects.None;
                return;
            }
            if (!e.Data.GetDataPresent(DataFormats.Text))
            {
                e.Effect = DragDropEffects.None;
                return;
            }

            base.OnDragEnter(e);

            var text = (String)e.Data.GetData(Reorder.GetType());

            e.Effect = text == Reorder ? DragDropEffects.Move : DragDropEffects.None;
        }

        protected override void OnItemDrag(ItemDragEventArgs e)
        {
            base.OnItemDrag(e);

            if (!AllowRowReorder)
                return;

            DoDragDrop(Reorder, DragDropEffects.Move);
        }
    }

    public delegate void ItemMovedEventHandler(object sender, ItemMovedEventArgs e);

    public class ItemMovedEventArgs : EventArgs
    {
        public ListViewItem Item { get; private set; }
        public int Index { get; private set; }

        public ItemMovedEventArgs(ListViewItem item, int index)
        {
            Item = item;
            Index = index;
        }
    }
}
