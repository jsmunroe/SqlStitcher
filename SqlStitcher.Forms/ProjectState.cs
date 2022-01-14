using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Helpers.Contracts;
using SqlStitcher.Forms.Contracts;
using SqlStitcher.Models;

namespace SqlStitcher.Forms
{
    public class ProjectState : IProjectState
    {
        private readonly IRecentFileList _recentFileList;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="recentFileList"></param>
        public ProjectState(IRecentFileList recentFileList)
        {
            _recentFileList = recentFileList;
        }

        /// <summary>
        /// File to which the current project is saved.
        /// </summary>
        public IFile SavedFile { get; private set; }

        /// <summary>
        /// Whether the current project needs to be saved.
        /// </summary>
        public bool SaveNeeded { get; private set; }

        /// <summary>
        /// Current working project.
        /// </summary>
        public Project CurrentProject { get; private set; }

        /// <summary>
        /// Create a project and make it the current project.
        /// </summary>
        /// <param name="projectRoot">Project root directory.</param>
        public void CreateProject(IDirectory projectRoot)
        {
            var project = new Project(projectRoot);

            project.Load();

            ClearProject();

            CurrentProject = project;
            SaveNeeded = true;
        }

        /// <summary>
        /// Load the a project from the given file (<paramref name="file"/>) as the current project.
        /// </summary>
        /// <param name="file">File from which to load.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="file"/> is null.</exception>
        public void OpenProject(IFile file)
        {
            #region Argument Validation

            if (file == null)
            {
                throw new ArgumentNullException("file");
            }

            #endregion

            if (CurrentProject != null)
                ClearProject();

            var document = XDocument.Load(file.Path);

            var element = document.Root;

            CurrentProject = Project.FromXml(element);

            SavedFile = file;
            _recentFileList.Push(file.Path);
            SaveNeeded = false;
        }

        /// <summary>
        /// Clear the current project.
        /// </summary>
        public void ClearProject()
        {
            CurrentProject = null;
            SavedFile = null;
            SaveNeeded = false;
        }

        /// <summary>
        /// Save the current project to the given file (<paramref name="file"/>).
        /// </summary>
        /// <param name="file">File to which to save.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="file"/> is null.</exception>
        public void SaveProject(IFile file)
        {
            #region Argument Validation

            if (file == null)
            {
                throw new ArgumentNullException("file");
            }

            #endregion

            if (CurrentProject == null)
                throw new Exception("There is no current project.");

            var element = CurrentProject.ToXml();

            var document = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                element);

            document.Save(file.Path);

            SavedFile = file;
            _recentFileList.Push(file.Path);
            SaveNeeded = false;
        }

        /// <summary>
        /// Indicate that the current project has been changed and needs to be saved.
        /// </summary>
        public void InvalidateSave()
        {
            SaveNeeded = true;
        }
    }
}
