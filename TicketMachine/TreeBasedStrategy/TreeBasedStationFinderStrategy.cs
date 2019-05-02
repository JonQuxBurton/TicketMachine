namespace TicketMachine.TreeBasedStrategy
{
    /// <summary>
    /// Performs GetSuggestions() using a supplied tree of stations.
    /// </summary>
    public class TreeBasedStationFinderStrategy : IStationFinderStrategy
    {
        private readonly Tree stationsTree;

        public TreeBasedStationFinderStrategy(Tree stationsTree)
        {
            this.stationsTree = stationsTree;
        }

        public Suggestions GetSuggestions(string userInput)
        {
            foreach (var letter in userInput)
            {
                this.stationsTree.Advance(letter);
            }

            var stations = stationsTree.GetCurrentNode().GetLeafValues();

            return new Suggestions()
            {
                NextLetters = stationsTree.GetCurrentNode().NextLetters,
                Stations = stations
            };
        }

        public void Reset()
        {
            stationsTree.ResetCurrentNode();
        }
    }
}