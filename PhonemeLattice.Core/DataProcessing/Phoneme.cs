using System;

namespace PhonemeLattice.Core.DataProcessing
{
    [Serializable]
    public struct Phoneme
    {
        public readonly int Id;
        public readonly string TextRepresentation;

        public Phoneme(int id, string textRepresentation) : this()
        {
            if (textRepresentation == null)
            {
                throw new ArgumentNullException("textRepresentation");
            }

            Id = id;
            TextRepresentation = textRepresentation;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (obj.GetType() != typeof (Phoneme)) return false;
            var other = (Phoneme) obj;
            var  result = Id == other.Id && string.Equals(TextRepresentation, other.TextRepresentation);
            return result;
        }

        public override int GetHashCode()
        {
            return (Id*397) ^ TextRepresentation.GetHashCode();
        }
    }
}