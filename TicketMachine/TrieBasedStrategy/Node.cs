using System.Collections.Generic;
using System.Linq;

namespace TicketMachine.TrieBasedStrategy
{
    /// <summary>
    /// A node of the trie.
    /// Each node represents either a letter of a station name(a LetterNode) or the full name of a station(a LeafNode). 
    /// Though some of the LetterNodes are also station names, for example ABER is a station name but also has child nodes for ABERDEEN.
    /// </summary>
    public class Node
    {
        private readonly SortedDictionary<char, Node> childrenDictionary = new SortedDictionary<char, Node>();
        private List<Node> childrenList = new List<Node>();
        private List<Node> childrenListInReverseOrder = new List<Node>();

        public Node(char letter, string value = null)
        {
            Letter = letter;
            this.Value = value;
        }

        public char Letter { get; }
        public bool IsLeaf => !childrenDictionary.Any();
        public bool HasValue => this.Value != null;
        public string Value { get; private set; }
        public IEnumerable<char> NextLetters => childrenDictionary.Keys;
        public IEnumerable<Node> GetChildren() => childrenList;
        public List<Node> GetChildrenInReverseOrder() => childrenListInReverseOrder;

        public Node GetChild(char letter)
        {
            if (childrenDictionary.TryGetValue(letter, out var child))
                return child;

            return null;
        }

        public void AddChild(Node child)
        {
            if (!childrenDictionary.ContainsKey(child.Letter))
                childrenDictionary.Add(child.Letter, child);

            childrenList = childrenDictionary.Values.ToList();
            childrenListInReverseOrder = childrenList.AsEnumerable().Reverse().ToList();
        }

        public bool HasChild(char letter)
        {
            if (childrenDictionary.ContainsKey(letter))
                return true;

            return false;
        }

        public void AddValue(string value)
        {
            this.Value = value;
        }
    }
}