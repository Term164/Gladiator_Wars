using Microsoft.Xna.Framework;

namespace Gladiator_Wars
{
    internal class GameObject
    {
        public Vector2 position;
        public Tile boardPosition;
        public Color tint = Color.White;


        public GameObject(Vector2 position, Tile boardPosition)
        {
            this.position = position;
            this.boardPosition = boardPosition;
        }

        public GameObject()
        {

        }

        public static int convertPositionToBoardPosition(float position)
        {
            return (int)(position / Tile.TILE_SIZE);
        }
    }
}
