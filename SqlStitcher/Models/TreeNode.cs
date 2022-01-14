using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlStitcher.Models
{
    public class TreeNode<TValue>
    {
        public TreeNode()
        {
            Children = new List<TreeNode<TValue>>();
        }

        public string Description { get; set; }

        public TValue Value { get; set; }

        public List<TreeNode<TValue>> Children { get; private set; }
    }
}
