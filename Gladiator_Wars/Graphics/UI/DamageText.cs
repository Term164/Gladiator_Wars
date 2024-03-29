﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Gladiator_Wars
{
    internal class DamageText : Text
    {
        float opacity;
        bool isActive;

        public DamageText(Vector2 position, string text, SpriteFont font) : base(position, text, font, Color.Red){
            opacity = 1f;
            isActive = false;
        }

        public override void Update(GameTime gameTime)
        {
            if (isActive) {
                opacity -= 0.01f;
                color = new Color(255, 0, 0) * opacity;
                position.Y -= 1.5f;
                if (opacity <= 0) isActive= false;
            }
            else
            {
                isActive = false;
            }

            base.Update(gameTime);
        }

        public void resetDamage(Vector2 position, string text)
        {
            opacity = 1f;
            isActive = true;
            this.position= position;
            this.text = text;
        }
    }
}
