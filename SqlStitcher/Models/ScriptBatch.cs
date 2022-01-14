using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlStitcher.Contracts;

namespace SqlStitcher.Models
{
    public class ScriptBatch : IEnumerable<ScriptEntry>, IScriptSource
    {
        private readonly Project _project;
        private readonly List<ScriptEntry> _entries;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="project">Project used to create script text.</param>
        /// <param name="entries">Entities from which the script text is generated.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="project"/> is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="entries"/> is null.</exception>
        public ScriptBatch(Project project, IEnumerable<ScriptEntry> entries)
        {
            #region Argument Validation

            if (project == null)
            {
                throw new ArgumentNullException("project");
            }

            if (entries == null)
            {
                throw new ArgumentNullException("entries");
            }

            #endregion

            _project = project;
            _entries = entries.ToList();
        }

        /// <summary>
        /// Get the script text for the entries batched within.
        /// </summary>
        /// <returns>Script text.</returns>
        public string GetScript(BuildOptions scriptBuildOptions = null)
        {
            return _project.Build(_entries, scriptBuildOptions);
        }

        #region IEnumerable Members
		        
        public IEnumerator<ScriptEntry> GetEnumerator()
        {
            return _entries.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

	    #endregion
    }
}
