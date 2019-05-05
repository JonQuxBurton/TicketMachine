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
        private readonly IVisitor visitor;

        public TrieBasedStationFinderStrategy(Trie trie, IVisitor visitor)
        {
            this.trie = trie;
            this.visitor = visitor;
        }

        public Suggestions GetSuggestions(string userInput)
        {
            var matchingNode = trie.Root;
            var noMatch = false;

            foreach (var letter in userInput)
            {
                var nextNode = matchingNode.GetChild(letter);

                if (nextNode != null)
                {
                    matchingNode = nextNode;
                }
                else
                {
                    noMatch = true;
                    break;
                }
            }

            var stations = new List<string>();

            Action<Node> act = (x) =>
            {
                if (x.HasValue)
                {
                    stations.Add($"{x.Value}");
                }
            };

            visitor.Visit(matchingNode, act);

            return new Suggestions()
            {
                NextLetters = noMatch ? new char[0] : matchingNode.NextLetters,
                Stations = stations
            };
        }
    }
}