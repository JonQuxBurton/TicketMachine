using FluentAssertions;
using TicketMachine.TreeBasedStrategy;
using Xunit;

namespace TicketMachine.Tests.TreeBasedStrategy
{
    public class TreeSpec
    {
        public class ConstructorShould
        {
            [Fact]
            public void CreateTreeWithRootNode()
            {
                var actual = new Tree();

                var actualRootNode = actual.GetCurrentNode();
                actualRootNode.Letter.Should().Be(Tree.RootSymbol);
                actualRootNode.NextLetters.Should().BeEmpty();
                actualRootNode.IsLeaf.Should().BeFalse();
            }
        }

        public class GetCurrentNodeShould
        {
            [Fact]
            public void ReturnCurrentNode()
            {
                var sut = new Tree();
                sut.AddNode(new Node('A', new char[0], false));
                sut.Advance('A');

                var actual =  sut.GetCurrentNode();

                actual.Letter.Should().Be('A');
            }
        }

        public class ResetCurrentNodeShould
        {
            [Fact]
            public void ResetCurrentNodeToRoot()
            {
                var sut = new Tree();
                sut.AddNode(new Node('A', new char[0], false));
                sut.Advance('A');
                sut.ResetCurrentNode();

                var actual =  sut.GetCurrentNode();

                actual.Letter.Should().Be(Tree.RootSymbol);
            }
        }

        public class AddNodeShould
        {
            [Fact]
            public void AddTheNodeToTree()
            {
                var expectedNode = new Node('A', new char[0], false);
                var sut = new Tree();
                sut.AddNode(expectedNode);
                sut.Advance('A');

                var actual = sut.GetCurrentNode();

                actual.Should().Be(expectedNode);
            }
        }

        public class AdvanceShould
        {
            [Fact]
            public void AdvanceToTheNextNode()
            {
                var sut = new Tree();
                var expectedNode = new Node('A', new[] { 'A', 'B', 'C' }, true);
                sut.AddNode(expectedNode);

                sut.Advance('A');
                var actual = sut.GetCurrentNode();

                actual.Should().Be(expectedNode);
            }
            
            [Fact]
            public void AdvanceToTheNextNodeTwice()
            {
                var sut = new Tree();
                var nodeA = new Node('A', new[] { 'B' }, true);
                var expectedNode = new Node('B', new char[0], true);
                nodeA.AddChild(expectedNode);

                sut.AddNode(nodeA);

                sut.Advance('A');
                sut.Advance('B');
                var actual = sut.GetCurrentNode();

                actual.Should().Be(expectedNode);
            }

            [Fact]
            public void NotAdvanceWhenNodeDoesNotExist()
            {
                var sut = new Tree();
                var nodeA = new Node('A', new char[0], false);
                sut.AddNode(nodeA);

                sut.Advance('B');

                var actual = sut.GetCurrentNode();
                actual.Letter.Should().Be(Tree.RootSymbol);
            }
        }

        public class DoesNodeExistShould
        {
            [Fact]
            public void ReturnTrueWhenNodeExists()
            {
                var node = new Node('A', new char[0], false);
                var sut = new Tree();
                sut.AddNode(node);

                var actual = sut.DoesNodeExist('A');

                actual.Should().BeTrue();
            }

            [Fact]
            public void ReturnFalseWhenNodeDoesNotExist()
            {
                var sut = new Tree();

                var actual = sut.DoesNodeExist('A');

                actual.Should().BeFalse();
            }
        }

    }
}