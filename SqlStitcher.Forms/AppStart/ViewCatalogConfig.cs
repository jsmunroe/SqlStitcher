using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.ServiceLocation;
using SqlStitcher.Forms.Infrastructure;
using SqlStitcher.Forms.ViewModels;
using SqlStitcher.Forms.Views;

namespace SqlStitcher.Forms.AppStart
{
    class ViewCatalogConfig
    {
        public static void Start()
        {
            var formCatalog = ServiceLocator.Current.GetInstance<ViewCatalog>();

            formCatalog.Register<MainViewModel, FMain>();
            formCatalog.Register<NewProjectViewModel, FNewProject>();
            formCatalog.Register<HomeViewModel, Home>();
            formCatalog.Register<AboutViewModel, FAbout>();
            formCatalog.Register<OptionsViewModel, FOptions>();
            formCatalog.Register<OptionsGeneralViewModel, OptionsGeneral>();
            formCatalog.Register<OptionsStitchingViewModel, OptionsStitching>();
            formCatalog.Register<ProjectOverviewViewModel, ProjectOverview>();
            formCatalog.Register<ProjectIdentifiersViewModel, FProjectIdentifiers>();
            formCatalog.Register<ProjectIdentifiersViewModel.IdentifierViewModel, Identifier>();
            formCatalog.Register<ScriptViewModel, FScript>();
        }
    }
}
