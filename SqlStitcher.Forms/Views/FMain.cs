using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
using SqlStitcher.Forms.ViewModels;

namespace SqlStitcher.Forms.Views
{
    public partial class FMain : Form, IForm<MainViewModel>
    {
        private readonly IProjectState _projectState = ServiceLocator.Current.GetInstance<IProjectState>();
        private readonly IMessenger _messenger = ServiceLocator.Current.GetInstance<IMessenger>();
        
        private UserControl _projectOverview;
        private UserControl _home;

        public FMain()
        {
            InitializeComponent();

            FormSizer.From(this)
                     .Size(p => p.FMain_Size)
                     .Location(p => p.FMain_Location)
                     .Maximized(p => p.FMain_IsMaximized);

            ShowHome();

            _messenger.Subscribe<RecentFileListChangedMessage>(this, OnMessageRecieved);
            _messenger.Subscribe<OpenRecentProjectMessage>(this, OnMessageRecieved);
        }

        public MainViewModel ViewModel { get; set; }

        private void UpdateFormMenu()
        {
            var isProjectLoaded = (_projectState.CurrentProject != null);

            _scriptsCopyToClipboardMenu.Enabled = isProjectLoaded;
            _scriptsIdentifiersMenu.Enabled = isProjectLoaded;
            _scriptsViewScriptMenu.Enabled = isProjectLoaded;

            _projectCloseMenu.Enabled = isProjectLoaded;
            _projectSaveMenu.Enabled = isProjectLoaded;
            _projectSaveAsMenu.Enabled = isProjectLoaded;
        }

        private void LoadCurrent()
        {
            var viewCatalog = ServiceLocator.Current.GetInstance<IViewCatalog>();

            var viewModel = new ProjectOverviewViewModel(_projectState.CurrentProject);

            if (_home != null)
                Controls.Remove(_home);

            if (_projectOverview != null)
            {
                Controls.Remove(_projectOverview);
                _projectOverview.Dispose();
            }
            _projectOverview = viewCatalog.Resolve(viewModel).AsUserControl();
            _projectOverview.Name = "Contents";
            _projectOverview.Visible = true;
            _projectOverview.Dock = DockStyle.Fill;

            Controls.Add(_projectOverview);

            _projectOverview.BringToFront();

            UpdateFormMenu();
        }

        private void CloseCurrent()
        {
            if (_projectOverview != null)
            {
                Controls.Remove(_projectOverview);
                _projectOverview.Dispose();
                _projectOverview = null;
            }

            UpdateFormMenu();
            ShowHome();
        }

        private void ShowHome()
        {
            if (_home != null)
                Controls.Add(_home);

            var viewCatalog = ServiceLocator.Current.GetInstance<IViewCatalog>();

            var viewModel = new HomeViewModel();
            _home = viewCatalog.Resolve(viewModel).AsUserControl();
            _home.Name = "Home";
            _home.Visible = true;
            _home.Dock = DockStyle.Fill;

            Controls.Add(_home);

            _home.BringToFront();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            var recentFilesList = ServiceLocator.Current.GetInstance<IRecentFileList>();
            LoadMostRecent(recentFilesList);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            ViewModel.Dispose();
        }

        private void OnMessageRecieved(RecentFileListChangedMessage message)
        {
            LoadMostRecent(message.MostRecentFilePaths);
        }

        private void OnMessageRecieved(OpenRecentProjectMessage message)
        {
            ViewModel.OpenRecentProject(message.ProjectFile);
            LoadCurrent();
        }

        private void LoadMostRecent(IEnumerable<string> mostRecentPaths)
        {
            _projectRecentMenu.DropDownItems.Clear();

            if (!mostRecentPaths.Any())
            {
                _projectRecentMenu.DropDownItems.Add(_projectRecentNoneMenu);
            }
            else
            {
                foreach (var filePath in mostRecentPaths)
                {
                    var menuItem = new ToolStripMenuItem
                    {
                        Text = Utilities.ShortenPath(filePath, 45),
                        Tag = filePath
                    };

                    menuItem.Click += OnRecentMenuItemClick;
                    _projectRecentMenu.DropDownItems.Add(menuItem);
                }
            }
        }

        private void OnRecentMenuItemClick(object sender, EventArgs e)
        {
            var menuItem = sender as ToolStripMenuItem;
            if (menuItem == null)
                return;

            var filePath = menuItem.Tag as string;
            if (filePath == null)
                return;

            if (ViewModel.OpenRecentProject(filePath))
            {
                LoadCurrent();
            }
        }

        private void _projectNewMenu_Click(object sender, EventArgs e)
        {
            if (ViewModel.NewProject())
            {
                LoadCurrent();
            }
        }

        private void _projectOpenMenu_Click(object sender, EventArgs e)
        {
            if (ViewModel.OpenProject())
            {
                LoadCurrent();
            }
        }

        private void _projectCloseMenu_Click(object sender, EventArgs e)
        {
            if (ViewModel.CloseProject())
            {
                CloseCurrent();
            }
        }

        private void _projectSaveMenu_Click(object sender, EventArgs e)
        {
            ViewModel.SaveProject();
        }

        private void _projectSaveAsMenu_Click(object sender, EventArgs e)
        {
            ViewModel.SaveProjectAs();
        }

        private void _exitMenu_Click(object sender, EventArgs e)
        {
            if (ViewModel.Exit())
                App.Current.Exit();
        }

        private void _scriptsIdentifiersMenu_Click(object sender, EventArgs e)
        {
            ViewModel.ShowIdentifiers();
        }

        private void _scriptsSendToClipboardMenu_Click(object sender, EventArgs e)
        {
            ViewModel.SendProjectScriptToClipboard();
        }

        private void _scriptsViewScriptMenu_Click(object sender, EventArgs e)
        {
            ViewModel.ViewProjectScript();
        }

        private void _toolsOptionsMenu_Click(object sender, EventArgs e)
        {
            ViewModel.ShowOptions();
        }

        private void _helpAboutMenu_Click(object sender, EventArgs e)
        {
            ViewModel.ShowAbout();
        }

        private void FMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!ViewModel.Exit())
                e.Cancel = true;
        }
    }
}
