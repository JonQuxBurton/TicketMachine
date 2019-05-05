using System;
using System.Collections.Generic;

namespace TicketMachine.TrieBasedStrategy
{
    /// <summary>
    /// StationFinder based on searching using a Trie.
    /// </summary>
    public class TrieBasedStationFinderStrategy : IStationFinderStrategy
    {
        private readonly Trie trie;

        public TrieBasedStationFinderStrategy(Trie trie)
        {
            this.trie = trie;
        }

        public Suggestions GetSuggestions(string userInput)
        {
            var node = trie.Root;
            var noMatch = false;

            foreach (var letter in userInput)
            {
                var nextNode = node.GetChild(letter);

                if (nextNode != null)
                    node = nextNode;
                else
                {
                    noMatch = true;
                    break;
                }
            }

            var visitor = new Visitor();

            var stations = new List<string>();

            Action<Node> act = (x) =>
            {
                if (x.HasValue)
                {
                    stations.Add($"{x.Value}");
                }
            };

            visitor.Visit(node, act);

            return new Suggestions()
            {
                NextLetters = noMatch ? new char[0] : node.NextLetters,
                Stations = stations
            };
        }
    }
}