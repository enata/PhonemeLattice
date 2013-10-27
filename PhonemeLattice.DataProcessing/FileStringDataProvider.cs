using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using PhonemeLattice.Core.DataProcessing;

namespace PhonemeLattice.DataProcessing
{
    /// <summary>
    ///     Reads raw string data from file
    /// </summary>
    public sealed class FileStringDataProvider : IStringDataProvider
    {
        private readonly string _filePath;

        public FileStringDataProvider(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentException(filePath);
            }

            _filePath = filePath;
        }

        /// <summary>
        ///     Reads data strings from file
        /// </summary>
        /// <returns>data entities serialized to string</returns>
        public IEnumerable<string> GetStrings()
        {
            string[] rawStrings = File.ReadAllLines(_filePath, Encoding.GetEncoding(1251));
            IEnumerable<string> result = rawStrings.Where(str => !string.IsNullOrWhiteSpace(str));
            return result;
        }
    }
}