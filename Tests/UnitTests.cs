using System;
using System.Collections.Generic;
using System.Linq;
using GraphTest;
using Xunit;

namespace GraphTests
{
    public class UnitTests
    {
        [Fact]
        public void Test1()
        {
            var node = new Node("node", new Node[0]);
            var result = Walker.CreatePath(node, node);
            Assert.Single(result);
            Assert.Equal(node, result[0]);
        }

        [Fact]
        public void Test2()
        {
            var node1 = new Node("node1", new Node[0]);
            var node2 = new Node("node2", new Node[0]);

            var result = Walker.CreatePath(node1, node2);
            Assert.True(!result.Any());
        }

        [Fact]
        public void Test3()
        {
            var node1NeighborArray = new Node[1];
            var node1 = new Node("node1", node1NeighborArray);

            var node2NeighborArray = new Node[1];
            var node2 = new Node("node2", node2NeighborArray);

            node1NeighborArray[0] = node2;
            node2NeighborArray[0] = node1;

            var result = Walker.CreatePath(node1, node2);
            Assert.Equal(2, result.Count);
            Assert.Equal(node1, result[0]);
            Assert.Equal(node2, result[1]);
        }

        [Fact]
        public void Test4()
        {
            var node1NeighborArray = new Node[2];
            var node1 = new Node("node1", node1NeighborArray);

            var node2NeighborArray = new Node[3];
            var node2 = new Node("node2", node2NeighborArray);

            var node3NeighborArray = new Node[3];
            var node3 = new Node("node3", node3NeighborArray);

            var node4NeighborArray = new Node[2];
            var node4 = new Node("node4", node4NeighborArray);

            node1NeighborArray[0] = node2;
            node1NeighborArray[1] = node3;

            node2NeighborArray[0] = node1;
            node2NeighborArray[1] = node3;
            node2NeighborArray[2] = node4;

            node3NeighborArray[0] = node1;
            node3NeighborArray[1] = node2;
            node3NeighborArray[2] = node4;

            node4NeighborArray[0] = node3;
            node4NeighborArray[1] = node4;

            var result = Walker.CreatePath(node1, node4);
            CheckResult(node1, node4, result);
        }

        [Fact]
        public void Test5()
        {
            var node1NeighborArray = new Node[3];
            var node1 = new Node("node1", node1NeighborArray);

            var node2NeighborArray = new Node[1];
            var node2 = new Node("node2", node2NeighborArray);

            var node3NeighborArray = new Node[1];
            var node3 = new Node("node3", node3NeighborArray);

            var node4NeighborArray = new Node[1];
            var node4 = new Node("node4", node4NeighborArray);

            var node5NeighborArray = new Node[5];
            var node5 = new Node("node5", node5NeighborArray);

            var node6NeighborArray = new Node[1];
            var node6 = new Node("node6", node6NeighborArray);

            var node7NeighborArray = new Node[2];
            var node7 = new Node("node7", node7NeighborArray);

            node1NeighborArray[0] = node2;
            node1NeighborArray[1] = node5;
            node1NeighborArray[2] = node7;

            node2NeighborArray[0] = node1;

            node3NeighborArray[0] = node5;

            node4NeighborArray[0] = node5;

            node5NeighborArray[0] = node1;
            node5NeighborArray[1] = node3;
            node5NeighborArray[2] = node4;
            node5NeighborArray[3] = node6;
            node5NeighborArray[4] = node7;

            node6NeighborArray[0] = node5;

            node7NeighborArray[0] = node1;
            node7NeighborArray[1] = node5;

            var result = Walker.CreatePath(node1, node4);
            CheckResult(node1, node4, result);

            result = Walker.CreatePath(node7, node6);
            CheckResult(node7, node6, result);

            result = Walker.CreatePath(node2, node3);
            CheckResult(node2, node3, result);

            result = Walker.CreatePath(node2, node6);
            CheckResult(node2, node6, result);
        }

        [Fact]
        public void Test6()
        {
            var node1Neighbors = new Node[2];
            var node1 = new Node("N1", node1Neighbors);

            var node2Neighbors = new Node[2];
            var node2 = new Node("N2", node2Neighbors);

            var node3Neighbors = new Node[3];
            var node3 = new Node("N3", node3Neighbors);
            
            var node33 = new Node("N33", Array.Empty<Node>());

            node1Neighbors[0] = node2;
            node1Neighbors[1] = node3;

            node2Neighbors[0] = node1;
            node2Neighbors[1] = node3;

            node3Neighbors[0] = node1;
            node3Neighbors[1] = node2;
            node3Neighbors[2] = node33;
            

            var result = Walker.CreatePath(node1, node33);
            CheckResult(node1, node33, result);
        }

        private static void CheckResult(Node from, Node to, List<Node> result)
        {
            using (var nodeIterator = result.GetEnumerator())
            {
                nodeIterator.MoveNext();
                var prev = nodeIterator.Current;
                Assert.Equal(from, prev);
                while (nodeIterator.MoveNext())
                {
                    var current = nodeIterator.Current;
                    Assert.True(prev.ContainsNeighbor(current));
                    prev = current;
                }

                Assert.Equal(to, prev);
            }
        }
    }
}