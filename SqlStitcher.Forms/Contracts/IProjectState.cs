using System;
using Helpers.Contracts;
using SqlStitcher.Models;

namespace SqlStitcher.Forms.Contracts
{
    public interface IProjectState
    {
        /// <summary>
        /// Current working project.
        /// </summary>
        Project CurrentProject { get; }

        /// <summary>
        /// File to which the current project is saved.
        /// </summary>
        IFile SavedFile { get; }

        /// <summary>
        /// Whether the current project needs to be saved.
        /// </summary>
        bool SaveNeeded { get; }

        /// <summary>
        /// Create a project and make it the current project.
        /// </summary>
        /// <param name="projectRoot">Project root directory.</param>
        void CreateProject(IDirectory projectRoot);

        /// <summary>
        /// Clear the current project.
        /// </summary>
        void ClearProject();

        /// <summary>
        /// Save the current project to the given file.
        /// </summary>
        /// <param name="file">File to which to save.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="file"/> is null.</exception>
        void SaveProject(IFile file);

        /// <summary>
        /// Load the a project from the given file () as the current project.
        /// </summary>
        /// <param name="file">File from which to load.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="file"/> is null.</exception>
        void OpenProject(IFile file);

        /// <summary>
        /// Indicate that the current project has been changed and needs to be saved.
        /// </summary>
        void InvalidateSave();
    }
}