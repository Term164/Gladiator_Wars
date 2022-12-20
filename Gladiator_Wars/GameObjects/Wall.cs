using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gladiator_Wars
{
    internal class Wall : Tile
    {
        public float rotation;
        public bool isCorner;
        public Vector2 origin;
        public Wall(Vector2 position, Tile BoardPosition, float rotation, bool isCorner, Vector2 origin) : base(position, BoardPosition)
        {
            this.rotation = rotation;   
            this.isCorner = isCorner;
            this.origin = origin;
        }
    }
}
