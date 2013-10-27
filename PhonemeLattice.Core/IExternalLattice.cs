using PhonemeLattice.Core.Lattice;

namespace PhonemeLattice.Core
{
    /// <summary>
    ///     Предоставляет требуемый по ТЗ интерфейс (или нечто на него похожее)
    /// </summary>
    public interface IExternalLattice
    {
        /// <summary>
        ///     Функция построения сети
        /// </summary>
        /// <param name="file">имя файла словаря</param>
        /// <returns>сеть</returns>
        ILattice LatInit(string file);

        /// <summary>
        /// Функция сохранения сети в файл
        /// </summary>
        /// <param name="lattice">сеть</param>
        /// <param name="file">имя файла сети</param>
        void LatSave(ILattice lattice, string file);

        /// <summary>
        /// Функция загрузки сети из файла
        /// </summary>
        /// <param name="file">имя файла сети</param>
        /// <returns>сеть</returns>
        ILattice LatLoad(string file);

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