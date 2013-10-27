using System;
using System.Collections.Generic;
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
    public sealed class DictionaryEntryProviderTests
    {
        private DictionaryEntryProvider GetProvider(IEnumerable<string> rawStrings, IEnumerable<Phoneme> phonemes)
        {
            var stringProvider = MockRepository.GenerateStub<IStringDataProvider>();
            stringProvider.Stub(sp => sp.GetStrings())
                          .Return(rawStrings);
            var phonemeStorage = MockRepository.GenerateStub<IPhonemeStorage>();
            phonemeStorage.Stub(ps => ps[Arg<string>.Is.Anything])
                          .Do(new Func<string, Phoneme>(str => phonemes.First(p => p.TextRepresentation == str)));

            var result = new DictionaryEntryProvider(stringProvider, phonemeStorage);
            return result;
        }

        [Test]
        public void GetDictionaryEntriesTest()
        {
            const string serializedData = @"ёж=j o0 sh#2";
            var phonemes = new[] {new Phoneme(1, "j"), new Phoneme(2, "o0"), new Phoneme(3, "sh")};
            DictionaryEntryProvider provider = GetProvider(new[] {serializedData}, phonemes);

            DictionaryEntry[] dictionaryEntries = provider.GetDictionaryEntries()
                                                          .ToArray();

            Assert.AreEqual(1, dictionaryEntries.Length);
            DictionaryEntry entry = dictionaryEntries[0];
            Assert.AreEqual("ёж", entry.Text);
            Assert.AreEqual(2, entry.Id);
            Assert.AreEqual(3, entry.Phonemes.Length);
            Assert.AreEqual(1, entry.Phonemes[0].Id);
            Assert.AreEqual(2, entry.Phonemes[1].Id);
            Assert.AreEqual(3, entry.Phonemes[2].Id);
        }

        [Test]
        [ExpectedException(typeof (InvalidDataException))]
        public void GetDictionaryEntriesTestInvalidDataExc()
        {
            const string serializedData = @"ёж=j";
            DictionaryEntryProvider provider = GetProvider(new[] {serializedData}, new Phoneme[0]);

            provider.GetDictionaryEntries()
                    .ToArray();
        }

        [Test]
        [ExpectedException(typeof (InvalidDataException))]
        public void GetDictionaryEntriesTestInvalidIdExc()
        {
            const string serializedData = @"ёж=j o0 sh#xx";
            var phonemes = new[] {new Phoneme(1, "j"), new Phoneme(2, "o0"), new Phoneme(3, "sh")};
            DictionaryEntryProvider provider = GetProvider(new[] {serializedData}, phonemes);

            provider.GetDictionaryEntries()
                    .ToArray();
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetDictionaryEntriesTestPhonemeNotFoundExc()
        {
            const string serializedData = @"ёж=j o0 sh#2";
            var phonemes = new[] { new Phoneme(1, "j"), new Phoneme(3, "sh") };
            DictionaryEntryProvider provider = GetProvider(new[] { serializedData }, phonemes);

            provider.GetDictionaryEntries()
                    .ToArray();
        }
    }
}