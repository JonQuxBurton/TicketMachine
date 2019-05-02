using System.Collections.Generic;

namespace TicketMachine.TreeBasedStrategy
{
    /// <summary>
    /// Creates a tree from a list of stationNames. Each letter of the stationName is added as a LetterNode and the full name is added as a LeafNode.
    /// </summary>
    public class TreeFactory
    {
        public Tree Build(IEnumerable<string> stationNames)
        {
            var tree = new Tree();

            foreach (var stationName in stationNames)
            {
                for (var i = 0; i < stationName.Length; i++)
                {
                    var currentLetter = stationName[i];

                    if (!tree.DoesNodeExist(currentLetter))
                        AddLetterNode(currentLetter, tree);

                    tree.Advance(currentLetter);

                    AddNextLetterToNode(i, stationName, tree.GetCurrentNode());
                }

                AddLeafNode(stationName, tree);
                tree.ResetCurrentNode();
            }

            return tree;
        }

        private static void AddLeafNode(string stationName, Tree tree)
        {
            var leafNode = Node.CreateLeafNode(stationName);
            tree.GetCurrentNode().AddChild(leafNode);
        }

        private static void AddNextLetterToNode(int i, string stationName, Node node)
        {
            var nextLetter = GetNextLetter(i, stationName);

            if (nextLetter == null)
                return;
            
            node.AddNextLetter(nextLetter.Value);
        }

        private static char? GetNextLetter(int i, string stationName)
        {
            if (i >= stationName.Length - 1)
                return null;

            return stationName[i + 1];
        }

        private static void AddLetterNode(char currentLetter, Tree tree)
        {
            var newLetterNode = Node.CreateLetterNode(currentLetter);
            tree.GetCurrentNode().AddChild(newLetterNode);
        }
    }
}