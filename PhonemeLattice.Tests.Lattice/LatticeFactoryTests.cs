using NUnit.Framework;
using PhonemeLattice.Core.DataProcessing;
using PhonemeLattice.Core.Lattice;
using PhonemeLattice.DataProcessing;
using PhonemeLattice.Lattice;
using PhonemeLattice.Lattice.Builder;
using System.Collections.Generic;

namespace PhonemeLattice.Tests.Lattice
{
    [TestFixture]
    public sealed class LatticeFactoryTests
    {
        [Test]
        public void CorectnessTest()
        {
            var phonemeStringProvider = new FileStringDataProvider("Phone.txt");
            var phonemeStorage = new PhonemeStorage(new PhonemeProvider(phonemeStringProvider));
            var dictionaryEntryStringProvider = new FileStringDataProvider("dictionary_rs_19935_strict.txt");
            var dictionaryEntryProvider = new DictionaryEntryProvider(dictionaryEntryStringProvider, phonemeStorage);
            var factory = new LatticeFactory(dictionaryEntryProvider);
            ILattice lattice = factory.Build();
            var backConverter = new LatticeConverter();
            var converted = new HashSet<DictionaryEntry>(backConverter.ConvertLattice(lattice));
            var initial = new HashSet<DictionaryEntry>(dictionaryEntryProvider.GetDictionaryEntries());

            Assert.AreEqual(initial.Count, converted.Count);
            foreach (DictionaryEntry entry in initial)
            {
                Assert.IsTrue(converted.Contains(entry));
            }
        }
    }
}