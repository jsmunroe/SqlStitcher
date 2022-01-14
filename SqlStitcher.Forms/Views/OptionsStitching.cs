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
using SqlStitcher.Forms.Helpers;
using SqlStitcher.Forms.Infrastructure;
using SqlStitcher.Forms.Infrastructure.ValueConverters;
using SqlStitcher.Forms.ViewModels;

namespace SqlStitcher.Forms.Views
{
    public partial class OptionsStitching : UserControl, IUserControl<OptionsStitchingViewModel>
    {
        public OptionsStitching()
        {
            InitializeComponent();
        }

        public OptionsStitchingViewModel ViewModel { get; set; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            _prependScriptComment.BindEnum<PrependDescription>();

            Binder.Create(ViewModel)
                  .Bind(p => p.InsertGoCommand).To(_insertGoCommand, p => p.CheckState, CheckStateConverter.Default).AndBack().Go()
                  .Bind(p => p.PrependScriptComment).To(_prependScriptComment, p => p.SelectedValue, EnumValueConverter<PrependDescription>.Default).AndBack().Go();
        }
    }
}
