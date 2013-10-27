using System;
using System.IO;
using System.Linq;
using PhonemeLattice.Core.DataProcessing;

namespace PhonemeLattice.DataProcessing
{
    internal sealed class DictionaryEntryDeserializer : RawStringDataDeserializer<DictionaryEntry>
    {
        private const int MinSerializedPartsCount = 3;
        private const int TextPosition = 0;
        private readonly IPhonemeStorage _phonemeStorage;
        private readonly char[] _separators = new[] {' ', '=', '#'};

        public DictionaryEntryDeserializer(IPhonemeStorage phonemeStorage)
        {
            if (phonemeStorage == null)
                throw new ArgumentNullException("phonemeStorage");

            _phonemeStorage = phonemeStorage;
        }

        protected override char[] Separators
        {
            get { return _separators; }
        }

        protected override DictionaryEntry DeserializeParts(string[] serializedParts)
        {
            if (serializedParts.Length < MinSerializedPartsCount)
            {
                throw new InvalidDataException("Invalid dictionary entry data");
            }

            string text = serializedParts[TextPosition].Trim();
            Phoneme[] phonemes = GetPhonemes(serializedParts);
            int id = GetId(serializedParts.Last());
            var result = new DictionaryEntry(text, phonemes, id);
            return result;
        }

        private Phoneme[] GetPhonemes(string[] serializedParts)
        {
            int endPhonemePosition = serializedParts.Length - 1;
            const int startPhonemePosition = TextPosition + 1;

            int phonemesNumber = endPhonemePosition - startPhonemePosition;
            var result = new Phoneme[phonemesNumber];

            for (int i = startPhonemePosition; i < endPhonemePosition; i++)
            {
                string phonemeRepresentation = serializedParts[i];
                Phoneme phoneme = _phonemeStorage[phonemeRepresentation];
                result[i - startPhonemePosition] = phoneme;
            }

            return result;
        }
    }
}