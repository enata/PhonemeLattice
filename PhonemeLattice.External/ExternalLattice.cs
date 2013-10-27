using PhonemeLattice.Core;
using PhonemeLattice.Core.Lattice;
using PhonemeLattice.DataProcessing;
using PhonemeLattice.Lattice.Builder;
using System;

namespace PhonemeLattice.External
{
    /// <summary>
    /// Предоставляет требуемый внешний интерфейс
    /// </summary>
    public sealed class ExternalLattice : IExternalLattice
    {
        private readonly PhonemeStorage _phonemeStorage = new PhonemeStorage(new PhonemeProvider(new FileStringDataProvider("Phone.txt")));

        /// <summary>
        /// Функция построения сети
        /// </summary>
        /// <param name="file">имя файла словаря</param>
        /// <returns>сеть</returns>
        public ILattice LatInit(string file)
        {
            var dictionaryEntryStringProvider = new FileStringDataProvider(file);
            var dictionaryEntryProvider = new DictionaryEntryProvider(dictionaryEntryStringProvider, _phonemeStorage);
            var factory = new LatticeFactory(dictionaryEntryProvider);
            ILattice lattice = factory.Build();
            return lattice;
        }

        /// <summary>
        /// Функция сохранения сети в файл
        /// </summary>
        /// <param name="lattice">сеть</param>
        /// <param name="file">имя файла сети</param>
        public void LatSave(ILattice lattice, string file)
        {
            if (lattice == null)
            {
                throw new ArgumentNullException("lattice");
            }
            if (string.IsNullOrEmpty(file))
            {
                throw new ArgumentException("Invalid file path");
            }

            var preserver = new LatticeFilePreserver(file);
            preserver.Save(lattice);
        }

        /// <summary>
        /// Функция загрузки сети из файла
        /// </summary>
        /// <param name="file">имя файла сети</param>
        /// <returns>сеть</returns>
        public ILattice LatLoad(string file)
        {
            if (string.IsNullOrEmpty(file))
            {
                throw new ArgumentException("Invalid file path");
            }

            var preserver = new LatticeFilePreserver(file);
            var result = preserver.Load();
            return result;
        }

        //int LatDestroy(HLATTICE& _handle);
        //obsolete

        //int LatGetFirstNode(HLATTICE _handle, HNODE& _node); 
        //Эквивалент: ILattice.StartNode

        //int LatGetFirstLink(HNODE _node, HLINK& _link);
        //int LatGetNextLink(HLINK _link, HLINK& _next);
        //Эквивалент: ILatticeNode.NextNodes

        //int LatGetNode(HLINK _link, HNODE& _node, int& _type);
        //obsolete

        //int LatGetPhone(HNODE _node, int& _phone);
        //Эквивалент: IPhonemeNode.Phoneme

        //int LatGetWordId(HNODE _node, int& _id);
        //Эквивалент: IWordNode.WordId
    }
}