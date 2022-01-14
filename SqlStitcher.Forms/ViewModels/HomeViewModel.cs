using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.ServiceLocation;
using SqlStitcher.Forms.Contracts;
using SqlStitcher.Forms.Infrastructure;
using SqlStitcher.Forms.Messages;

namespace SqlStitcher.Forms.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public List<string> ProjectFiles { get; private set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public HomeViewModel()
        {
            var recentFilesList = ServiceLocator.Current.GetInstance<IRecentFileList>();
            ProjectFiles = recentFilesList.ToList();

            Messenger.Subscribe<RecentFileListChangedMessage>(this, OnMessageRecieved);
        }

        /// <summary>
        /// When the given <see cref="RecentFileListChangedMessage"/> (<paramref name="message"/>) 
        ///     has been recieved.
        /// </summary>
        /// <param name="message">Recieved message.</param>
        private void OnMessageRecieved(RecentFileListChangedMessage message)
        {
            ProjectFiles = message.MostRecentFilePaths.ToList();
            RaisePropertyChanged("ProjectFiles");
        }

        /// <summary>
        /// Open the given project file (<paramref name="projectFile"/>).
        /// </summary>
        /// <param name="projectFile">Project file.</param>
        public void OpenProject(string projectFile)
        {
            Messenger.Publish(new OpenRecentProjectMessage { ProjectFile = projectFile });
        }
    }
}
