using System;
using System.Linq;

namespace PhonemeLattice.Core.DataProcessing
{
    public sealed class DictionaryEntry
    {
        private readonly int _id;
        private readonly Phoneme[] _phonemes;
        private readonly string _text;

        public DictionaryEntry(string text, Phoneme[] phonemes, int id)
        {
            if (phonemes == null)
            {
                throw new ArgumentNullException("phonemes");
            }

            _text = text;
            _phonemes = phonemes;
            _id = id;
        }

        public string Text
        {
            get { return _text; }
        }

        public Phoneme[] Phonemes
        {
            get { return _phonemes; }
        }

        public int Id
        {
            get { return _id; }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (obj.GetType() != typeof(DictionaryEntry)) return false;
            if (ReferenceEquals(this, obj)) return true;
            var other = (DictionaryEntry) obj;
            var result = (_phonemes.SequenceEqual(other._phonemes) && _id == other._id);
            return result;
        }

        public override int GetHashCode()
        {
            return _id;
        }
    }
}