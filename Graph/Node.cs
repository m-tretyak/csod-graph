using System.Diagnostics;

namespace GraphTest
{
#if DEBUG
    [DebuggerDisplay("node {_name}")]
#endif
    public class Node
    {
        private readonly string _name;
        private readonly Node[] _neighborArray;

        public Node(string name, Node[] neighborArray)
        {
            _name = name;
            _neighborArray = neighborArray;
        }

        public string GetName()
        {
            return _name;
        }

        public Node GetNeighbor(int index)
        {
            return _neighborArray[index];
        }

        public int GetNeighborCount()
        {
            return _neighborArray.Length;
        }

        public bool ContainsNeighbor(Node node)
        {
            for (var index = 0; index != _neighborArray.Length; ++index)
            {
                if (node == _neighborArray[index])
                {
                    return true;
                }
            }

            return false;
        }
    }
}