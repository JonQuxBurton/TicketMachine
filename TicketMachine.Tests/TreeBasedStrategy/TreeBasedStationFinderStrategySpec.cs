using System.Collections.Generic;
using FluentAssertions;
using TicketMachine.TreeBasedStrategy;
using Xunit;

namespace TicketMachine.Tests.TreeBasedStrategy
{
    public class TreeBasedStationFinderStrategySpec
    {
        public class GetSuggestionsShould
        {
            public IStationFinderStrategy CreateSut(IEnumerable<string> data)
            {
                var treeFactory = new TreeFactory();
                var stationsTree = treeFactory.Build(data);

                return new TreeBasedStationFinderStrategy(stationsTree);
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
                
                actual.Stations.Should().Equal("DARTON", "DARTFORD", "DEANSGATE", "DISS");
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
        }

        public class ResetShould
        {
            [Fact]
            public void SetCurrentNodeToRoot()
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
                var treeFactory = new TreeFactory();
                var stationsTree = treeFactory.Build(data);

                var sut = new TreeBasedStationFinderStrategy(stationsTree);
                sut.GetSuggestions("D");

                sut.Reset();

                var actual = stationsTree.GetCurrentNode();

                actual.Letter.Should().Be(Tree.RootSymbol);
            }
        }
    }
}
