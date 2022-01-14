using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SqlStitcher.Forms.Contracts;
using SqlStitcher.Forms.Infrastructure;
using SqlStitcher.Forms.ViewModels;

namespace SqlStitcher.Forms.Views
{
    public partial class FNewProject : Form, IForm<NewProjectViewModel>
    {
        public NewProjectViewModel ViewModel { get; set; }

        public FNewProject(NewProjectViewModel viewModel)
        {
            #region Argument Validation

            if (viewModel == null)
            {
                throw new ArgumentNullException("viewModel");
            }

            #endregion

            ViewModel = viewModel;

            InitializeComponent();

            Binder.Create(ViewModel)
                  .Bind(p => p.RootPath).To(_projectRootText, p => p.Text).AndBack().Go()
                  .Bind(p => p.ErrorMessage).To(_errorMessage, p=> p.Text).Go();
        }

        private void _okButton_Click(object sender, EventArgs e)
        {
            ViewModel.RootPath = _projectRootText.Text;

            if (!ViewModel.Save())
            {
                DialogResult = DialogResult.None;
                return;
            }

            Close();
        }

        private void _projectRootBrowse_Click(object sender, EventArgs e)
        {
            ViewModel.BrowseForRoot();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            ViewModel.Dispose();
        }
    }
}
