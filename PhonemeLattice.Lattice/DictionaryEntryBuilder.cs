using System;
using System.Collections.Generic;
using PhonemeLattice.Core.DataProcessing;
using PhonemeLattice.Core.Lattice;

namespace PhonemeLattice.Lattice
{
    internal sealed class DictionaryEntryBuilder : INodeVisitor
    {
        private const string UnknownText = "<Unknown>";
        private int? _id;

        private readonly List<Phoneme> _phonemes; 

        public DictionaryEntryBuilder()
        {
            _phonemes = new List<Phoneme>();
        }

        public DictionaryEntryBuilder(DictionaryEntryBuilder other)
        {
            _id = other._id;
            _phonemes = new List<Phoneme>(other.Phonemes);
        }

        public IEnumerable<Phoneme> Phonemes { get { return _phonemes; } } 

        public void Visit(IBeginNode node)
        {
        }

        public void Visit(IWordNode node)
        {
            if(_id.HasValue)
                throw new InvalidOperationException("Double word node for a single word");

            _id = node.WordId;
        }

        public void Visit(IPhonemeNode node)
        {
            _phonemes.Add(node.Phoneme);
        }

        public DictionaryEntry ToDictionaryEntry()
        {
            if(!_id.HasValue)
                throw new InvalidOperationException("No word node found");

            var result = new DictionaryEntry(UnknownText, _phonemes.ToArray(), _id.Value);
            return result;
        }
    }
}