using System.Collections.Generic;
using FluentAssertions;
using TicketMachine.TreeBasedStrategy;
using Xunit;

namespace TicketMachine.Tests.TreeBasedStrategy
{
    public class TreeFactorySpec
    {
        public class BuildShould
        { 
            [Fact]
            public void ReturnTreeWithRootNode()
            {
                var data = new List<string>();

                var sut = new TreeFactory();

                var actual = sut.Build(data);

                var actualNode = actual.GetCurrentNode();
                actualNode.Letter.Should().Be(Tree.RootSymbol);
                actualNode.IsLeaf.Should().BeFalse();
                actualNode.Children.Should().BeEmpty();
                actualNode.LeafValue.Should().BeNull();
                actualNode.NextLetters.Should().BeEmpty();
            }

            [Theory]
            [InlineData('A')]
            [InlineData('B')]
            [InlineData('C')]
            public void ReturnTreeWithLetterNodesAdded(char expectedLetter)
            {
                var data = new List<string>()
                {
                    "A",
                    "B",
                    "C"
                };

                var sut = new TreeFactory();

                var actual = sut.Build(data);
                
                actual.Advance(expectedLetter);
                var actualNode = actual.GetCurrentNode();
                actualNode.Letter.Should().Be(expectedLetter);
                actualNode.Children.Should().HaveCount(1);
                actualNode.IsLeaf.Should().BeFalse();
                actualNode.LeafValue.Should().Be(null);
                actualNode.NextLetters.Should().BeEmpty();
            }

            [Theory]
            [InlineData('A', 'A', "AA")]
            [InlineData('B', 'B', "BB")]
            [InlineData('C', 'C', "CC")]
            public void ReturnTreeWithMultipleLetterNodesAdded(char expectedFirstLetter, char expectedSecondLetter, string expectedLeafValue)
            {
                var data = new List<string>()
                {
                    "AA",
                    "BB",
                    "CC"
                };

                var sut = new TreeFactory();

                var actual = sut.Build(data);
                
                actual.Advance(expectedFirstLetter);
                var actualFirstNode = actual.GetCurrentNode();
                actualFirstNode .Letter.Should().Be(expectedFirstLetter);
                actualFirstNode .Children.Should().HaveCount(1);
                actualFirstNode .IsLeaf.Should().BeFalse();
                actualFirstNode .LeafValue.Should().Be(null);
                actualFirstNode.NextLetters.Should().Equal(expectedSecondLetter);

                actual.Advance(expectedSecondLetter);
                var actualSecondNode = actual.GetCurrentNode();
                actualSecondNode .Letter.Should().Be(expectedSecondLetter);
                actualSecondNode .Children.Should().HaveCount(1);
                actualSecondNode .IsLeaf.Should().BeFalse();
                actualSecondNode .LeafValue.Should().Be(null);
                actualSecondNode .GetLeafValues().Should().Equal(expectedLeafValue);
                actualSecondNode.NextLetters.Should().BeEmpty();
            }

            [Fact]
            public void ReturnTreeWithLetterNodeAddedToExisting()
            {
                var data = new List<string>()
                {
                    "AB",
                    "ABC"
                };

                var sut = new TreeFactory();

                var actual = sut.Build(data);

                actual.Advance('A');
                var actualFirstNode = actual.GetCurrentNode();
                actualFirstNode.Letter.Should().Be('A');
                actualFirstNode.Children.Should().HaveCount(1);
                actualFirstNode.IsLeaf.Should().BeFalse();
                actualFirstNode.NextLetters.Should().Equal("B");

                actual.Advance('B');
                var actualSecondNode = actual.GetCurrentNode();
                actualSecondNode.Letter.Should().Be('B');
                actualSecondNode.Children.Should().HaveCount(2);
                actualSecondNode.IsLeaf.Should().BeFalse();
                actualSecondNode.GetLeafValues().Should().Equal("AB", "ABC");
                actualSecondNode.NextLetters.Should().Equal("C");

                actual.Advance('C');
                var actualThirdNode = actual.GetCurrentNode();
                actualThirdNode.Letter.Should().Be('C');
                actualThirdNode.Children.Should().HaveCount(1);
                actualThirdNode.IsLeaf.Should().BeFalse();
                actualThirdNode.GetLeafValues().Should().Equal("ABC");
                actualThirdNode.NextLetters.Should().BeEmpty();
            }

            [Theory]
            [InlineData('A')]
            [InlineData('B')]
            public void ReturnTreeWithLeafNodesAdded(char expectedLetter)
            {
                var data = new List<string>()
                {
                    "A",
                    "B"
                };

                var sut = new TreeFactory();

                var actual = sut.Build(data);

                actual.Advance(expectedLetter);
                actual.GetCurrentNode().GetLeafValues().Should().Equal(expectedLetter.ToString());
            }

            [Fact]
            public void ReturnSampleTree()
            {
                var data = new List<string>()
                {
                    "DARTON",
                    "DARTMOUTH"
                };

                var sut = new TreeFactory();

                var actual = sut.Build(data);

                actual.Advance('D');
                actual.GetCurrentNode().Letter.Should().Be('D');
                actual.GetCurrentNode().IsLeaf.Should().BeFalse();
                actual.GetCurrentNode().NextLetters.Should().Equal('A');

                actual.Advance('A');
                actual.GetCurrentNode().Letter.Should().Be('A');
                actual.GetCurrentNode().IsLeaf.Should().BeFalse();
                actual.GetCurrentNode().NextLetters.Should().Equal('R');

                actual.Advance('R');
                actual.GetCurrentNode().Letter.Should().Be('R');
                actual.GetCurrentNode().IsLeaf.Should().BeFalse();
                actual.GetCurrentNode().NextLetters.Should().Equal('T');

                actual.Advance('T');
                actual.GetCurrentNode().Letter.Should().Be('T');
                actual.GetCurrentNode().IsLeaf.Should().BeFalse();
                actual.GetCurrentNode().NextLetters.Should().Equal('M', 'O');

                actual.Advance('O');
                actual.GetCurrentNode().Letter.Should().Be('O');
                actual.GetCurrentNode().IsLeaf.Should().BeFalse();
                actual.GetCurrentNode().NextLetters.Should().Equal('N');

                actual.Advance('N');
                actual.GetCurrentNode().Letter.Should().Be('N');
                actual.GetCurrentNode().IsLeaf.Should().BeFalse();
                actual.GetCurrentNode().GetLeafValues().Should().Equal("DARTON");
                actual.GetCurrentNode().NextLetters.Should().BeEmpty();
            }
        }
    }
}
