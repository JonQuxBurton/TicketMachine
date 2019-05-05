using System.Linq;
using FluentAssertions;
using TicketMachine.TrieBasedStrategy;
using Xunit;

namespace TicketMachine.Tests.TrieBasedStrategy
{
    public class TrieSpec
    {
        public class ConstructorShould
        {
            [Fact]
            public void CreateTrieWithRoot()
            {
                var sut = new Trie();

                sut.Root.Letter.Should().Be(Trie.RootSymbol);
                sut.Root.IsLeaf.Should().BeTrue();
                sut.Root.HasValue.Should().BeFalse();
            }
        }

        public class AddWordShould
        {
            private readonly Trie sut;

            public AddWordShould()
            {
                sut = new Trie();
            }

            [Fact]
            public void AddLeafNodes()
            {
                sut.AddWord("HAT");

                sut.Root.GetChild('H').IsLeaf.Should().BeFalse();
                sut.Root.GetChild('H').GetChild('A').IsLeaf.Should().BeFalse();
                sut.Root.GetChild('H').GetChild('A').GetChild('T').IsLeaf.Should().BeTrue();
                sut.Root.GetChild('H').GetChild('A').GetChild('T').HasValue.Should().BeTrue();
                sut.Root.GetChild('H').GetChild('A').GetChild('T').Value.Should().Be("HAT");
            }

            [Fact]
            public void AddLeafNodesInAlphabeticalOrder()
            {
                sut.AddWord("CAN");
                sut.AddWord("CAT");
                sut.AddWord("CAB");

                sut.Root.GetChild('C').GetChild('A').GetChildren().First().Letter.Should().Be('B');
                sut.Root.GetChild('C').GetChild('A').GetChildren().Skip(1).First().Letter.Should().Be('N');
                sut.Root.GetChild('C').GetChild('A').GetChildren().Skip(2).First().Letter.Should().Be('T');
            }

            [Fact]
            public void AddNodes()
            {
                sut.AddWord("HAT");

                sut.Root.GetChild('H').Letter.Should().Be('H');
                sut.Root.GetChild('H').GetChild('A').Letter.Should().Be('A');
                sut.Root.GetChild('H').GetChild('A').GetChild('T').Letter.Should().Be('T');
            }

            [Fact]
            public void AddNodesWithBranch()
            {
                sut.AddWord("CAB");
                sut.AddWord("CAT");

                sut.Root.GetChild('C').GetChild('A').GetChild('B').Letter.Should().Be('B');
                sut.Root.GetChild('C').GetChild('A').GetChild('T').Letter.Should().Be('T');
            }

            [Fact]
            public void AddValueNodes()
            {
                sut.AddWord("HA");
                sut.AddWord("HAT");

                sut.Root.GetChild('H').IsLeaf.Should().BeFalse();
                sut.Root.GetChild('H').GetChild('A').IsLeaf.Should().BeFalse();
                sut.Root.GetChild('H').GetChild('A').HasValue.Should().BeTrue();
                sut.Root.GetChild('H').GetChild('A').Value.Should().Be("HA");
            }

            [Fact]
            public void AddValueToExistingNode()
            {
                sut.AddWord("HAT");
                sut.AddWord("HA");

                sut.Root.GetChild('H').GetChild('A').IsLeaf.Should().BeFalse();
                sut.Root.GetChild('H').GetChild('A').HasValue.Should().BeTrue();
                sut.Root.GetChild('H').GetChild('A').Value.Should().Be("HA");
            }
        }
    }
}