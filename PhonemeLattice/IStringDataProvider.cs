using System.Collections.Generic;

namespace PhonemeLattice
{
    /// <summary>
    /// Acquires raw string data
    /// </summary>
    public interface IStringDataProvider
    {
        /// <summary>
        /// Gets data entries serialized to string
        /// </summary>
        /// <returns></returns>
        IEnumerable<string> GetStrings();
    }
}