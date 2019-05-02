using FluentAssertions;
using TicketMachine.TreeBasedStrategy;
using Xunit;

namespace TicketMachine.Tests.TreeBasedStrategy
{
    public class NodeSpec
    {
        public class CreateLetterNodeShould
        {
            [Fact]
            public void ReturnNewLetterNode()
            { 
                var actualNode = Node.CreateLetterNode('A');

                actualNode.Letter.Should().Be('A');
                actualNode.IsLeaf.Should().BeFalse();
                actualNode.LeafValue.Should().BeNull();
            }

            [Fact]
            public void ReturnNewLetterNodeWithNextLetter()
            { 
                var actualNode = Node.CreateLetterNode('A', 'B');

                actualNode.NextLetters.Should().Equal('B');
            }
        }

        public class CreateLeafNodeShould
        {
            [Fact]
            public void ReturnNewLeafNode()
            {
                var expectedLeafValue = "AAA";

                var actualNode = Node.CreateLeafNode(expectedLeafValue);

                actualNode.Letter.Should().Be(Node.LeafNodeSymbol);
                actualNode.IsLeaf.Should().BeTrue();
                actualNode.LeafValue.Should().Be(expectedLeafValue);
            }
        }

        public class GetChildShould
        {
            [Fact]
            public void ReturnChild()
            {
                var child = new Node('B', new char[0], false);
                var sut = new Node('A', new char[0], false);
                sut.AddChild(child);

                var actual = sut.GetChild('B');

                actual.Should().Be(child);
            }

            [Fact]
            public void ReturnNullWhenChildDoesNotExist()
            {
                var sut = new Node('A', new char[0], false);
             
                var actual = sut.GetChild('B');

                actual.Should().BeNull();
            }
        }

        public class AddNextLetterShould
        {
            [Fact]
            public void AddTheLetter()
            {
                var sut = new Node('A', new char[0], false);

                sut.AddNextLetter('B');

                sut.NextLetters.Should().Equal('B');
            }
        }

        public class GetLeafValuesShould
        {
            [Fact]
            public void ReturnLeafValuesForTheNode()
            {
                var sut = new Node('A', new char[0], false);
                sut.AddChild(new Node('#', new char[0], true, "A"));

                var actual = sut.GetLeafValues();

                actual.Should().Equal("A");
            }

            [Fact]
            public void ReturnLeafValuesForTheChildNodes()
            {
                var sut = new Node('A', new char[0], false);
                var nodeB = new Node('B', new char[0], false);
                sut.AddChild(nodeB);
                nodeB.AddChild(new Node('#', new char[0], true, "B"));
                var nodeC = new Node('C', new char[0], false);
                sut.AddChild(nodeC);
                nodeC.AddChild(new Node('#', new char[0], true, "C"));

                var actual = sut.GetLeafValues();

                actual.Should().Equal("B", "C");
            }
        }
    }
}