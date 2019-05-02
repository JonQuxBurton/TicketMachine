using System.Collections.Generic;

namespace TicketMachine.TreeBasedStrategy
{
    /// <summary>
    /// A node of the tree. Each node is either a LetterNode (a single character of the letter and child nodes with further letters) or a LeafNode (the full name of a station).
    /// </summary>
    public class Node
    {
        public static char LeafNodeSymbol = '#';

        private readonly Dictionary<char, Node> children = new Dictionary<char, Node>();
        private readonly SortedSet<char> nextLetters = new SortedSet<char>();

        public Node(char letter, IEnumerable<char> nextLetters, bool isLeaf, string leafValue = null)
        {
            Letter = letter;
            IsLeaf = isLeaf;
            LeafValue = leafValue;
            if (nextLetters != null)
            {
                foreach (var nextLetter in nextLetters)
                {
                    this.AddNextLetter(nextLetter);
                }
            }
        }

        public char Letter { get; }
        public IEnumerable<char> NextLetters => this.nextLetters;
        public bool IsLeaf { get; }
        public string LeafValue { get; }

        public IEnumerable<Node> Children => this.children.Values;

        public static Node CreateLeafNode(string leafValue)
        {
            return new Node(Node.LeafNodeSymbol, null, true, leafValue);
        }

        public static Node CreateLetterNode(char letter, char? firstNextLetter = null)
        {
            var nextLetters = new List<char>();
            if (firstNextLetter != null)
                nextLetters.Add(firstNextLetter.Value);

            return new Node(letter, nextLetters, false);
        }

        public void AddChild(Node child)
        {
            this.children.Add(child.Letter, child);
        }

        public IEnumerable<string> GetLeafValues()
        {
            var leaves = new List<string>();
            if (this.children.ContainsKey(Node.LeafNodeSymbol))
                leaves.Add(this.children[Node.LeafNodeSymbol].LeafValue);

            foreach (var child in this.children.Values)
            {
                leaves.AddRange(child.GetLeafValues());
            }

            return leaves;
        }

        public Node GetChild(char letter)
        {
            if (!this.children.ContainsKey(letter))
                return null;

            return this.children[letter];
        }

        public void AddNextLetter(char nextLetter)
        {
            nextLetters.Add(nextLetter);
        }
    }
}