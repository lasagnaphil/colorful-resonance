using System.Collections.Generic;

namespace Bosses
{
    public class TestBoss : BigMonster
    {
        public override void Setup()
        {
            collider = new List<Vector2i>()
            {
                new Vector2i(-1, -1), new Vector2i(0, -1), new Vector2i(1, -1),
                new Vector2i(-1, 0), new Vector2i(0, 0), new Vector2i(1, 0),
                new Vector2i(-1, 1), new Vector2i(0, 1), new Vector2i(1, 1)
            };
        }
    }
}