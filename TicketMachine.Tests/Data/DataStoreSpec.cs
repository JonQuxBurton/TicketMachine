using FluentAssertions;
using Moq;
using TicketMachine.Data;
using Xunit;

namespace TicketMachine.Tests.Data
{
    public class DataStoreSpec
    {
        public class GetStationsShould
        {
            [Fact]
            public void ReturnStationsTrimmedAndUppercased()
            {
                var rawDataGetterMock = new Mock<IRawDataGetter>();
                rawDataGetterMock.Setup(x => x.Get()).Returns(
                    "Aaa, Bbb, Ccc");

                var sut = new DataStore(rawDataGetterMock.Object);

                var actual = sut.GetStations();

                actual.Should().Equal("AAA", "BBB", "CCC");
            }
        }
    }
}