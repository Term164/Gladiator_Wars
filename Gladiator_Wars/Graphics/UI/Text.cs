using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gladiator_Wars
{
    internal class Text : GraphicsObject
    {
        public string text;
        public SpriteFont font;

        public Text(Vector2 position, string text, SpriteFont font, Color color) : base(position, Vector2.Zero, null)
        {
            this.text = text;
            this.font = font;
            this.color = color;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, text, position*Renderer.SCALE, color);
        }
    }
}
