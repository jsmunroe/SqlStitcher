using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SqlStitcher.Forms.Contracts;
using SqlStitcher.Forms.Infrastructure;
using SqlStitcher.Forms.Infrastructure.ValueConverters;
using SqlStitcher.Forms.ViewModels;

namespace SqlStitcher.Forms.Views
{
    public partial class OptionsGeneral : UserControl, IUserControl<OptionsGeneralViewModel>
    {
        public OptionsGeneral(OptionsGeneralViewModel viewModel)
        {
            InitializeComponent();

            _recentFilesListLength.SelectedIndex = 0;
            ViewModel = viewModel;
        }

        public OptionsGeneralViewModel ViewModel { get; set; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Binder.Create(ViewModel)
                  .Bind(p => p.RecentListLength).To(_recentFilesListLength, p => p.Text, IntegerToStringConverter.Default).AndBack().Go()
                  .Bind(p => p.ProjectsDirectoryPath).To(_projectsRoot, p => p.Text).AndBack().Go();
        }

        private void _projectsRootBrowse_Click(object sender, EventArgs e)
        {
            ViewModel.BrowseForRoot();
        }
    }
}
