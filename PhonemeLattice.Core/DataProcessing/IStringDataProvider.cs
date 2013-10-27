using System.Collections.Generic;

namespace PhonemeLattice.Core.DataProcessing
{
    /// <summary>
    /// Acquires raw string data
    /// </summary>
    public interface IStringDataProvider
    {
        /// <summary>
        /// Gets data entries serialized to string
        /// </summary>
        IEnumerable<string> GetStrings();
    }
}