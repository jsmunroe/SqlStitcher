using System.Collections.Generic;
using System.Windows.Forms;
using SqlStitcher.Forms.Properties;

namespace SqlStitcher.Forms.Contracts
{
    public interface IApp
    {
        /// <summary>
        /// Application settings.
        /// </summary>
        ISettings Settings{ get; }

        /// <summary>
        /// Get the path to the projects directory.
        /// </summary>
        /// <returns>Path to projects directory.</returns>
        string GetProjectsDirectoryPath();

        /// <summary>
        /// Exit the application.
        /// </summary>
        void Exit();
    }
}