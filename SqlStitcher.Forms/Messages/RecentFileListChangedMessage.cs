using System.Collections.Generic;
using SqlStitcher.Forms.Contracts;
using SqlStitcher.Forms.Infrastructure;

namespace SqlStitcher.Forms.Messages
{
    public class RecentFileListChangedMessage
    {
        public IEnumerable<string> MostRecentFilePaths { get; private set; }

        public RecentFileListChangedMessage(IEnumerable<string> mostRecentFilePaths)
        {
            MostRecentFilePaths = mostRecentFilePaths;
        }
    }
}