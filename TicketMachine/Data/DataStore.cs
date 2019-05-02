using System.Collections.Generic;
using System.Linq;

namespace TicketMachine.Data
{
    /// <summary>
    /// DataStore for retrieving the stations name as a list of strings.
    /// </summary>
    public class DataStore
    {
        private readonly IRawDataGetter rawDataGetter;

        public DataStore(IRawDataGetter rawDataGetter)
        {
            this.rawDataGetter = rawDataGetter;
        }

        public IEnumerable<string> GetStations()
        {
            return this.rawDataGetter.Get()
                        .Split(",")
                        .Select(x => x.Trim().ToUpperInvariant());
        }
    }
}