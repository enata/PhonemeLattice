using PhonemeLattice.Core;
using PhonemeLattice.Core.Lattice;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace PhonemeLattice.DataProcessing
{
    public sealed class LatticeFilePreserver : ILatticePreserver
    {
        private readonly string _fileName;

        public LatticeFilePreserver(string fileName)
        {
            if(string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentNullException("fileName");

            _fileName = fileName;
        }

        public void Save(ILattice lattice)
        {
            var formatter = new BinaryFormatter();
            using (var stream = new FileStream(_fileName, FileMode.Create, FileAccess.Write))
            {
                formatter.Serialize(stream, lattice);
            }
        }

        public ILattice Load()
        {
            var formatter = new BinaryFormatter();
            using (var stream = new FileStream(_fileName, FileMode.Open, FileAccess.Read))
            {
                return (ILattice) formatter.Deserialize(stream);
            }
        }
    }
}