using System;
using System.Collections.Generic;
using System.Linq;
using PhonemeLattice.Core.DataProcessing;

namespace PhonemeLattice.DataProcessing
{
    public abstract class EntityProvider<T>
    {
        private readonly IStringDataProvider _serializedPhonemesProvider;

        protected EntityProvider(IStringDataProvider serializedPhonemesProvider)
        {
            if (serializedPhonemesProvider == null)
                throw new ArgumentNullException("serializedPhonemesProvider");

            _serializedPhonemesProvider = serializedPhonemesProvider;
        }

        protected abstract RawStringDataDeserializer<T> EntityDeserializer { get; }

        public IEnumerable<T> GetEntities()
        {
            IEnumerable<string> serializedEntities = _serializedPhonemesProvider.GetStrings();
            IEnumerable<T> phonemes = serializedEntities.Select(phonemeStr => EntityDeserializer.Deserialize(phonemeStr));
            return phonemes;
        }
    }
}