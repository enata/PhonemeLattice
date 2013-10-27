using System.Collections.Generic;

namespace PhonemeLattice.Core.DataProcessing
{
    public interface IDictionaryEntriesProvider
    {
        /// <summary>
        /// Obtains dictionary entries from a storage
        /// </summary>
        IEnumerable<DictionaryEntry> GetDictionaryEntries();
    }
}