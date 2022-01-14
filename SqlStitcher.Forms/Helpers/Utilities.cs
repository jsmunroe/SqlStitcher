using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SqlStitcher.Forms.Helpers
{
    public static class Utilities
    {
        private const string Ellipses = "…";

        /// <summary>
        /// Shorten the given path (<paramref name="path"/>) by adding ellipses.
        /// </summary>
        /// <param name="path">Path to shorten.</param>
        /// <param name="maxLength">Maximum length.</param>
        /// <returns>Shortened path.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="path"/> is null.</exception>
        public static string ShortenPath(string path, int maxLength = 25)
        {
            #region Argument Validation

            if (path == null)
            {
                throw new ArgumentNullException("path");
            }

            #endregion

            var root = FindRoot(path);
            var rest = path.Substring(root.Length);

            while (path.Length > maxLength)
            {
                var backslashIndex = rest.IndexOf("\\", StringComparison.Ordinal);

                if (backslashIndex < 0)
                    return root + Ellipses + path.Substring(Math.Max(0, path.Length - (maxLength - root.Length - 1)));

                rest = rest.Substring(backslashIndex + 1);
                path = root + Ellipses + "\\" + rest;
            }

            return path;
        }

        /// <summary>
        /// Find the root for the given path (<paramref name="path"/>)
        /// </summary>
        /// <param name="path">Path.</param>
        /// <returns>Root.</returns>
        private static string FindRoot(string path)
        {
            var patterns = new[]
            {
                new Regex(@"^(?<root>[a-z]\:\\)", RegexOptions.IgnoreCase),
                new Regex(@"^(?<root>\\\\\w+\\)", RegexOptions.IgnoreCase),
                new Regex(@"^(?<root>\\)", RegexOptions.IgnoreCase) 
            };

            return patterns.Select(p => p.Match(path))
                           .Where(p => p.Success)
                           .Select(p => p.Groups["root"].Value)
                           .FirstOrDefault() ?? "";
        }
    }
}
