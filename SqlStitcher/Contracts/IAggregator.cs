using System.Collections.Generic;
using SqlStitcher.Models;

namespace SqlStitcher.Contracts
{
    public interface IAggregator
    {
        /// <summary>
        /// Get the entries found in this aggregator.
        /// </summary>
        /// <returns>Entries found in this aggregator.</returns>
        IEnumerable<ScriptEntry> GetEntries();
    }
}