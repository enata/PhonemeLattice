using System.Collections.Generic;
using PhonemeLattice.Core.DataProcessing;

namespace PhonemeLattice.DataProcessing
{
    /// <summary>
    ///     Provides phonemes
    /// </summary>
    public sealed class PhonemeProvider : EntityProvider<Phoneme>, IPhonemeProvider
    {
        private readonly PhonemeStringDeserializer _deserializer = new PhonemeStringDeserializer();


        public PhonemeProvider(IStringDataProvider serializedPhonemesProvider) : base(serializedPhonemesProvider)
        {
        }

        protected override RawStringDataDeserializer<Phoneme> EntityDeserializer
        {
            get { return _deserializer; }
        }

        /// <summary>
        ///     Loads phonemes information from a storage
        /// </summary>
        public IEnumerable<Phoneme> GetPhonemes()
        {
            return GetEntities();
        }
    }
}