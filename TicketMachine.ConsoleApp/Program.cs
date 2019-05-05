using System;
using System.Linq;
using TicketMachine.BasicStrategy;
using TicketMachine.Data;
using TicketMachine.TrieBasedStrategy;

namespace TicketMachine.ConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            var rawDataGetter = new RawDataGetter();
            var dataStore = new DataStore(rawDataGetter);

            var stationsTrie = new Trie();
            foreach (var station in dataStore.GetStations())
            {
                stationsTrie.AddWord(station);
            }

            IVisitor visitor = new DepthFirstVisitor();
            IStationFinderStrategy finderStrategy = new TrieBasedStationFinderStrategy(stationsTrie, visitor);
            
            if (args.Length > 0)
            {
                if (args.First() == "perf")
                {
                    var basicStrategy = new BasicStationFinderStrategy(dataStore.GetStations());

                    var timedTest = new TimedTest();
                    timedTest.Execute("TrieBased", dataStore, finderStrategy);

                    timedTest = new TimedTest();
                    timedTest.Execute("Basic", dataStore, basicStrategy);
                    return;
                }

                if (args.First() == "basic")
                {
                    Console.WriteLine("[Strategy: Basic]");
                    finderStrategy = new BasicStationFinderStrategy(dataStore.GetStations());
                }
            }

            var finder = new StationFinder(finderStrategy);

            string userInput = "";

            while (true)
            {
                Console.WriteLine("Enter a letter:");
                Console.WriteLine("(or Escape to quit, backspace to delete)");

                var read = Console.ReadKey();

                if (read.Key == ConsoleKey.Escape)
                    break;

                if (read.Key == ConsoleKey.Backspace)
                {
                    if (userInput.Length > 0)
                    {
                        userInput = userInput.Substring(0, userInput.Length - 1);
                    }
                }
                else
                {
                    userInput += read.KeyChar.ToString().ToUpperInvariant();
                }

                var suggestions = finder.GetSuggestions(userInput);

                Console.WriteLine($"\n\nUserInput: {userInput}");
                Console.WriteLine($"NextLetters: {string.Join(" | ", suggestions.NextLetters)}");
                Console.WriteLine($"Stations: {string.Join(" | ", suggestions.Stations)}\n");
            }
        }
    }
}