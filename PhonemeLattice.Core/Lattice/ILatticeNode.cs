using System.Collections.Generic;

namespace PhonemeLattice.Core.Lattice
{
    public interface ILatticeNode
    {
        /// <summary>
        /// Nodes reacheable from this one
        /// </summary>
        IEnumerable<ILatticeNode> NextNodes { get; }

        void Accept(INodeVisitor visitor);
    }
}