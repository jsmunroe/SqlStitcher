using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Helpers.Archetecture;
using Helpers.IO;
using SqlStitcher.Forms.Contracts;
using SqlStitcher.Forms.Infrastructure;

namespace SqlStitcher.Forms.ViewModels
{
    public class NewProjectViewModel : BaseViewModel
    {
        private readonly IProjectState _projectState;
        private readonly FolderBrowserDialog _browseForRootDialog;
        private string _rootPath;

        public string RootPath
        {
            get { return _rootPath; }
            set { _rootPath = value; RaisePropertyChanged(); }
        }

        public NewProjectViewModel()
        {
            _projectState = Container.GetInstance<IProjectState>();

            RootPath = App.Current.Settings.LastNewProjectRoot;

            _browseForRootDialog = new FolderBrowserDialog
            {
                Description = "Select the project's root folder.",
                SelectedPath = App.Current.Settings.LastNewProjectRoot,
            };
        }

        public void BrowseForRoot()
        {
            _browseForRootDialog.SelectedPath = RootPath;
            if (_browseForRootDialog.ShowDialog(App.Current.MainForm) == DialogResult.OK)
            {
                RootPath = _browseForRootDialog.SelectedPath;
            }
        }

        public bool Save()
        {
            ErrorMessage = "";

            var result = Validate();
            if (!result)
            {
                ErrorMessage = result.Message;
                return false;
            }

            var projectRoot = new FsDirectory(RootPath);
            App.Current.Settings.LastNewProjectRoot = RootPath;

            _projectState.CreateProject(projectRoot);

            return true;
        }

        private Result Validate()
        {
            if (string.IsNullOrEmpty(RootPath))
            {
                return Result.Fail("Root path is empty.");
            }

            if (!Directory.Exists(RootPath))
            {
                return Result.Fail("Given root path does not exist.");
            }

            return Result.OK;
        }
    }
}
