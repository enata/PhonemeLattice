using System;
using System.Collections.Generic;
using System.Linq;
using PhonemeLattice.Core.DataProcessing;

namespace PhonemeLattice.DataProcessing
{
    /// <summary>
    ///     Stores all available phonemes
    /// </summary>
    public sealed class PhonemeStorage : IPhonemeStorage
    {
        private readonly Dictionary<string, Phoneme> _phonemeDictionary;

        public PhonemeStorage(IPhonemeProvider provider)
        {
            if(provider == null)
                throw new ArgumentNullException("provider");

            _phonemeDictionary = provider.GetPhonemes()
                                         .ToDictionary(phoneme => phoneme.TextRepresentation);
        }

        public Phoneme this[string representation]
        {
            get { return _phonemeDictionary[representation]; }
        }
    }
}