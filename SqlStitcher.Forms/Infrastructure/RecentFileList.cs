using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helpers.Contracts;
using Helpers.Extensions;
using SqlStitcher.Forms.Contracts;
using SqlStitcher.Forms.Messages;

namespace SqlStitcher.Forms.Infrastructure
{
    public class RecentFileList : IRecentFileList
    {
        private readonly ISettings _settings;
        private readonly IMessenger _messenger;

        private readonly List<string> _files = new List<string>();

        private readonly int _maxLength;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="settings">Application settings.</param>
        /// <param name="messenger">Application messenger.</param>
        public RecentFileList(ISettings settings, IMessenger messenger)
        {
            _settings = settings;
            _messenger = messenger;
            _maxLength = settings.RecentListLength;

            Load();
        }

        /// <summary>
        /// Push the given file (<paramref name="file"/>) into the top of this list.
        /// </summary>
        /// <param name="file">Pushed file.</param>
        public void Push(string file)
        {
            _files.RemoveWhere(p => string.Equals(p, file, StringComparison.OrdinalIgnoreCase));

            _files.Insert(0, file);

            while (_files.Count > _maxLength)
                _files.RemoveAt(_files.Count - 1);

            Save();

            _messenger.Publish(new RecentFileListChangedMessage(this));
        }

        /// <summary>
        /// Remove the given file (<paramref name="file"/>) from the list.
        /// </summary>
        /// <param name="file">File to remove.</param>
        public void Remove(string file)
        {
            _files.RemoveAll(p => StringComparer.OrdinalIgnoreCase.Equals(file, p));

            Save();
            
            _messenger.Publish(new RecentFileListChangedMessage(this));
        }

        /// <summary>
        /// Load the list.
        /// </summary>
        private void Load()
        {
            var list = _settings.RecentList.Split(new[] {";"}, StringSplitOptions.RemoveEmptyEntries);
            _files.AddRange(list);
        }

        /// <summary>
        /// Save the list.
        /// </summary>
        private void Save()
        {
            _settings.RecentList = string.Join(";", _files);
            _settings.Save();
        }

        #region IEnumerator Members
        
        public IEnumerator<string> GetEnumerator()
        {
            return _files.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
     
	    #endregion
    }
}
