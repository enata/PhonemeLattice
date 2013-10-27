using System.Collections.Generic;

namespace PhonemeLattice.Core.DataProcessing
{
    /// <summary>
    /// Provides phonemes
    /// </summary>
    public interface IPhonemeProvider
    {
        /// <summary>
        /// Loads phonemes information from a storage 
        /// </summary>
        IEnumerable<Phoneme> GetPhonemes();
    }

    /// <summary>
    /// Stores all available phonems
    /// </summary>
    public interface IPhonemeStorage
    {
        Phoneme this[string representation] { get; }
    }
}