namespace PhonemeLattice.Core.Lattice
{
    public interface INodeVisitor
    {
        void Visit(IBeginNode node);
        void Visit(IWordNode node);
        void Visit(IPhonemeNode node);
    }
}