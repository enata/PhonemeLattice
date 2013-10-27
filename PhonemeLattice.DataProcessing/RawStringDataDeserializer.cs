using System;
using System.IO;

namespace PhonemeLattice.DataProcessing
{
    /// <summary>
    /// Base class for string to object deserializers
    /// </summary>
    /// <typeparam name="T">resulted object type</typeparam>
    public abstract class RawStringDataDeserializer<T>
    {
        protected abstract char[] Separators { get; }

        public T Deserialize(string serialized)
        {
            if(serialized == null)
                throw new ArgumentNullException("serialized");

            var splittedData = serialized.Split(Separators, StringSplitOptions.RemoveEmptyEntries);

            return DeserializeParts(splittedData);
        }

        protected abstract T DeserializeParts(string[] serializedParts);

        protected int GetId(string serializedId)
        {
            int result;
            if (!int.TryParse(serializedId, out result))
            {
                throw new InvalidDataException("Invalid id");
            }

            return result;
        }
    }
}