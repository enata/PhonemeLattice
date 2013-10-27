namespace PhonemeLattice.Core.Lattice
{
    /// <summary>
    /// Contains word identifier
    /// </summary>
    public interface IWordNode : ILatticeNode
    {
        int WordId { get; }
    }
}