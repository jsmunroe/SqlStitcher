using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SqlStitcher.Models;

namespace SqlStitcher
{
    public class ScriptBuilder
    {
        private readonly List<ScriptEntry> _entries = new List<ScriptEntry>();

        /// <summary>
        /// Script entries currently in this builder.
        /// </summary>
        public IReadOnlyList<ScriptEntry> Entries { get { return _entries.AsReadOnly(); } }

        /// <summary>
        /// Append the given script entry (<paramref name="scriptEntry"/>) to this builder.
        /// </summary>
        /// <param name="scriptEntry">Script entry.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="scriptEntry"/> is null.</exception>
        public void Append(ScriptEntry scriptEntry)
        {
            #region Argument Validation

            if (scriptEntry == null)
            {
                throw new ArgumentNullException("scriptEntry");
            }

            #endregion

            _entries.Add(scriptEntry);
        }

        /// <summary>
        /// Append the given range of script entries (<paramref name="scriptEntries"/>) to this 
        ///     builder keeping them in order.
        /// </summary>
        /// <param name="scriptEntries">Script entries.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="scriptEntries"/> is null.</exception>
        public void AppendRange(IEnumerable<ScriptEntry> scriptEntries)
        {
            #region Argument Validation

            if (scriptEntries == null)
            {
                throw new ArgumentNullException("scriptEntries");
            }

            #endregion

            _entries.AddRange(scriptEntries);
        }

        /// <summary>
        /// Build the script text.
        /// </summary>
        /// <returns>Built script text.</returns>
        public string Build(BuildOptions buildOptions = null)
        {
            buildOptions = buildOptions ?? new BuildOptions();

            var scriptTextBuilder = new StringBuilder();

            foreach (var entry in _entries)
            {
                var entryText = entry.ReadToEnd();

                if (buildOptions.PrependScriptDescription == PrependDescription.FileName)
                {
                    scriptTextBuilder.AppendLine($"-- Script File: {entry.File.Name}");
                    scriptTextBuilder.AppendLine();
                }

                if (buildOptions.PrependScriptDescription == PrependDescription.FileFullPath)
                {
                    scriptTextBuilder.AppendLine($"-- Script File: {entry.File.Path}");
                    scriptTextBuilder.AppendLine();
                }

                scriptTextBuilder.AppendLine(entryText);
                scriptTextBuilder.AppendLine();

                if (buildOptions.InsertGoCommand)
                {
                    scriptTextBuilder.AppendLine("GO");
                    scriptTextBuilder.AppendLine();
                }
            }

            var scriptText = scriptTextBuilder.ToString();

            return scriptText;
        }
    }
}
