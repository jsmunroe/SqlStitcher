using System;
using System.IO;
using System.Xml.Linq;
using Helpers.Contracts;
using Helpers.IO;
using SqlStitcher.Contracts;

namespace SqlStitcher.Models
{
    public class ScriptEntry : IScriptSource, IXmlSavable
    {
        /// <summary>
        /// File name.
        /// </summary>
        public string Name { get { return File.Name; } }

        /// <summary>
        /// File info.
        /// </summary>
        public IFile File { get; private set; }

        /// <summary>
        /// Whether this <see cref="ScriptEntry"/> is selected to be built. 
        /// </summary>
        public bool IsSelected { get; set; }

        /// <summary>
        /// Script ordinal.
        /// </summary>
        public int Ordinal { get; set; }

        /// <summary>
        /// Build the script entry to represent the given file (<paramref name="file"/>).
        /// </summary>
        /// <param name="file">File from which to build the entry.</param>
        /// <returns>Built entry.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="file"/> is null.</exception>
        public static ScriptEntry Build(IFile file)
        {
            #region Argument Validation

            if (file == null)
            {
                throw new ArgumentNullException("file");
            }

            #endregion

            return new ScriptEntry
            {
                File = file,
                IsSelected = true,
            };
        }

        /// <summary>
        /// Read to the end of the script file and return it as text.
        /// </summary>
        /// <returns>Script text.</returns>
        public string ReadToEnd()
        {
            using (var fin = File.OpenRead())
            {
                var reader = new StreamReader(fin);

                return reader.ReadToEnd();
            }
        }

        /// <summary>
        /// Create an XML representation of this instance as an <see cref="XElement"/>.
        /// </summary>
        /// <returns>Created <see cref="XElement"/>.</returns>
        public XElement ToXml()
        {
            return new XElement("ScriptEntry",
                new XAttribute("Version", 1),
                new XAttribute("IsSelected", IsSelected),
                new XAttribute("Ordinal", Ordinal),
                File.Path);
        }

        /// <summary>
        /// Create an instance of this type from the given XML element.
        /// </summary>
        /// <param name="element">XML element.</param>
        /// <returns>Created instance.</returns>
        public static ScriptEntry FromXml(XElement element)
        {
            var version = (int)element.Attribute("Version");

            if (version != 1)
                throw new Exception("ScriptEntry element is anachronistic with this version of the software and cannot be loaded.");

            var path = (string)element;
            var file = new FsFile(path);

            var scriptEntry = Build(file);

            scriptEntry.IsSelected = (bool)element.Attribute("IsSelected");
            scriptEntry.Ordinal = (int?)element.Attribute("Ordinal") ?? 0;

            return scriptEntry;
        }

        #region IScriptSource Memebers

        string IScriptSource.GetScript(BuildOptions scriptBuildOptions)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Equals Implementation
                
        /// <summary>
        /// Determines whether the given object (<paramref name="obj"/>) is equal to this one.
        /// </summary>
        /// <param name="obj">Object.</param>
        /// <returns>True if equal.</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            if (obj is ScriptEntry)
            {
                return Equals(obj as ScriptEntry);
            }

            return false;
        }

        /// <summary>
        /// Determines whether the other given <see cref="ScriptEntry"/> (<paramref name="other"/>) 
        ///     is equal to this one.
        /// </summary>
        /// <param name="other">Other <see cref="ScriptEntry"/>.</param>
        /// <returns>True if equal.</returns>
        public bool Equals(ScriptEntry other)
        {
            if (other == null)
                return false;

            if (File == null || other.File == null)
                return false;

            return Equals(File.Path, other.File.Path);
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>Instance hash.</returns>
        public override int GetHashCode()
        {
            return (File != null ? File.Path.ToString().GetHashCode() : 0);
        }

        #endregion
    }
}