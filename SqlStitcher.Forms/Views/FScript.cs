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
    public partial class FScript : Form, IForm<ScriptViewModel>
    {
        public FScript(ScriptViewModel viewModel)
        {
            InitializeComponent();

            FormSizer.From(this)
                .Size(p => p.FScripts_Size)
                .Location(p => p.FScripts_Location)
                .Maximized(p => p.FScripts_IsMaximized);

            ViewModel = viewModel;

            Binder.Create(ViewModel)
                  .Bind(p => p.ScriptText).To(_script, p => p.Text).Go();

            _script.Text = ViewModel.ScriptText;
        }

        public ScriptViewModel ViewModel { get; set; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            _script.SelectionStart = 0;
            _script.SelectionLength = 0;
        }

        private void _script_TextChanged(object sender, EventArgs e)
        {
            ViewModel.ScriptText = _script.Text;
        }

        private void _sendToClipboard_Click(object sender, EventArgs e)
        {
            ViewModel.CopyToClipboard();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            ViewModel.Dispose();
        }
    }
}
