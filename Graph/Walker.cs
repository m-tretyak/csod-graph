using System;
using System.Collections.Generic;

namespace GraphTest
{
    public class Walker
    {
        public static List<Node> CreatePath(Node from, Node to)
        {
            if (from == null)
            {
                throw new ArgumentNullException(nameof(from));
            }

            if (to == null)
            {
                throw new ArgumentNullException(nameof(to));
            }

            var result = GetPath(from, to);

            return result;
        }

        private static List<Node> GetPath(Node from, Node to)
        {
            if (from == to)
            {
                return new List<Node>(1) { from };
            }

            var toVisit = new Queue<Node>();
            toVisit.Enqueue(from);
            EnqueueNeighbors(toVisit, from);

            var visited = new Stack<Node>();
            visited.Push(from);

            while (toVisit.Count > 0)
            {
                var current = toVisit.Dequeue();
                if (visited.Contains(current))
                {
                    continue;
                }

                visited.Push(current);
                if (current == to)
                {
                    return ReduceVisited(visited);
                }

                EnqueueNeighbors(toVisit, current);
            }

            return new List<Node>();
        }

        private static void EnqueueNeighbors(Queue<Node> queue, Node from)
        {
            if (queue == null)
            {
                throw new ArgumentNullException(nameof(queue));
            }

            if (from == null)
            {
                throw new ArgumentNullException(nameof(from));
            }

            var result = from.GetNeighborCount();
            for (int index = 0; index < result; index++)
            {
                queue.Enqueue(from.GetNeighbor(index));
            }
        }

        private static List<Node> ReduceVisited(Stack<Node> visited)
        {
            var result = new List<Node>(visited.Count);
            var current = visited.Pop();
            result.Add(current);
            while (visited.Count > 0)
            {
                var prev = visited.Pop();
                if (!prev.ContainsNeighbor(current))
                {
                    continue;
                }

                result.Add(prev);
                current = prev;
            }

            result.Reverse();

            return result;
        }
    }
}