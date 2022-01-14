using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SqlStitcher.Forms.Annotations;

namespace SqlStitcher.Forms.Contracts
{
    public static class ViewExtensions
    {
        /// <summary>
        /// Show "this" view as a dialog.
        /// </summary>
        /// <typeparam name="TViewModel">Type of view model.</typeparam>
        /// <param name="view">"this" view.</param>
        /// <param name="parentWindow">Parent window.</param>
        /// <returns>Dialog results.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="view"/> is null.</exception>
        public static DialogResult ShowDialog<TViewModel>(this IView<TViewModel> view, IWin32Window parentWindow = null)
        {
            #region Argument Validation

            if (view == null)
            {
                throw new NullReferenceException("view");
            }

            #endregion

            var form = view as IForm<TViewModel>;

            if (form == null)
            {
                throw new InvalidOperationException("The view is not a form and cannot be shown.");
            }

            return form.ShowDialog(parentWindow);
        }

        /// <summary>
        /// Show "this" view.
        /// </summary>
        /// <typeparam name="TViewModel">Type of view model.</typeparam>
        /// <param name="view">"this" view.</param>
        /// <param name="parentWindow">Parent window.</param>
        /// <returns>Dialog results.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="view"/> is null.</exception>
        public static void Show<TViewModel>(this IView<TViewModel> view, IWin32Window parentWindow = null)
        {
            #region Argument Validation

            if (view == null)
            {
                throw new NullReferenceException("view");
            }

            #endregion

            var form = view as IForm<TViewModel>;

            if (form == null)
            {
                throw new InvalidOperationException("The view is not a form and cannot be shown.");
            }

            form.Show(parentWindow);
        }

        /// <summary>
        /// Convert this view to a user control.
        /// </summary>
        /// <typeparam name="TViewModel">Type of view model.</typeparam>
        /// <param name="view">"this" view.</param>
        /// <returns>User control.</returns>
        [NotNull]
        public static UserControl AsUserControl<TViewModel>(this IView<TViewModel> view)
        {
            var userControl = view as UserControl;

            if (userControl == null)
            {
                throw new InvalidOperationException("The view is not a user control.");
            }

            return userControl;
        }
    }
}
