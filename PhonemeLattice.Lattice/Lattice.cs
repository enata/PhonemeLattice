using System;
using PhonemeLattice.Core.Lattice;

namespace PhonemeLattice.Lattice
{
    [Serializable]
    internal sealed class Lattice : ILattice
    {
        private readonly IBeginNode _startNode;

        public Lattice(IBeginNode startNode)
        {
            _startNode = startNode;
        }

        public IBeginNode StartNode
        {
            get { return _startNode; }
        }

        private bool Equals(Lattice other)
        {
            return Equals(_startNode, other._startNode);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is Lattice && Equals((Lattice) obj);
        }

        public override int GetHashCode()
        {
            return (_startNode != null ? _startNode.GetHashCode() : 0);
        }
    }
}