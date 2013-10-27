using System;
using System.Collections.Generic;
using PhonemeLattice.Core.Lattice;

namespace PhonemeLattice.Lattice
{
    [Serializable]
    internal sealed class BeginNode : LatticeNode, IBeginNode
    {
        public BeginNode(IEnumerable<ILatticeNode> nextNodes) : base(nextNodes)
        {
        }

        public BeginNode()
        {
        }

        public override void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}