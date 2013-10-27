using NUnit.Framework;
using PhonemeLattice.Core.Lattice;
using PhonemeLattice.DataProcessing;
using PhonemeLattice.Lattice.Builder;

namespace PhonemeLattice.Tests.Lattice
{
    [TestFixture]
    public sealed class LatticePreserverTest
    {
        [Test]
        public void SaveLoadTest()
        {
            var phonemeStringProvider = new FileStringDataProvider("Phone.txt");
            var phonemeStorage = new PhonemeStorage(new PhonemeProvider(phonemeStringProvider));
            var dictionaryEntryStringProvider = new FileStringDataProvider("dict_test.txt");
            var dictionaryEntryProvider = new DictionaryEntryProvider(dictionaryEntryStringProvider, phonemeStorage);
            var factory = new LatticeFactory(dictionaryEntryProvider);
            ILattice lattice = factory.Build();

            var latticePreserver = new LatticeFilePreserver("lattice");
            latticePreserver.Save(lattice);
            var loadedLattice = latticePreserver.Load();

            Assert.AreEqual(lattice, loadedLattice);
        }
    }
}