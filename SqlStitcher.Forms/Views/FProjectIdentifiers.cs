using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Practices.ServiceLocation;
using SqlStitcher.Forms.Contracts;
using SqlStitcher.Forms.Infrastructure;
using SqlStitcher.Forms.ViewModels;

namespace SqlStitcher.Forms.Views
{
    public partial class FProjectIdentifiers : Form, IForm<ProjectIdentifiersViewModel>
    {
        private readonly IViewCatalog _viewCatalog = ServiceLocator.Current.GetInstance<IViewCatalog>();

        public FProjectIdentifiers(ProjectIdentifiersViewModel viewModel)
        {
            InitializeComponent();

            ViewModel = viewModel;

            LoadIdentifiers(30);
        }

        private void LoadIdentifiers(int count)
        {
            _identifiers.Controls.Clear();

            var identifiers = ViewModel.Identifiers;

            if (!string.IsNullOrWhiteSpace(_searchText.Text))
            {
                identifiers = identifiers.Where(p => p.ReplacementName.StartsWith(_searchText.Text, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            foreach (var identifier in identifiers.Take(count))
            {
                var control = (UserControl)_viewCatalog.Resolve(identifier);
                control.Visible = true;
                control.Dock = DockStyle.Top;

                _identifiers.Controls.Add(control);
                control.BringToFront();
            }
        }

        public ProjectIdentifiersViewModel ViewModel { get; set; }

        private void _okButton_Click(object sender, EventArgs e)
        {
            var validationResult = ViewModel.Save();

            if (!validationResult)
            {
                MessageBox.Show(this, validationResult.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
            }
        }

        private void _revert_Click(object sender, EventArgs e)
        {
            ViewModel.Revert();
            LoadIdentifiers(30);
        }

        private void _search_Click(object sender, EventArgs e)
        {
            LoadIdentifiers(30);
        }
    }
}
