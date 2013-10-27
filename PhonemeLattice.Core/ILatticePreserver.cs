using PhonemeLattice.Core.Lattice;

namespace PhonemeLattice.Core
{
    public interface ILatticePreserver
    {
        void Save(ILattice lattice);
        ILattice Load();
    }
}