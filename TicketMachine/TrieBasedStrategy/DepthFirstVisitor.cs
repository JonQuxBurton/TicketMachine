using System;
using System.Collections.Generic;

namespace TicketMachine.TrieBasedStrategy
{
    /// <summary>
    /// A Visitor to visit every child node of a node and execute an Action with the node as a parameter.
    /// </summary>
    public class DepthFirstVisitor : IVisitor
    {
        public void Visit(Node node, Action<Node> action)
        {
            Stack<Node> stack = new Stack<Node>();

            stack.Push(node);

            while (stack.Count > 0)
            {
                var currentNode = stack.Pop();

                action(currentNode);

                // ReverseOrder (C,B,A) so the Nodes will be popped off the stack in alphabetical order (A,B,C)
                foreach (var child in currentNode.GetChildrenInReverseOrder())
                {
                    stack.Push(child);
                }
            }
        }
    }
}