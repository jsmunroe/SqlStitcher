using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Practices.ServiceLocation;
using SqlStitcher.Forms.Contracts;
using SqlStitcher.Forms.Helpers;
using SqlStitcher.Forms.ViewModels;

namespace SqlStitcher.Forms.Views
{
    public partial class Home : UserControl, IUserControl<HomeViewModel>
    {
        public Home(HomeViewModel viewModel)
        {
            InitializeComponent();

            ViewModel = viewModel;
            ViewModel.PropertyChanged += ViewModel_PropertyChanged;

            LoadRecentProjects();
        }

        public HomeViewModel ViewModel { get; set; }

        public void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ProjectFiles")
                LoadRecentProjects();
        }

        private void LoadRecentProjects()
        {
            _recentProjectsPanel.Visible = ViewModel.ProjectFiles.Any();
            
            _recentProjects.Controls.Clear();

            var index = 0;
            foreach (var projectFile in ViewModel.ProjectFiles)
            {
                var recentProjectLink = _linkPrototype.Clone();

                recentProjectLink.Text = Utilities.ShortenPath(projectFile, 45);
                var location = recentProjectLink.Location;
                location.Offset(0, 20 * index);
                recentProjectLink.Location = location;
                recentProjectLink.Tag = projectFile;
                recentProjectLink.LinkClicked += recentProjectLink_LinkClicked;
                _toolTip.SetToolTip(recentProjectLink, projectFile);
                _recentProjects.Controls.Add(recentProjectLink);

                index++;
            }
        }

        private void recentProjectLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var recentProjectLink = (LinkLabel) sender;

            var projectFile = recentProjectLink.Tag as string;

            ViewModel.OpenProject(projectFile);
        }
    }
}
