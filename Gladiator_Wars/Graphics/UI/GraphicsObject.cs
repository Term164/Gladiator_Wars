using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Gladiator_Wars
{
    internal class GraphicsObject
    {

        public Vector2 position;
        public Vector2 dimensions;
        public Color color;
        public Sprite sprite;

        public GraphicsObject(Vector2 position, Vector2 dimensions, Sprite sprite)
        {
            this.position = position;
            this.dimensions = dimensions;
            this.sprite = sprite;
            color = Color.White;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite.texture, position, sprite.sourceRectangle, color,0,new Vector2(0,0),4,SpriteEffects.None,0);
        }

        public virtual void Update()
        {

        }
    }
}
