using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Moq;
using TicketMachine.TrieBasedStrategy;
using Xunit;

namespace TicketMachine.Tests.TrieBasedStrategy
{
    public class VisitorSpec
    {
        public class VisitShould
        {
            [Fact]
            public void VisitAllNodes()
            {
                var trie = new Trie();
                trie.AddWord("ABER");

                var sut = new Visitor();
                var actual = new List<Node>();
                Action<Node> expectedAction = x => { actual.Add(x); };

                sut.Visit(trie.Root, expectedAction);

                actual.Select(x => x.Letter).Should().Equal('_', 'A', 'B', 'E', 'R');
            }

            [Fact]
            public void VisitChildNodesInAlphabeticalOrder()
            {
                var trie = new Trie();
                trie.AddWord("A");
                trie.AddWord("AB");
                trie.AddWord("AC");
                trie.AddWord("AD");

                var sut = new Visitor();
                var actual = new List<Node>();
                Action<Node> expectedAction = x => { actual.Add(x); };

                sut.Visit(trie.Root, expectedAction);

                actual.Select(x => x.Letter).Should().Equal('_', 'A', 'B', 'C', 'D');
            }

            [Fact]
            public void VisitRootNode()
            {
                var trie = new Trie();
                var sut = new Visitor();
                var expectedAction = new Mock<Action<Node>>();

                sut.Visit(trie.Root, expectedAction.Object);

                expectedAction.Verify(x => x(trie.Root));
            }
        }
    }
}