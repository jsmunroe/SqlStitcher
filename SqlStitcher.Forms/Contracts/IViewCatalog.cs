using System;
using SqlStitcher.Forms.Contracts;

namespace SqlStitcher.Forms.Contracts
{
    public interface IViewCatalog
    {
        /// <summary>
        /// Register the given view (<typeparamref name="TView"/>) type under the given 
        ///     view model type (<typeparamref name="TViewModel"/>.
        /// </summary>
        /// <typeparam name="TViewModel">Type of view model.</typeparam>
        /// <typeparam name="TView">Type of view.</typeparam>
        void Register<TViewModel, TView>()
            where TView : IView<TViewModel>;

        /// <summary>
        /// Resolve the view registered under the tyep of the given view model 
        ///     (<paramref name="viewModel"/>) assigning it as the views view model.
        /// </summary>
        /// <typeparam name="TViewModel">Type of view model.</typeparam>
        /// <param name="viewModel">View model.</param>
        /// <returns>Resolved view.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="viewModel"/> is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown if type of <paramref name="viewModel"/> is not registered.</exception>
        IView<TViewModel> Resolve<TViewModel>(TViewModel viewModel);

        /// <summary>
        /// Indicate whether the type of the given view model (<paramref name="viewModel"/>) has 
        ///     been registered herein.
        /// </summary>
        /// <typeparam name="TViewModel">Type of view model.</typeparam>
        /// <param name="viewModel">View model.</param>
        /// <returns>True if the view model has been registered.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="viewModel"/> is null.</exception>
        bool IsRegistered<TViewModel>(TViewModel viewModel);
    }
}