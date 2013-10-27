using PhonemeLattice.Core.DataProcessing;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PhonemeLattice.Lattice.Builder
{
    /// <summary>
    ///     Builds lattice by processing dictionary entries one by one
    ///     Takes O(kn) time and O(kn) space
    ///     Where n - dictionary entries number, k - average word length
    /// </summary>
    internal sealed class LatticeBuilder
    {
        // postfix -> 1st postfix node
        private readonly Dictionary<string, PhonemeNode> _postfixDictionary = new Dictionary<string, PhonemeNode>();
        // prefix -> last prefix node
        private readonly Dictionary<string, PhonemeNode> _prefixDictionary = new Dictionary<string, PhonemeNode>();
        private readonly BeginNode _startNode = new BeginNode();

        /// <summary>
        ///     Lattice entry point
        /// </summary>
        public BeginNode StartNode
        {
            get { return _startNode; }
        }

        /// <summary>
        ///     Integrates dictionary entry to the lattice being constructed
        /// </summary>
        public void AddWordEntry(DictionaryEntry entry)
        {
            if (entry == null)
            {
                throw new ArgumentNullException("entry");
            }

            WordDivision wordDivision = FindContainedParts(entry.Phonemes);
            Tuple<LatticeNode, LatticeNode> middlePart = BuildMiddlePart(entry, wordDivision);
            wordDivision.Prefix.AddNextNode(middlePart.Item1);
            if (wordDivision.Postfix != null)
            {
                middlePart.Item2.AddNextNode(wordDivision.Postfix);
            }
        }

        /// <summary>
        ///     Builds node sequence for the part of the word between prefix and postfix
        /// </summary>
        /// <param name="dictionaryEntry">dictionary entry for processed word</param>
        /// <param name="division">word prefix-middle part-postfix division</param>
        /// <returns>Middle part 1st node, middle part last node</returns>
        private Tuple<LatticeNode, LatticeNode> BuildMiddlePart(DictionaryEntry dictionaryEntry,
                                                                WordDivision division)
        {
            Phoneme[] phonemes = dictionaryEntry.Phonemes;
            var wordNode = new WordNode(dictionaryEntry.Id);

            LatticeNode firstNode = wordNode, lastNode = wordNode;
            int wordNodePosition = GetWordNodePosition(division);
            List<PhonemeNode> phonemeNodes;
            if (division.MiddlePartLength > 0)
            {
                phonemeNodes = BuildMiddlePartPhonemeNodes(division, phonemes, wordNodePosition, wordNode);
                firstNode = phonemeNodes[0];
                lastNode = phonemeNodes[phonemeNodes.Count - 1];
                if (wordNodePosition == division.MiddlePartStart)
                {
                    wordNode.AddNextNode(firstNode);
                    firstNode = wordNode;
                }
            }
            else
                phonemeNodes = new List<PhonemeNode>();
            int endPosition = division.MiddlePartStart + division.MiddlePartLength;
            UpdateWordPartDictionaries(phonemes, phonemeNodes, division.MiddlePartStart, wordNodePosition, endPosition);
            var result = new Tuple<LatticeNode, LatticeNode>(firstNode, lastNode);
            return result;
        }

        private static int GetWordNodePosition(WordDivision division)
        {
            return division.MiddlePartLength/2 + division.MiddlePartStart;
        }

        /// <summary>
        ///     Builds phoneme node sequence for middle part of the word
        /// </summary>
        /// <param name="division">prefix-middle part-postfix division</param>
        /// <param name="phonemes">words phonemes</param>
        /// <param name="wordNodePosition">position of word node in node sequence</param>
        /// <param name="wordNode"></param>
        /// <returns>phoneme node sequence</returns>
        private static List<PhonemeNode> BuildMiddlePartPhonemeNodes(WordDivision division, Phoneme[] phonemes,
                                                                     int wordNodePosition, WordNode wordNode)
        {
            var result = new List<PhonemeNode>();

            var firstNode = new PhonemeNode(phonemes[division.MiddlePartStart]);
            result.Add(firstNode);
            int middlePartEnd = division.MiddlePartStart + division.MiddlePartLength;
            LatticeNode prevNode = firstNode;
            for (int i = division.MiddlePartStart + 1; i < middlePartEnd; i++)
            {
                if (i == wordNodePosition)
                {
                    prevNode.AddNextNode(wordNode);
                    prevNode = wordNode;
                }

                var newNode = new PhonemeNode(phonemes[i]);
                result.Add(newNode);
                prevNode.AddNextNode(newNode);
                prevNode = newNode;
            }
            return result;
        }

        /// <summary>
        ///     Adds new prefixes and postfixes to dictionaries
        /// </summary>
        /// <param name="phonemes">current words phonemes</param>
        /// <param name="nodes">phoneme nodes for middle word part</param>
        /// <param name="startPosition">middle part start position</param>
        /// <param name="wordPartsSeparator">position of prefix|postfix border</param>
        /// <param name="endPosition">middle part end position</param>
        private void UpdateWordPartDictionaries(Phoneme[] phonemes, List<PhonemeNode> nodes, int startPosition,
                                                int wordPartsSeparator, int endPosition)
        {
            for (int i = startPosition; i < wordPartsSeparator; i++)
            {
                string key = BuildKey(phonemes.Take(i + 1));
                _prefixDictionary.Add(key, nodes[i - startPosition]);
            }
            for (int i = wordPartsSeparator; i < endPosition; i++)
            {
                string key = BuildKey(phonemes.Skip(i));
                _postfixDictionary.Add(key, nodes[i - startPosition]);
            }
        }

        /// <summary>
        ///     Breaks word into prefix-middle part-postfix parts
        /// </summary>
        /// <param name="phonemes">current word phonemes</param>
        private WordDivision FindContainedParts(Phoneme[] phonemes)
        {
            IEnumerable<Tuple<WordPart, WordPart>> wordParts = GetPrefixesAndPostfixes(phonemes);
            LatticeNode prefix = _startNode, postfix = null;
            int prefixLength = 0, postfixLength = 0;
            foreach (var prefixPostfix in wordParts)
            {
                WordPart prefixPart = prefixPostfix.Item1;
                if (_prefixDictionary.ContainsKey(prefixPart.Key))
                {
                    prefix = _prefixDictionary[prefixPart.Key];
                    prefixLength = prefixPart.Length;
                    postfix = GetPostfix(phonemes, prefixPart.Length, out postfixLength);
                    break;
                }

                WordPart postfixPart = prefixPostfix.Item2;
                if (_postfixDictionary.ContainsKey(postfixPart.Key))
                {
                    postfix = _postfixDictionary[postfixPart.Key];
                    postfixLength = postfixPart.Length;
                    prefix = GetPrefix(phonemes, postfixPart.Length, out prefixLength);
                    break;
                }
            }
            return new WordDivision(prefix, postfix, phonemes.Length - prefixLength - postfixLength, prefixLength);
        }

        private LatticeNode GetPrefix(Phoneme[] phonemes, int posfixLength, out int prefixLength)
        {
            return GetWordPart(phonemes.Length, posfixLength, i => phonemes.Take(phonemes.Length - i), _prefixDictionary,
                               out prefixLength, _startNode);
        }

        private LatticeNode GetPostfix(Phoneme[] phonemes, int prefixLength, out int postfixLength)
        {
            return GetWordPart(phonemes.Length, prefixLength, phonemes.Skip, _postfixDictionary,
                               out postfixLength, null);
        }

        private LatticeNode GetWordPart(int phonemesNumber, int oppositeLength,
                                        Func<int, IEnumerable<Phoneme>> filter,
                                        Dictionary<string, PhonemeNode> wordPartDictionary, out int length,
                                        LatticeNode defaultNode)
        {
            for (int i = oppositeLength; i < phonemesNumber; i++)
            {
                string key = BuildKey(filter.Invoke(i));
                if (wordPartDictionary.ContainsKey(key))
                {
                    length = phonemesNumber - i;
                    return wordPartDictionary[key];
                }
            }

            length = 0;
            return defaultNode;
        }

        /// <summary>
        ///     Builds prefix|postfix pair, where prefix length == postfix length.
        ///     Ordered by length from largest to smallest
        /// </summary>
        /// <param name="phonemes">phonemes of the word to process</param>
        private IEnumerable<Tuple<WordPart, WordPart>> GetPrefixesAndPostfixes(Phoneme[] phonemes)
        {
            for (int i = 0; i < phonemes.Length; i++)
            {
                int length = phonemes.Length - i;
                string prefixKey = BuildKey(phonemes.Take(length));
                string postfixKey = BuildKey(phonemes.Skip(i));
                var prefix = new WordPart(length, prefixKey);
                var postfix = new WordPart(length, postfixKey);
                var element = new Tuple<WordPart, WordPart>(prefix, postfix);
                yield return element;
            }
        }

        private string BuildKey(IEnumerable<Phoneme> phonemes)
        {
            IEnumerable<string> phonemesTexts = phonemes.Select(phoneme => phoneme.TextRepresentation);
            string result = String.Join("@", phonemesTexts);
            return result;
        }
    }
}