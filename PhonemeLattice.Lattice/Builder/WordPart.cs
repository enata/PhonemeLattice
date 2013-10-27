using System;

namespace PhonemeLattice.Lattice.Builder
{
    internal struct WordPart
    {
        public readonly String Key;
        public readonly int Length;

        public WordPart(int length, string key) : this()
        {
            Length = length;
            Key = key;
        }
    }
}