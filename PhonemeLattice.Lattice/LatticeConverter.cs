using System;
using System.Collections.Generic;
using System.Linq;
using PhonemeLattice.Core.DataProcessing;
using PhonemeLattice.Core.Lattice;

namespace PhonemeLattice.Lattice
{
    public sealed class LatticeConverter : ILatticeWalker
    {
        public IEnumerable<DictionaryEntry> ConvertLattice(ILattice lattice)
        {
            if (lattice == null)
            {
                throw new ArgumentNullException("lattice");
            }

            var initialBuilder = new DictionaryEntryBuilder();
            var entries = ProcessNode(lattice.StartNode, initialBuilder);
            return entries;
        }

        private IEnumerable<DictionaryEntry> ProcessNode(ILatticeNode node, DictionaryEntryBuilder activeBuilder)
        {

            node.Accept(activeBuilder);
            if (!node.NextNodes.Any())
            {
                return new[] {activeBuilder.ToDictionaryEntry()};
            }

            return
                node.NextNodes.SelectMany(nextNode => ProcessNode(nextNode, new DictionaryEntryBuilder(activeBuilder)));
        }
    }
}