using System;
using PhonemeLattice.Core.DataProcessing;
using PhonemeLattice.Core.Lattice;

namespace PhonemeLattice.Lattice.Builder
{
    public sealed class LatticeFactory : ILatticeFactory
    {
        private readonly IDictionaryEntriesProvider _entryProvider;

        public LatticeFactory(IDictionaryEntriesProvider entryProvider)
        {
            if (entryProvider == null)
            {
                throw new ArgumentNullException("entryProvider");
            }

            _entryProvider = entryProvider;
        }

        public ILattice Build()
        {
            var builder = new LatticeBuilder();
            foreach (DictionaryEntry entry in _entryProvider.GetDictionaryEntries())
            {
                builder.AddWordEntry(entry);
            }
            var result = new Lattice(builder.StartNode);
            return result;
        }
    }
}