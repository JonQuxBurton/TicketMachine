using System.Collections.Generic;
using FluentAssertions;
using TicketMachine.BasicStrategy;
using Xunit;

namespace TicketMachine.Tests.BasicStrategy
{
    public class BasicStationFinderStrategySpec
    {
        public class GetSuggestionsShould
        {
            public IStationFinderStrategy CreateSut(IEnumerable<string> data)
            {
                return new BasicStationFinderStrategy(data);
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
    }
}