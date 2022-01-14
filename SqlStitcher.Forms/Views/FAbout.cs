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

namespace SqlStitcher.Forms.Views
{
    public partial class FAbout : Form, IForm<AboutViewModel>
    {
        public FAbout(AboutViewModel viewModel)
        {
            InitializeComponent();

            ViewModel = viewModel;

            Binder.Create(ViewModel)
                  .Bind(p => p.Version).To(_version, p => p.Text).Go();
        }

        public AboutViewModel ViewModel { get; set; }
    }
}
