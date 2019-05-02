using System.Collections.Generic;

namespace TicketMachine
{
    public class Suggestions
    {
        public IEnumerable<char> NextLetters { get; set; }
        public IEnumerable<string> Stations { get; set; }
    }
}