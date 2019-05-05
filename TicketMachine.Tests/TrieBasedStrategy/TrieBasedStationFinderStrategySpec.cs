using System.Collections.Generic;
using FluentAssertions;
using TicketMachine.TrieBasedStrategy;
using Xunit;

namespace TicketMachine.Tests.TrieBasedStrategy
{
    public class TrieBasedStationFinderStrategySpec
    {
        public class GetSuggestionsShould
        {
            public IStationFinderStrategy CreateSut(IEnumerable<string> data)
            {
                var trie = new Trie();
                foreach (var station in data)
                {
                    trie.AddWord(station);
                }
                IVisitor visitor = new DepthFirstVisitor();
                return new TrieBasedStationFinderStrategy(trie, visitor);
            }

            [Fact]
            public void ReturnSuggestedStations()
            {
                var data = new List<string>()
                {
                    "ABERDEEN",
                    "DARTON",
                    "DARTFORD",
                    "DEANSGATE",
                    "DISS",
                    "EDALE"

                };
                var sut = CreateSut(data);

                var actual = sut.GetSuggestions("D");
                
                actual.Stations.Should().Equal("DARTFORD", "DARTON", "DEANSGATE", "DISS");
            }

            [Fact]
            public void ReturnNextLetters()
            {
                var data = new List<string>()
                {
                    "ABERDEEN",
                    "DARTON",
                    "DARTFORD",
                    "DEANSGATE",
                    "DISS",
                    "EDALE"

                };
                var sut = CreateSut(data);

                var actual = sut.GetSuggestions("D");
                
                actual.NextLetters.Should().Equal('A', 'E', 'I');
            }

            [Fact]
            public void ReturnEmptyNextLettersWhenNoNodeMatches()
            {
                var data = new List<string>()
                {
                    "ABERDEEN"
                };
                var sut = CreateSut(data);

                var actual = sut.GetSuggestions("ABERH");

                actual.NextLetters.Should().BeEmpty();
            }
        }
    }
}