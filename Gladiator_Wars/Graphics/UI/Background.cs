using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gladiator_Wars
{
    internal class Background : GraphicsObject
    {
        public int size;

        public Background(Vector2 position, int size) : base(position, new Vector2(25*size+7,32), null)
        {
            this.size = size;
            color = Color.SaddleBrown;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(GUIRenderer.backgroundSpriteLeft.texture, position, GUIRenderer.backgroundSpriteLeft.sourceRectangle, color, 0, Vector2.Zero, Renderer.SCALE, SpriteEffects.None, 0);

            for(int i = 1; i <= size; i++)
            {
                spriteBatch.Draw(GUIRenderer.backgroundSpriteCenter.texture, position + new Vector2(dimensions.X - 4 - i*25, 0) * new Vector2(Renderer.SCALE,1), GUIRenderer.backgroundSpriteCenter.sourceRectangle,color, 0, Vector2.Zero, Renderer.SCALE, SpriteEffects.None, 0);
            }

            spriteBatch.Draw(GUIRenderer.backgroundSpriteRight.texture, position + new Vector2(dimensions.X-4,0) * new Vector2(Renderer.SCALE, 1), GUIRenderer.backgroundSpriteRight.sourceRectangle, color, 0, Vector2.Zero, Renderer.SCALE, SpriteEffects.None, 0);
        }
    }
}
