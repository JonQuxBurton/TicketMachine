using FluentAssertions;
using Moq;
using Xunit;

namespace TicketMachine.Tests
{
    public class StationFinderSpec
    {
        public class GetSuggestionsShould
        {
            [Fact]
            public void ReturnSuggestionsFromStationFinderStrategy()
            {
                var expected = new Suggestions()
                {
                    Stations = new []{ "DARTFORD", "DARTON "},
                    NextLetters = new [] { 'F', 'O' }
                };
                var stationFinderStrategyMock = new Mock<IStationFinderStrategy>();
                stationFinderStrategyMock.Setup(x => x.GetSuggestions("DART"))
                    .Returns(expected);
                var sut = new StationFinder(stationFinderStrategyMock.Object);

                var actual = sut.GetSuggestions("DART");

                actual.Should().Be(expected);
            }
        }

        public class ResetShould
        {
            [Fact]
            public void ResetStationFinderStrategy()
            {
                var stationFinderStrategyMock = new Mock<IStationFinderStrategy>();
                var sut = new StationFinder(stationFinderStrategyMock.Object);

                sut.Reset();

                stationFinderStrategyMock.Verify(x => x.Reset());
            }
        }
    }
}