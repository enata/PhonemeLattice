using System.Collections.Generic;
using PhonemeLattice.Core.DataProcessing;

namespace PhonemeLattice.Core.Lattice
{
    public interface ILatticeWalker
    {
        /// <summary>
        /// Converts lattice to collection of dictionary entries
        /// </summary>
        IEnumerable<DictionaryEntry> ConvertLattice(ILattice lattice);
    }
}