using NUnit.Framework;
using PhonemeLattice.Core;
using PhonemeLattice.Core.DataProcessing;
using PhonemeLattice.DataProcessing;
using Rhino.Mocks;

namespace PhonemeLattice.Tests.DataProcessing
{
    [TestFixture]
    public sealed class PhonemeStorageTest
    {
        [Test]
        public void IndexerTest()
        {
            var phoneme = new Phoneme(3, "a");
            var storage = GetStorage(phoneme);

            var storedPhoneme = storage["a"];

            Assert.AreEqual(3, storedPhoneme.Id);
        }

        [Test]
        [ExpectedException]
        public void IndexerTestNoEntryErr()
        {
            var phoneme = new Phoneme(3, "a");
            var storage = GetStorage(phoneme);

            var storedPhoneme = storage["b"];
        }

        private PhonemeStorage GetStorage(params Phoneme[] phonemes)
        {
            var phonemesProvider = MockRepository.GenerateStub<IPhonemeProvider>();
            phonemesProvider.Stub(pp => pp.GetPhonemes())
                            .Return(phonemes);
            var result = new PhonemeStorage(phonemesProvider);
            return result;
        }
    }
}