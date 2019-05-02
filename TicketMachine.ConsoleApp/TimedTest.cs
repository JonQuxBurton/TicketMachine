using System;
using System.Diagnostics;
using TicketMachine.Data;

namespace TicketMachine.ConsoleApp
{
    public class TimedTest
    {
        public void Execute(string label, DataStore dataStore, IStationFinderStrategy finder)
        {
            Console.WriteLine($"Running performance test for For Strategy [{label}]...");

            var stopwatch = new Stopwatch();
            var stations = dataStore.GetStations();
            stopwatch.Start();

            foreach (var station in stations)
            {
                for (int i = 0; i < station.Length; i++)
                {
                    finder.GetSuggestions(station.Substring(0, i));
                }
            }

            stopwatch.Stop();

            Console.WriteLine($"TimeTaken: {stopwatch.Elapsed.TotalSeconds} seconds");
        }
    }
}