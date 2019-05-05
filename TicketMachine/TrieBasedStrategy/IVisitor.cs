using System;

namespace TicketMachine.TrieBasedStrategy
{
    public interface IVisitor
    {
        void Visit(Node node, Action<Node> action);
    }
}