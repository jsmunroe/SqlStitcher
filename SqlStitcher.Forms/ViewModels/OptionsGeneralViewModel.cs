using System;
using System.IO;
using System.Windows.Forms;
using Helpers.Archetecture;
using SqlStitcher.Forms.Contracts;
using SqlStitcher.Forms.Infrastructure;

namespace SqlStitcher.Forms.ViewModels
{
    public class OptionsGeneralViewModel : BaseViewModel
    {
        private readonly ISettings _settings;

        private int _recentListLength;
        private string _projectsDirectoryPath;

        private readonly FolderBrowserDialog _browseForProjectRootDialog;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="settings">Application settings.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="settings"/> is null.</exception>
        public OptionsGeneralViewModel(ISettings settings)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));

            RecentListLength = _settings.RecentListLength;
            ProjectsDirectoryPath = _settings.ProjectsDirectoryPath;

            _browseForProjectRootDialog = new FolderBrowserDialog
            {
                Description = "Select the Default Projects Directory."
            };
        }

        /// <summary>
        /// Default projects root directory path.
        /// </summary>
        public string ProjectsDirectoryPath
        {
            get { return _projectsDirectoryPath; }
            set { _projectsDirectoryPath = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// Length of recent projects list.
        /// </summary>
        public int RecentListLength
        {
            get { return _recentListLength; }
            set { _recentListLength = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// Browse for the <see cref="ProjectsDirectoryPath"/> value.
        /// </summary>
        public void BrowseForRoot()
        {
            _browseForProjectRootDialog.SelectedPath = ProjectsDirectoryPath;
            if (_browseForProjectRootDialog.ShowDialog(App.Current.MainForm) == DialogResult.OK)
            {
                ProjectsDirectoryPath = _browseForProjectRootDialog.SelectedPath;
            }
        }

        /// <summary>
        /// Save the settings herein.
        /// </summary>
        public void Save()
        {
            _settings.RecentListLength = RecentListLength;
            _settings.ProjectsDirectoryPath = ProjectsDirectoryPath;
        }

        /// <summary>
        /// Validate this view model.
        /// </summary>
        /// <returns>Validation result.</returns>
        public override Result Validate()
        {
            if (string.IsNullOrEmpty(ProjectsDirectoryPath))
            {
                return Result.Fail("Default Projects Directory is empty.");
            }

            if (!Directory.Exists(ProjectsDirectoryPath))
            {
                return Result.Fail("Given Default Projects Directory does not exist.");
            }

            return Result.OK;
        }
    }
}