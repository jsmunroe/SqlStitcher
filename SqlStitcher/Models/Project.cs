using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using Helpers.Contracts;
using Helpers.IO;
using Microsoft.Practices.ServiceLocation;
using SqlStitcher.Contracts;
using SqlStitcher.Helpers;

namespace SqlStitcher.Models
{
    public class Project : IScriptSource, IXmlSavable
    {
        private readonly List<ScriptIdentifier> _identifiers = new List<ScriptIdentifier>();
        private readonly DirectoryAggregator _aggregator;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="root">Project root directory.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="root"/> is null.</exception>
        public Project(IDirectory root)
        {
            #region Argument Validation

            if (root == null)
            {
                throw new ArgumentNullException("root");
            }

            #endregion

            Root = root;
            Entries = new List<ScriptEntry>();

            _aggregator = new DirectoryAggregator(Root);
        }

        /// <summary>
        /// Project root directory.
        /// </summary>
        public IDirectory Root { get; set; }

        /// <summary>
        /// Script entries in this project.
        /// </summary>
        public IReadOnlyList<ScriptEntry> Entries { get; set; }

        /// <summary>
        /// Load this project's entries.
        /// </summary>
        public void Load()
        {
            Entries = _aggregator.GetEntries().ToList().AsReadOnly();
            UnselectAll();
        }
        
        /// <summary>
        /// Build a tree from the project's loaded entries.
        /// </summary>
        /// <returns>Root node of tree.</returns>
        public TreeNode<ScriptEntry> BuildTree()
        {
            return _aggregator.BuildTree();
        }

        /// <summary>
        /// Build the script.
        /// </summary>
        public string Build(BuildOptions options = null)
        {
            return Build(Entries.Where(p => p.IsSelected), options);
        }

        /// <summary>
        /// Build a script from the given entries.
        /// </summary>
        public string Build(IEnumerable<ScriptEntry> entries, BuildOptions options = null)
        {
            var scriptBuilder = new ScriptBuilder();

            scriptBuilder.AppendRange(entries);

            var scriptText = scriptBuilder.Build(options);

            foreach (var identifier in _identifiers)
            {
                scriptText = identifier.Replace(scriptText);
            }

            return scriptText;
        }
        
        /// <summary>
        /// Select the given entries (<paramref name="entries"/>) if they are extant in this project.
        /// </summary>
        /// <param name="entries">Entries to select.</param>
        public void Select(IEnumerable<ScriptEntry> entries)
        {
            var selectedEntries = Entries.Join(entries, p => p, q => q, (p, q) => new { Entry=p, q.Ordinal });

            Parallel.ForEach(selectedEntries, p =>
            {
                p.Entry.IsSelected = true;
                p.Entry.Ordinal = p.Ordinal;
            });
        }

        /// <summary>
        /// Select all entries.
        /// </summary>
        public void SelectAll()
        {
            Parallel.ForEach(Entries, p => p.IsSelected = true);
        }

        /// <summary>
        /// Unselect all entries.
        /// </summary>
        public void UnselectAll()
        {
            Parallel.ForEach(Entries, p => p.IsSelected = false);
        }

        /// <summary>
        /// Return the identifiers within the script text.
        /// </summary>
        /// <returns>Identifiers in the script text.</returns>
        public IEnumerable<ScriptIdentifier> GetIdentifiers()
        {
            var scriptBuilder = new ScriptBuilder();
            scriptBuilder.AppendRange(Entries.Where(p => p.IsSelected));
            var scriptText = scriptBuilder.Build();

            var identifiers = GetIdentifiers(scriptText);

            foreach (var identifier in identifiers)
            {
                var scriptIdentifier = new ScriptIdentifier
                {
                    Name = identifier,
                    ReplacementName = identifier
                };

                var replacementIdentifier = _identifiers.FirstOrDefault(p => string.Equals(p.Name, identifier, StringComparison.OrdinalIgnoreCase));

                if (replacementIdentifier != null)
                {
                    scriptIdentifier.ReplacementName = replacementIdentifier.ReplacementName;
                }

                yield return scriptIdentifier;
            }
        }

        /// <summary>
        /// Replace the identifier with the given text (<paramref name="text"/>) with one 
        ///     containing the given replacement text (<paramref name="replacementText"/>).
        /// </summary>
        /// <param name="text">Original identifier text.</param>
        /// <param name="replacementText">Replacement identifier text.</param>
        /// <returns>True if identifier was replaced.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="text"/> is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="replacementText"/> is null.</exception>
        public bool ReplaceIdentifier(string text, string replacementText)
        {
            #region Argument Validation

            if (text == null)
            {
                throw new ArgumentNullException("text");
            }

            if (replacementText == null)
            {
                throw new ArgumentNullException("replacementText");
            }

            #endregion

            var originalIdentifiers = GetIdentifiers();

            if (!originalIdentifiers.Any(p => string.Equals(p.Name, text, StringComparison.OrdinalIgnoreCase)))
            {
                return false;
            }

            _identifiers.RemoveAll(p => string.Equals(p.Name, text, StringComparison.OrdinalIgnoreCase));

            if (!string.Equals(text, replacementText, StringComparison.OrdinalIgnoreCase))
            {
                _identifiers.Add(new ScriptIdentifier
                {
                    Name = text,
                    ReplacementName = replacementText,
                });
                
            }

            return true;
        }

        /// <summary>
        /// Revert the project to the original identifiers.
        /// </summary>
        public void Revertidentifiers()
        {
            _identifiers.Clear();
        }

        public XElement ToXml()
        {
            var xProject = new XElement("Project",
                new XAttribute("Version", 1),
                new XElement("Root", Root.Path),
                new XElement("Identifiers", _identifiers.ToXml()),
                new XElement("SelectedEntries", Entries.Where(p => p.IsSelected).ToXml()));

            return xProject;
        }

        /// <summary>
        /// Create an XML representation of this instance as an <see cref="XElement"/>.
        /// </summary>
        /// <returns>Created <see cref="XElement"/>.</returns>
        public static Project FromXml(XElement element)
        {
            var version = (int) element.Attribute("Version");

            if (version != 1)
                throw new Exception("Project element is anachronistic with this version of the stoftware and cannot be loaded.");

            var rootPath = (string) element.Element("Root");

            var identifiers = Enumerable.Empty<ScriptIdentifier>();
            var xIdentifiers = element.Element("Identifiers");
            if (xIdentifiers != null)
            {
                identifiers = xIdentifiers.Elements().Select(ScriptIdentifier.FromXml);
            }

            var selectedEntries = Enumerable.Empty<ScriptEntry>();
            var xSelectedEntries = element.Element("SelectedEntries");
            if (xSelectedEntries != null)
            {
                selectedEntries = xSelectedEntries.Elements().Select(ScriptEntry.FromXml);
            }

            var project = new Project(new FsDirectory(rootPath));

            project.Load();
            project._identifiers.AddRange(identifiers);
            project.Select(selectedEntries);

            return project;
        }

        /// <summary>
        /// Create an XML representation of this instance as an <see cref="XElement"/>.
        /// </summary>
        /// <returns>Created <see cref="XElement"/>.</returns>
        private static IEnumerable<string> GetIdentifiers(string scriptText)
        {
            return Regex.Matches(scriptText, @"\[(?<text>[\w\.]+?)\]")
                .OfType<Match>()
                .Select(p => p.Groups["text"].Value)
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(p => p);
        }

        #region IScriptSource Members

        string IScriptSource.GetScript(BuildOptions scriptBuildOptions)
        {
            return Build(scriptBuildOptions);
        } 

        #endregion
    }
}
