using System.Collections.Generic;
using PhonemeLattice.Core.DataProcessing;

namespace PhonemeLattice.DataProcessing
{
    public sealed class DictionaryEntryProvider : EntityProvider<DictionaryEntry>, IDictionaryEntriesProvider
    {
        private readonly DictionaryEntryDeserializer _entryDeserializer;

        public DictionaryEntryProvider(IStringDataProvider serializedEntriesProvider, IPhonemeStorage phonemeStorage)
            : base(serializedEntriesProvider)
        {
            _entryDeserializer = new DictionaryEntryDeserializer(phonemeStorage);
        }


        protected override RawStringDataDeserializer<DictionaryEntry> EntityDeserializer
        {
            get { return _entryDeserializer; }
        }

        /// <summary>
        ///     Obtains dictionary entries from a storage
        /// </summary>
        public IEnumerable<DictionaryEntry> GetDictionaryEntries()
        {
            return GetEntities();
        }
    }
}