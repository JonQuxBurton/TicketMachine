using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace TicketMachine.BasicStrategy
{
    /// <summary>
    /// Performs GetSuggestions() using a LINQ. Basic version used only for baseline performance comparison.
    /// </summary>
    public class BasicStationFinderStrategy : IStationFinderStrategy
    {
        private IEnumerable<string> data;

        public BasicStationFinderStrategy(IEnumerable<string> stations)
        {
            this.data = stations;
        }

        public Suggestions GetSuggestions(string userInput)
        {
            var stations = data.Where(x => x.StartsWith(userInput)).ToList();
            var firstLetters =
                new SortedSet<char>(stations.Where(y => y.Length > userInput.Length).Select(x => x[userInput.Length]));

            return new Suggestions()
            {
                NextLetters = firstLetters,
                Stations = stations
            };
        }
    }
}