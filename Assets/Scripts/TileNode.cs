using System.Linq;
using System.Collections.Generic;

namespace Algorithm
{
    public enum Direction
    {
        Left = 0,
        Right,
        Up,
        Down
    }

    public class TileNode
    {
        public Tile tile;
        public List<TileNode> nodes;
        public bool marked = false;
    }
}