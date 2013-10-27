using System.IO;
using System.Text;
using PhonemeLattice.Core.DataProcessing;

namespace PhonemeLattice.DataProcessing
{
    /// <summary>
    ///     Deserializes phoneme from a raw string data
    /// </summary>
    internal sealed class PhonemeStringDeserializer : RawStringDataDeserializer<Phoneme>
    {
        private const char RepresentationSeparator = '"';
        private const int SerializedPartsCount = 3;
        private const int SerializedRepresentationPosition = 0;
        private const int SerializedIdPosition = 2;
        private readonly char[] _splitSymbols = new[] {' '};

        protected override char[] Separators
        {
            get { return _splitSymbols; }
        }

        protected override Phoneme DeserializeParts(string[] splittedData)
        {
            if (splittedData.Length != SerializedPartsCount)
            {
                throw new InvalidDataException("Invalid phoneme data");
            }

            string representation = GetStringRepresentation(splittedData[SerializedRepresentationPosition]);
            int id = GetId(splittedData[SerializedIdPosition]);
            var result = new Phoneme(id, representation);
            return result;
        }

        private string GetStringRepresentation(string serializedRepresentation)
        {
            var resultBuilder = new StringBuilder(serializedRepresentation.Length);
            bool toSave = false;
            foreach (char symbol in serializedRepresentation)
            {
                if (toSave)
                {
                    if (symbol == RepresentationSeparator)
                        break;
                    resultBuilder.Append(symbol);
                }
                else if (symbol == RepresentationSeparator)
                {
                    toSave = true;
                }
            }
            string result = resultBuilder.ToString();
            if (string.IsNullOrEmpty(result))
            {
                throw new InvalidDataException("Invalid phoneme representation data");
            }
            return result;
        }
    }
}