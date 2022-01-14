using System;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Xml.Schema;
using SqlStitcher.Contracts;

namespace SqlStitcher.Models
{
    public class ScriptIdentifier : IXmlSavable
    {
        public string Name { get; set; }

        public string ReplacementName { get; set; }

        /// <summary>
        /// Replace this identifier against the given script text <paramref name="scriptText"/>.
        /// </summary>
        /// <param name="scriptText">Script text.</param>
        /// <returns>Replace script text.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="scriptText"/> is null.</exception>
        public string Replace(string scriptText)
        {
            #region Argument Validation

            if (scriptText == null)
            {
                throw new ArgumentNullException("scriptText");
            }

            #endregion

            var pattern = string.Format(@"\[{0}\]", Regex.Escape(Name));
            var replacementText = string.Format(@"[{0}]", ReplacementName.Trim('[', ']'));

            return Regex.Replace(scriptText, pattern, replacementText, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// Create an XML representation of this instance as an <see cref="XElement"/>.
        /// </summary>
        /// <returns>Created <see cref="XElement"/>.</returns>
        public XElement ToXml()
        {
            return new XElement("ScriptIdentifier",
                new XAttribute("Version", 1),
                new XElement("Name", new XCData(Name)),
                new XElement("ReplacementName", new XCData(ReplacementName)));
        }

        /// <summary>
        /// Create an XML representation of this instance as an <see cref="XElement"/>.
        /// </summary>
        /// <returns>Created <see cref="XElement"/>.</returns>
        public static ScriptIdentifier FromXml(XElement element)
        {
            var version = (int)element.Attribute("Version");

            if (version != 1)
                throw new Exception("ScriptIdentifier element is anachronistic with this version of the stoftware and cannot be loaded.");

            var name = (string) element.Element("Name");
            var replacementName = (string) element.Element("ReplacementName");

            return new ScriptIdentifier
            {
                Name = name,
                ReplacementName = replacementName
            };
        }
    }
}