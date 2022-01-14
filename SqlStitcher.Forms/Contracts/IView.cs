using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SqlStitcher.Forms.Contracts
{
    public interface IView<TViewModel>
    {
        TViewModel ViewModel { get; set; }
    }

    public interface IForm<TViewModel> : IView<TViewModel>
    {
        void Show(IWin32Window owner);
        DialogResult ShowDialog();
        DialogResult ShowDialog(IWin32Window owner);
    }

    public interface IUserControl<TViewModel> : IView<TViewModel>
    {
        
    }
}
