using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Helpers.Archetecture;
using Microsoft.Practices.ServiceLocation;
using SqlStitcher.Forms.Annotations;
using SqlStitcher.Forms.Contracts;

namespace SqlStitcher.Forms.Infrastructure
{
    public class BaseViewModel : INotifyPropertyChanged, IDisposable
    {
        private string _errorMessage;

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Constructor.
        /// </summary>
        public BaseViewModel()
        {
            Container = ServiceLocator.Current;
            ViewCatalog = Container.GetInstance<IViewCatalog>();
            Messenger = Container.GetInstance<IMessenger>();
        }

        protected IServiceLocator Container { get; private set; }
        protected IViewCatalog ViewCatalog { get; private set; }
        protected IMessenger Messenger { get; private set; }

        /// <summary>
        /// View model error message.
        /// </summary>
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// Rais the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        [NotifyPropertyChangedInvocator]
        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void Disposing()
        {
            // Optionally handled by subclasses.
        }

        /// <summary>
        /// Dispose of this view model.
        /// </summary>
        public void Dispose()
        {
            Messenger.Unsubscribe(this);

            Disposing();
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Validate this view model.
        /// </summary>
        /// <returns>Validation result.</returns>
        public virtual Result Validate()
        {
            // Optionally handled by subclasses.
            return true;
        }
    }
}
