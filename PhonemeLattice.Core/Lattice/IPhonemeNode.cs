using PhonemeLattice.Core.DataProcessing;

namespace PhonemeLattice.Core.Lattice
{
    /// <summary>
    /// Contains phoneme
    /// </summary>
    public interface IPhonemeNode : ILatticeNode
    {
        Phoneme Phoneme { get; }
    }
}