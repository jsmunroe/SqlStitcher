using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using SqlStitcher.Forms.Infrastructure;
using SqlStitcher.Forms.Contracts;
using SqlStitcher.Forms.Properties;

namespace SqlStitcher.Forms.AppStart
{
    public static class ContainerConfig
    {
        public static IServiceLocator Start()
        {
            var container = new UnityContainer();

            // Register types.
            container.RegisterInstance<IApp>(App.Current);
            container.RegisterInstance<ISettings>(App.Current.Settings);
            container.RegisterType<IViewCatalog, ViewCatalog>(new ContainerControlledLifetimeManager());
            container.RegisterType<IProjectState, ProjectState>(new ContainerControlledLifetimeManager());
            container.RegisterType<IMessenger, Messenger>(new ContainerControlledLifetimeManager());
            container.RegisterType<IClipboarder, Clipboarder>();
            container.RegisterType<IRecentFileList, RecentFileList>(new ContainerControlledLifetimeManager());

            var serviceLocator = new UnityServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => serviceLocator);

            return serviceLocator;
        }
    }
}
