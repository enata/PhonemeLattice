using System.IO;
using System.Linq;
using NUnit.Framework;
using PhonemeLattice.Core;
using PhonemeLattice.Core.DataProcessing;
using PhonemeLattice.DataProcessing;
using Rhino.Mocks;

namespace PhonemeLattice.Tests.DataProcessing
{
    [TestFixture]
    public sealed class PhonemeProviderTests
    {
        [Test]
        public void GetPhonemesTest()
        {
            const string serializedPhoneme = @"	""a0"" //  5";
            var phonemeProvider = SetUpProvider(serializedPhoneme);

            var phonemes = phonemeProvider.GetPhonemes().ToArray();
            
            Assert.AreEqual(1, phonemes.Length);
            var phoneme = phonemes[0];
            Assert.AreEqual("a0", phoneme.TextRepresentation);
            Assert.AreEqual(5, phoneme.Id);
        }

        [Test]
        [ExpectedException(typeof (InvalidDataException))]
        public void GetPhonemeTestInvalidPhonemePartsExc()
        {
            const string serializedPhoneme = @"	""a0"" //  ";
            var phonemeProvider = SetUpProvider(serializedPhoneme);

            phonemeProvider.GetPhonemes().ToArray();
        }

        [Test]
        [ExpectedException(typeof (InvalidDataException))]
        public void GetPhonemeTestInvalidPhonemeIdExc()
        {
            const string serializedPhoneme = @"	a0 //  xx";
            var phonemeProvider = SetUpProvider(serializedPhoneme);

            phonemeProvider.GetPhonemes().ToArray();
        }

        [Test]
        [ExpectedException(typeof(InvalidDataException))]
        public void GetPhonemeTestInvalidPhonemeRepresenationExc()
        {
            const string serializedPhoneme = @"	a0 //  5";
            var phonemeProvider = SetUpProvider(serializedPhoneme);

            phonemeProvider.GetPhonemes().ToArray();
        }

        private PhonemeProvider SetUpProvider(params string[] strs)
        {
            var stringProvider = MockRepository.GenerateStub<IStringDataProvider>();
            stringProvider.Stub(sp => sp.GetStrings())
                          .Return(strs);
            var result = new PhonemeProvider(stringProvider);
            return result;
        }
    }
}