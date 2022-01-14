using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Helpers.IO;
using Microsoft.Practices.ServiceLocation;
using SqlStitcher.Forms.Contracts;
using SqlStitcher.Forms.Helpers;
using SqlStitcher.Forms.Infrastructure;
using SqlStitcher.Forms.Messages;
using SqlStitcher.Forms.Properties;
using SqlStitcher.Forms.Views;

namespace SqlStitcher.Forms.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly IProjectState _projectState = ServiceLocator.Current.GetInstance<IProjectState>();
        private NewProjectViewModel _newProjectViewModel;

        private readonly SaveFileDialog _saveProjectDialog;
        private readonly OpenFileDialog _openProjectDialog;

        public MainViewModel()
        {
            _saveProjectDialog = new SaveFileDialog
            {
                Title = "Save Project",
                Filter = "SQL Stitcher Project (*.ssp)|*.ssp",
                DefaultExt = ".ssp",
                InitialDirectory = App.Current.GetProjectsDirectoryPath()
            };

            _openProjectDialog = new OpenFileDialog
            {
                Title = "Open Project",
                Filter = "SQL Stitcher Project (*.ssp)|*.ssp",
                DefaultExt = ".ssp",
                InitialDirectory = App.Current.GetProjectsDirectoryPath()
            };
        }

        public bool NewProject()
        {
            if (!AskToSave())
                return false;

            if (_newProjectViewModel != null)
                _newProjectViewModel.Dispose();

            _newProjectViewModel = new NewProjectViewModel();
            if (ViewCatalog.Resolve(_newProjectViewModel).ShowDialog(App.Current.MainForm) == DialogResult.OK)
                return true;

            return false;
        }

        public bool OpenProject()
        {
            if (!AskToSave())
                return false;

            _openProjectDialog.InitialDirectory = App.Current.GetProjectsDirectoryPath();
            if (_openProjectDialog.ShowDialog(App.Current.MainForm) == DialogResult.Cancel)
                return false;

            var openFile = new FsFile(_openProjectDialog.FileName);

            _projectState.OpenProject(openFile);

            return true;
        }

        public bool OpenRecentProject(string filePath)
        {
            if (!AskToSave())
                return false;

            var file = new FsFile(filePath);
            if (!file.Exists)
            {
                if (DialogResult.Yes == MessageBox.Show("Project does not exists.\nDo you want to remove it from the list?", "Project File Not Found", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                {
                    var recentFileList = ServiceLocator.Current.GetInstance<IRecentFileList>();
                    recentFileList.Remove(file.Path);
                }

                return false;
            }

            _projectState.OpenProject(new FsFile(filePath));

            return true;
        }

        public bool CloseProject()
        {
            if (!AskToSave())
                return false;

            _projectState.ClearProject();

            return true;
        }

        public bool SaveProject()
        {
            var saveFile = _projectState.SavedFile;

            if (saveFile == null)
            {
                _saveProjectDialog.InitialDirectory = App.Current.GetProjectsDirectoryPath();
                if (_saveProjectDialog.ShowDialog(App.Current.MainForm) == DialogResult.Cancel)
                    return false;

                saveFile = new FsFile(_saveProjectDialog.FileName);
            }

            _projectState.SaveProject(saveFile);

            return true;
        }

        public bool SaveProjectAs()
        {
            _saveProjectDialog.InitialDirectory = App.Current.GetProjectsDirectoryPath();
            if (_saveProjectDialog.ShowDialog(App.Current.MainForm) == DialogResult.Cancel)
                return false;

            var saveFile = new FsFile(_saveProjectDialog.FileName);

            _projectState.SaveProject(saveFile);

            return true;
        }

        public bool AskToSave()
        {
            if (!_projectState.SaveNeeded)
                return true;

            var answer = MessageBox.Show("The current project has unsaved changes.\nWould you like to save it now?", "Save Changes?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            if (answer == DialogResult.Cancel)
                return false;

            if (answer == DialogResult.No)
                return true;

            if (_projectState.SavedFile != null)
            {
                SaveProject();
                return true;
            }
            else
            {
                SaveProjectAs();
                return false; // Do not continue original operation after Save As.
            }
        }

        public void ShowIdentifiers()
        {
            Messenger.Publish(new ShowIdentifiersMessage());
        }

        public void SendProjectScriptToClipboard()
        {
            Messenger.Publish(new SendProjectScriptToClipboardMessage());
        }

        public void ViewProjectScript()
        {
            Messenger.Publish(new ViewProjectScriptMessage());
        }

        public void ShowOptions()
        {
            var settings = Container.GetInstance<ISettings>();
            ViewCatalog.Resolve(new OptionsViewModel(settings)).ShowDialog(App.Current.MainForm);
        }

        public void ShowAbout()
        {
            ViewCatalog.Resolve(new AboutViewModel()).ShowDialog(App.Current.MainForm);
        }

        public bool Exit()
        {
            if (!AskToSave())
                return false;

            return true;
        }

        protected override void Disposing()
        {
            if (_newProjectViewModel != null)
                _newProjectViewModel.Dispose();

            base.Disposing();
        }
    }
}
