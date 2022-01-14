using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SqlStitcher.Forms.Contracts;
using SqlStitcher.Forms.ViewModels;

namespace SqlStitcher.Forms.Infrastructure
{
    public class ViewCatalog : IViewCatalog
    {
        private readonly List<CatalogEntry> _forms = new List<CatalogEntry>();
        
        /// <summary>
        /// Registera the given view type (<typeparamref name="TView"/>) under the given 
        ///     view model type (<typeparamref name="TViewModel"/>.
        /// </summary>
        /// <typeparam name="TViewModel">Type of view model.</typeparam>
        /// <typeparam name="TView">Type of form.</typeparam>
        public void Register<TViewModel, TView>()
            where TView : IView<TViewModel>
        {
            if (ResolveEntry(typeof(TViewModel)) != null)
            {
                throw new InvalidOperationException(string.Format("Form was already resolved under the \"{0}\" view model.", typeof(TViewModel).Name));
            }

            var entry = new CatalogEntry
            {
                ViewModelType = typeof(TViewModel),
                ViewType = typeof(TView),
            };

            _forms.Add(entry);
        }

        /// <summary>
        /// Resolve the form registered under the tyep of the given view model 
        ///     (<paramref name="viewModel"/>) assigning it as the forms view model.
        /// </summary>
        /// <typeparam name="TViewModel">Type of view model.</typeparam>
        /// <param name="viewModel">View model.</param>
        /// <returns>Resolved form.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="viewModel"/> is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown if type of <paramref name="viewModel"/> is not registered.</exception>
        public IView<TViewModel> Resolve<TViewModel>(TViewModel viewModel)
        {
            #region Argument Validation

            if (viewModel == null)
            {
                throw new ArgumentNullException("viewModel");
            }

            #endregion

            var viewModelType = viewModel.GetType();

            var entry = ResolveEntry(viewModelType);

            if (entry == null)
            {
                throw new InvalidOperationException(string.Format("No form has been registered under the \"{0}\" view model.", viewModelType.Name));
            }

            IView<TViewModel> view = null;

            if (entry.ViewType.GetConstructors().Any(p => IsViewModelConstructor(p, viewModelType)))
                view = Activator.CreateInstance(entry.ViewType, viewModel) as IView<TViewModel>;
            else
                view = Activator.CreateInstance(entry.ViewType) as IView<TViewModel>;

            if (view == null)
            {
                // This should never happen as the consumer cannot register a type that isn't a 
                //  subclass of IView(T). I am adding this for clarity.
                throw new InvalidOperationException("Resolved form was not a \"IView(T)\"!");
            }

            view.ViewModel = viewModel;

            return view;
        }

        /// <summary>
        /// Indicate whether the type of the given view model (<paramref name="viewModel"/>) has 
        ///     been registered herein.
        /// </summary>
        /// <typeparam name="TViewModel">Type of view model.</typeparam>
        /// <param name="viewModel">View model.</param>
        /// <returns>True if the view model has been registered.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="viewModel"/> is null.</exception>
        public bool IsRegistered<TViewModel>(TViewModel viewModel)
        {
            #region Argument Validation

            if (viewModel == null)
            {
                throw new ArgumentNullException("viewModel");
            }

            #endregion

            var viewModelType = viewModel.GetType();

            var entry = ResolveEntry(viewModelType);

            if (entry == null)
            {
                return false;
            }

            return true;
        }

        private bool IsViewModelConstructor(ConstructorInfo constructor, Type viewModelType)
        {
            if (!constructor.IsPublic)
            {
                return false;
            }

            var parameters = constructor.GetParameters();
            if (parameters.Length != 1)
            {
                return false;
            }

            return parameters[0].ParameterType.IsAssignableFrom(viewModelType);
        }

        private CatalogEntry ResolveEntry(Type viewModelType)
        {
            var entry = _forms.FirstOrDefault(p => viewModelType.IsAssignableFrom(p.ViewModelType));

            return entry;
        }

        class CatalogEntry
        {
            public Type ViewModelType { get; set; }
            public Type ViewType { get; set; }
        }
    }
}
