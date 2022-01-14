using System.Collections.Generic;
using Helpers.Contracts;
using Helpers.IO;

namespace SqlStitcher.Forms.Contracts
{
    public interface IRecentFileList : IEnumerable<string>
    {
        void Push(string file);
        void Remove(string file);
    }
}