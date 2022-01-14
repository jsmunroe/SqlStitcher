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
using SqlStitcher.Forms.ViewModels;

namespace SqlStitcher.Forms.Views
{
    public partial class Identifier : UserControl, IUserControl<ProjectIdentifiersViewModel.IdentifierViewModel>
    {
        public Identifier()
        {
            InitializeComponent();
        }

        public Identifier(ProjectIdentifiersViewModel.IdentifierViewModel viewModel)
        {
            ViewModel = viewModel;

            InitializeComponent();

            _identifierName.Text = viewModel.ReplacementName;
        }

        public ProjectIdentifiersViewModel.IdentifierViewModel ViewModel { get; set; }

        private void _identifierName_TextChanged(object sender, EventArgs e)
        {
            ViewModel.ReplacementName = _identifierName.Text;
        }

    }
}
