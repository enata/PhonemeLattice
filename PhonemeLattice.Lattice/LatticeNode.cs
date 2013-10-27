using System;
using System.Collections.Generic;
using System.Linq;
using PhonemeLattice.Core.Lattice;

namespace PhonemeLattice.Lattice
{
    [Serializable]
    internal abstract class LatticeNode : ILatticeNode
    {
        private readonly List<ILatticeNode> _nextNodes;

        protected LatticeNode(IEnumerable<ILatticeNode> nextNodes)
        {
            _nextNodes = nextNodes.ToList();
        }

        protected LatticeNode()
        {
            _nextNodes = new List<ILatticeNode>();
        }

        public IEnumerable<ILatticeNode> NextNodes
        {
            get { return _nextNodes; }
        }

        public abstract void Accept(INodeVisitor visitor);

        public void AddNextNode(ILatticeNode node)
        {
            _nextNodes.Add(node);
        }

        protected bool Equals(LatticeNode other)
        {
            return _nextNodes.SequenceEqual(other._nextNodes);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((LatticeNode) obj);
        }

        public override int GetHashCode()
        {
            return _nextNodes.Count;
        }
    }
}