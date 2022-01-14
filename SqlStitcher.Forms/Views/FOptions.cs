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
    public partial class FOptions : Form, IForm<OptionsViewModel>
    {
        private IViewCatalog _viewCatalog;

        private readonly List<UserControl> _pageList = new List<UserControl>();

        public FOptions(OptionsViewModel viewModel)
        {
            _viewCatalog = ServiceLocator.Current.GetInstance<IViewCatalog>();

            InitializeComponent();

            ViewModel = viewModel;

            FormSizer.From(this)
                .Size(p => p.FOptions_Size)
                .Location(p => p.FOptions_Location)
                .Maximized(p => p.FOptions_IsMaximized);

            Binder.Create(ViewModel)
                  .Bind(p => p.ErrorMessage).To(_errorMessage, p => p.Text).Go();

            _pages.SelectedIndex = 0;

            var generalPage = _viewCatalog.Resolve(ViewModel.General).AsUserControl();
            generalPage.Dock = DockStyle.Fill;
            _page.Controls.Add(generalPage);
            generalPage.Visible = true;
            _pageList.Add(generalPage);

            var stitchingPage = _viewCatalog.Resolve(ViewModel.Stitching).AsUserControl();
            stitchingPage.Dock = DockStyle.Fill;
            _page.Controls.Add(stitchingPage);
            stitchingPage.Visible = false;
            _pageList.Add(stitchingPage);
        }

        public OptionsViewModel ViewModel { get; set; }

        private void _okButton_Click(object sender, EventArgs e)
        {
            if (!ViewModel.Save())
                DialogResult = DialogResult.None;
        }

        private void _pages_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedIndex = ((ListBox)sender).SelectedIndex;

            for (int i = 0; i < _pageList.Count; i++)
            {
                var page = _pageList[i];

                page.Visible = (i == selectedIndex);
            }
        }
    }
}
