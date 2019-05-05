namespace TicketMachine.TrieBasedStrategy
{
    /// <summary>
    /// Data structure for storing the nodes which represent the letters of the station names.
    /// </summary>
    public class Trie
    {
        public Trie()
        {
            Root = new Node('_');
        }

        public Node Root { get; }

        public void AddWord(string word)
        {
            var nextNode = Root;

            for (int i = 0; i < word.Length; i++)
            {
                var letter = word[i];

                Node child;

                if (!nextNode.HasChild(letter))
                {
                    if (IsFinalLetter(word, i))
                        child = new Node(letter, word);
                    else
                        child = new Node(letter);
                }
                else
                {
                    child = nextNode.GetChild(letter);
                    if (IsFinalLetter(word, i))
                        child.AddValue(word);
                }

                nextNode.AddChild(child);
                nextNode = child;
            }
        }

        private static bool IsFinalLetter(string word, int i)
        {
            return i == word.Length - 1;
        }
    }
}