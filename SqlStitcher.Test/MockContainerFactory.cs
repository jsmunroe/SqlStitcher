using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Moq;
using SqlStitcher.Forms.Contracts;

namespace SqlStitcher.Test
{
    public class MockContainerFactory
    {
        private UnityContainer _container;

        public Mock<IViewCatalog> FormCatalog { get; set; }
        public Mock<IProjectState> ProjectState { get; set; }
        public Mock<IMessenger> Messenger { get; set; }
        public Mock<IClipboarder> Clipboarder { get; set; }

        public MockContainerFactory()
        {
            FormCatalog = new Mock<IViewCatalog>();
            ProjectState = new Mock<IProjectState>();
            Messenger = new Mock<IMessenger>();
            Clipboarder = new Mock<IClipboarder>();
        }

        public void Create()
        {
            _container = new UnityContainer();
            _container.RegisterInstance(FormCatalog.Object);
            _container.RegisterInstance(ProjectState.Object);
            _container.RegisterInstance(Messenger.Object);
            _container.RegisterInstance(Clipboarder.Object);

            ServiceLocator.SetLocatorProvider(() => new UnityServiceLocator(_container));
        }

        public void OverrideInstance<TInstance>(TInstance instance)
        {
            _container.RegisterInstance(instance);
        }

        public void OverrideInstance<TInstance>(TInstance instance, LifetimeManager manager)
        {
            _container.RegisterInstance(instance, manager);
        }

    }
}
