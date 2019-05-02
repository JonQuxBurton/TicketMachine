namespace TicketMachine
{
    /// <summary>
    /// Searches for Stations based on a userInput string. Uses the Strategy supplied through the constructor.
    /// </summary>
    public class StationFinder
    {
        private readonly IStationFinderStrategy stationFinder;

        public StationFinder(IStationFinderStrategy stationFinder)
        {
            this.stationFinder = stationFinder;
        }

        public Suggestions GetSuggestions(string userInput)
        {
            return this.stationFinder.GetSuggestions(userInput);
        }

        public void Reset()
        {
            this.stationFinder.Reset();
        }
    }
}