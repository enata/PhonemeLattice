namespace PhonemeLattice.Lattice.Builder
{
    internal sealed class WordDivision
    {
        private readonly int _middlePartLength;
        private readonly int _middlePartStart;
        private readonly LatticeNode _postfix;
        private readonly LatticeNode _prefix;

        public WordDivision(LatticeNode prefix, LatticeNode postfix, int middlePartLength, int middlePartStart)
        {
            _prefix = prefix;
            _postfix = postfix;
            _middlePartLength = middlePartLength;
            _middlePartStart = middlePartStart;
        }

        public LatticeNode Prefix
        {
            get { return _prefix; }
        }

        public LatticeNode Postfix
        {
            get { return _postfix; }
        }

        public int MiddlePartLength
        {
            get { return _middlePartLength; }
        }

        public int MiddlePartStart
        {
            get { return _middlePartStart; }
        }
    }
}