using Microsoft.Xna.Framework;

namespace Gladiator_Wars
{
    internal interface MovableObject
    {
        float velocity { get; set; }
        void MoveObject (GameTime gameTime);
    }
}
