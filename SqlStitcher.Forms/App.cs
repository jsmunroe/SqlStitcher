using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Helpers.Contracts;
using Helpers.IO;
using Microsoft.Practices.ServiceLocation;
using SqlStitcher.Forms.AppStart;
using SqlStitcher.Forms.Contracts;
using SqlStitcher.Forms.Properties;
using SqlStitcher.Forms.ViewModels;
using SqlStitcher.Forms.Views;

namespace SqlStitcher.Forms
{
    public class App : IApp
    {
        private static App _current = null;

        /// <summary>
        /// The current App instance.
        /// </summary>
        public static App Current
        {
            get { return _current ?? (_current = new App()); }
        }

        /// <summary>
        /// Application settings.
        /// </summary>
        public Settings Settings { get; set; }
        
        /// <summary>
        /// Main form of the application.
        /// </summary>
        public Form MainForm { get; set; }

        /// <summary>
        /// Bootstrap the application.
        /// </summary>
        public void Bootstrap()
        {
            Settings = new Settings();

            ContainerConfig.Start();
            ViewCatalogConfig.Start();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var mainForm = new FMain
            {
                ViewModel = new MainViewModel()
            };

            Application.Run(MainForm = mainForm);
        }

        /// <summary>
        /// Get the path to the projects directory.
        /// </summary>
        /// <returns>Path to projects directory.</returns>
        public string GetProjectsDirectoryPath()
        {
            if (string.IsNullOrWhiteSpace(Settings.ProjectsDirectoryPath))
            {
                Settings.ProjectsDirectoryPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\SQL Stitcher Projects";
            }

            Directory.CreateDirectory(Settings.ProjectsDirectoryPath);

            return Settings.ProjectsDirectoryPath;
        }

        /// <summary>
        /// Get the <see cref="BuildOptions"/> based on the current settings.
        /// </summary>
        public BuildOptions GetScriptBuildOptions()
        {
            return new BuildOptions
            {
                InsertGoCommand = Current.Settings.InsertGoCommand,
                PrependScriptDescription = Current.Settings.PrependScriptDescription,
            };
        }

        /// <summary>
        /// Exit the application.
        /// </summary>
        public void Exit()
        {
            Application.ExitThread();
        }

        #region IApp Members
        
        ISettings IApp.Settings { get { return Settings; } }

        #endregion

    }
}
