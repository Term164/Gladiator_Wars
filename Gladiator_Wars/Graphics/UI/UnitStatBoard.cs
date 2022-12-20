using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gladiator_Wars
{
    internal class UnitStatBoard : GraphicsObject
    {
        Gladiator unit;
        SpriteFont font;

        Text HealthPoints;
        Text ExperiencePoints;
        Text MoveDistance;
        Text ArmourPoints;
        Text WeaponDamage;

        Background Background;


        public UnitStatBoard(Vector2 position, Gladiator unit, SpriteFont font) : base(position, Vector2.Zero, null)
        {
            this.unit = unit;
            this.font = font;
            Background = new Background(position, 4);
            HealthPoints = new Text((position + new Vector2(10,2))*Renderer.SCALE,"HP: " + unit.healthPoints.ToString(),font,Color.White);
            ExperiencePoints = new Text((position + new Vector2(10, 12)) * Renderer.SCALE, "XP: " + unit.experiencePoints.ToString(), font, Color.White);
            MoveDistance = new Text((position + new Vector2(10, 22)) * Renderer.SCALE, "MP: " + unit.moveDistance.ToString(), font, Color.White);
            ArmourPoints = new Text((position + new Vector2(42, 2)) * Renderer.SCALE, "AP: " + unit.ArmourPoints.ToString(), font, Color.White);
            WeaponDamage = new Text((position + new Vector2(42, 12)) * Renderer.SCALE, "DMG: " + unit.getDamageValue().ToString(), font, Color.White);
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            Background.Draw(spriteBatch);
            HealthPoints.Draw(spriteBatch);
            ExperiencePoints.Draw(spriteBatch);
            MoveDistance.Draw(spriteBatch);
            ArmourPoints.Draw(spriteBatch);
            WeaponDamage.Draw(spriteBatch);
        }
    }
}
