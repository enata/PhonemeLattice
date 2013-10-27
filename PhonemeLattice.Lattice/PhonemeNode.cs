using System;
using PhonemeLattice.Core.DataProcessing;
using PhonemeLattice.Core.Lattice;
using System.Collections.Generic;
using System.Linq;

namespace PhonemeLattice.Lattice
{
    [Serializable]
    internal sealed class PhonemeNode : LatticeNode, IPhonemeNode
    {
        private readonly Phoneme _phoneme;

        public PhonemeNode(IEnumerable<ILatticeNode> nextNodes, Phoneme phoneme) : base(nextNodes)
        {
            _phoneme = phoneme;
        }

        public PhonemeNode(Phoneme phoneme) : this(Enumerable.Empty<ILatticeNode>(), phoneme)
        {}

        public override void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }

        public Phoneme Phoneme
        {
            get { return _phoneme; }
        }

        private bool Equals(PhonemeNode other)
        {
            return base.Equals(other) && _phoneme.Equals(other._phoneme);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is PhonemeNode && Equals((PhonemeNode) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (base.GetHashCode()*397) ^ _phoneme.GetHashCode();
            }
        }
    }
}