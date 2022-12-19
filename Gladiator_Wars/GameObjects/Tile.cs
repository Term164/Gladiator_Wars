
using Microsoft.Xna.Framework;

namespace Gladiator_Wars
{
    internal class Tile : GameObject
    {
        public Gladiator? unit;
        public static readonly int TILE_SIZE = 32;

        public Tile(Vector2 position, Tile BoardPosition) : base(position, BoardPosition)
        {
            base.tint = Color.SandyBrown;

        }
    }
}
