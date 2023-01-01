using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Gladiator_Wars
{
    internal class Sprite
    {
        public Texture2D texture;
        public Rectangle sourceRectangle;

        public Sprite(Texture2D texture, Rectangle sourceRectangle)
        {
            this.texture = texture;
            this.sourceRectangle = sourceRectangle;
        }   

        public virtual void Update(GameTime gameTime) { }
    }
}
