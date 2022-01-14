using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helpers.Contracts;
using Helpers.Test;
using SqlStitcher.Contracts;
using SqlStitcher.Models;

namespace SqlStitcher
{
    public class DirectoryAggregator : IAggregator
    {
        private IEnumerable<ScriptEntry> _entryCache = Enumerable.Empty<ScriptEntry>();

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="root">Root directory.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="root"/> is null.</exception>
        public DirectoryAggregator(IDirectory root)
        {
            #region Argument Validation

            if (root == null)
            {
                throw new ArgumentNullException("root");
            }

            #endregion;

            Root = root;
        }

        /// <summary>
        /// Root directory of this aggregator.
        /// </summary>
        public IDirectory Root { get; set; }

        /// <summary>
        /// Get the entries found in this aggregator.
        /// </summary>
        /// <returns>Entries found in this aggregator.</returns>
        public IEnumerable<ScriptEntry> GetEntries()
        {
            var entries = new List<ScriptEntry>();

            SearchEntries(Root, entries);

            _entryCache = entries;

            return entries;
        }

        /// <summary>
        /// Build a tree of script entries based upon the directory structure in which the 
        /// </summary>
        /// <returns>Root node of the tree.</returns>
        public TreeNode<ScriptEntry> BuildTree()
        {
            var treeNode = new TreeNode<ScriptEntry>
            {
                Description = Root.Path
            };

            var entries = new List<ScriptEntry>();

            BuildTree(Root, treeNode, entries);

            _entryCache = entries;

            return treeNode;
        }

        private void SearchEntries(IDirectory current, List<ScriptEntry> entries)
        {
            var localEntryFiles = current.Files("*.sql");
            var localEntries = localEntryFiles.Select(ScriptEntry.Build).ToArray();
            localEntries = _entryCache.Intersect(localEntries).Concat(localEntries.Except(_entryCache)).ToArray();

            entries.AddRange(localEntries);

            foreach (var child in current.Directories())
            {
                SearchEntries(child, entries);
            }
        }

        private void BuildTree(IDirectory current, TreeNode<ScriptEntry> parentNode, List<ScriptEntry> entries)
        {
            foreach (var child in current.Directories())
            {
                var childNode = new TreeNode<ScriptEntry>
                {
                    Description = child.Name
                };

                BuildTree(child, childNode, entries);

                if (childNode.Children.Any())
                {
                    parentNode.Children.Add(childNode);
                }
            }

            foreach (var scriptFile in current.Files("*.sql"))
            {
                var entry = ScriptEntry.Build(scriptFile);

                entry = _entryCache.FirstOrDefault(p => p.Equals(entry)) ?? entry;

                entries.Add(entry);

                var childNode = new TreeNode<ScriptEntry>
                {
                    Description = scriptFile.Name,
                    Value = entry
                };

                parentNode.Children.Add(childNode);
            }
        }
    }
}
