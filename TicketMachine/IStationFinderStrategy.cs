namespace TicketMachine
{
    public interface IStationFinderStrategy
    {
        Suggestions GetSuggestions(string userInput);
    }
}