using System;
using PhonemeLattice.Core.Lattice;
using System.Collections.Generic;
using System.Linq;

namespace PhonemeLattice.Lattice
{
    [Serializable]
    internal sealed class WordNode : LatticeNode, IWordNode
    {
        private readonly int _wordId;

        public WordNode(IEnumerable<ILatticeNode> nextNodes, int wordId) : base(nextNodes)
        {
            _wordId = wordId;
        }

        public WordNode(int wordId) : this(Enumerable.Empty<ILatticeNode>(), wordId)
        {
        }

        public override void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }

        public int WordId
        {
            get { return _wordId; }
        }

        private bool Equals(WordNode other)
        {
            return base.Equals(other) && _wordId == other._wordId;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is WordNode && Equals((WordNode) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (base.GetHashCode()*397) ^ _wordId;
            }
        }
    }
}