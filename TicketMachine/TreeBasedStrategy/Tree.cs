namespace TicketMachine.TreeBasedStrategy
{
    /// <summary>
    /// The tree data structure containing the LetterNodes and LeafNodes of the stations. Has methods to walk the tree(Advance()) and other tree operations.
    /// </summary>
    public class Tree
    {
        public const char RootSymbol = '_';

        private readonly Node root;
        private Node currentNode;

        public Tree()
        {
            this.root = new Node(RootSymbol, new char[0], false);
            this.currentNode = root;
        }

        public void AddNode(Node node)
        {
            root.AddChild(node);
        }

        public void Advance(char letter)
        {
            var nextNode = this.currentNode.GetChild(letter);

            if (nextNode != null)
                this.currentNode = nextNode;
        }

        public Node GetCurrentNode()
        {
            return this.currentNode;
        }

        public void ResetCurrentNode()
        {
            this.currentNode = root;
        }

        public bool DoesNodeExist(char letter)
        {
            var node = this.currentNode.GetChild(letter);

            if (node == null)
                return false;

            return true;
        }
    }
}