using System.Linq;
using FluentAssertions;
using TicketMachine.TrieBasedStrategy;
using Xunit;

namespace TicketMachine.Tests.TrieBasedStrategy
{
    public class NodeSpec
    {
        public class ConstructorShould
        {
            [Fact]
            public void CreateLetterNode()
            {
                var sut = new Node('A');

                sut.Letter.Should().Be('A');
                sut.IsLeaf.Should().BeTrue();
                sut.HasValue.Should().BeFalse();
            }

            [Fact]
            public void CreateValueNode()
            {
                var sut = new Node('A', "ABER");

                sut.IsLeaf.Should().BeTrue();
                sut.HasValue.Should().BeTrue();
                sut.Value.Should().Be("ABER");
            }
        }

        public class IsLeafShould
        {
            [Fact]
            public void ReturnFalseForNodeWithChild()
            {
                var sut = new Node('A');
                sut.AddChild(new Node('B'));

                var actual = sut.IsLeaf;

                actual.Should().BeFalse();
            }

            [Fact]
            public void ReturnTrueForLeafNode()
            {
                var sut = new Node('A');

                var actual = sut.IsLeaf;

                actual.Should().BeTrue();
            }
        }

        public class HasValueShould
        {
            [Fact]
            public void ReturnFalseForNodeWithoutValue()
            {
                var sut = new Node('A');

                var actual = sut.HasValue;

                actual.Should().BeFalse();
            }

            [Fact]
            public void ReturnTrueForValueNode()
            {
                var sut = new Node('A', "ABER");

                var actual = sut.HasValue;

                actual.Should().BeTrue();
            }
        }

        public class NextLettersShould
        {
            [Fact]
            public void ReturnNextLetters()
            {
                var sut = new Node('A');
                sut.AddChild(new Node('A'));
                sut.AddChild(new Node('B'));
                sut.AddChild(new Node('C'));

                var actual = sut.NextLetters;

                actual.Should().Equal('A', 'B', 'C');
            }
        }

        public class GetChildrenShould
        {
            [Fact]
            public void ReturnChildren()
            {
                var sut = new Node('A');
                sut.AddChild(new Node('B'));
                sut.AddChild(new Node('C'));
                sut.AddChild(new Node('A'));

                var actual = sut.GetChildren();

                actual.Select(x => x.Letter).Should().Equal('A', 'B', 'C');
            }
        }

        public class GetChildrenInReverseOrderShould
        {
            [Fact]
            public void ReturnChildrenInReverseOrder()
            {
                var sut = new Node('A');
                sut.AddChild(new Node('B'));
                sut.AddChild(new Node('C'));
                sut.AddChild(new Node('A'));

                var actual = sut.GetChildrenInReverseOrder();

                actual.Select(x => x.Letter).Should().Equal('C', 'B', 'A');
            }
        }

        public class GetChildShould
        {
            [Fact]
            public void ReturnChild()
            {
                var expectedChild = new Node('A');
                var sut = new Node('H');

                sut.AddChild(expectedChild);

                sut.GetChild(expectedChild.Letter).Should().Be(expectedChild);
            }

            [Fact]
            public void ReturnNullWhenChildNotFound()
            {
                var sut = new Node('H');

                sut.GetChild('A').Should().BeNull();
            }
        }

        public class AddChildShould
        {
            [Fact]
            public void AddNewNode()
            {
                var expectedChild = new Node('A');
                var sut = new Node('H');

                sut.AddChild(expectedChild);

                sut.GetChild(expectedChild.Letter).Should().Be(expectedChild);
            }

            [Fact]
            public void AddNewNodeAlphabetically()
            {
                var sut = new Node('A');
                sut.AddChild(new Node('B'));
                sut.AddChild(new Node('C'));
                sut.AddChild(new Node('A'));

                sut.GetChildren().Select(x => x.Letter).Should().Equal('A', 'B', 'C');
            }

            [Fact]
            public void NotAddNewNodeWhenNodeAlreadyExists()
            {
                var nodeH = new Node('H');
                var nodeA = new Node('A');
                nodeH.AddChild(nodeA);

                nodeH.AddChild(new Node('A'));

                nodeH.GetChild('A').Should().Be(nodeA);
            }
        }

        public class HasChildShould
        {
            [Fact]
            public void ReturnFalseIfNodeDoesNotExist()
            {
                var sut = new Node('A');

                var actual = sut.HasChild('B');

                actual.Should().BeFalse();
            }

            [Fact]
            public void ReturnTrueIfNodeExists()
            {
                var sut = new Node('A');
                sut.AddChild(new Node('B'));

                var actual = sut.HasChild('B');

                actual.Should().BeTrue();
            }
        }

        public class NextLetterShould
        {
            public class AddValueShould
            {
                [Fact]
                public void StoreTheValue()
                {
                    var sut = new Node('A');

                    sut.AddValue("ABER");

                    sut.HasValue.Should().BeTrue();
                    sut.Value.Should().Be("ABER");
                }
            }
        }

        [Fact]
        public void NextLettersShould_ReturnEmptyWhenNoNextLetters()
        {
            var sut = new Node('A');

            var actual = sut.NextLetters;

            actual.Should().BeEmpty();
        }

        [Fact]
        public void NextLettersShould_ReturnNextLetters()
        {
            var sut = new Node('A');
            sut.AddChild(new Node('B'));
            sut.AddChild(new Node('C'));
            sut.AddChild(new Node('A'));

            var actual = sut.NextLetters;

            actual.Should().Equal('A', 'B', 'C');
        }
    }
}