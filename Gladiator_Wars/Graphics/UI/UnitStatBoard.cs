using Gladiator_Wars.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Gladiator_Wars
{
    internal class UnitStatBoard : GraphicsObject
    {
        HumanPlayer player;

        Text HealthPoints;
        Text ExperiencePoints;
        Text MoveDistance;
        Text ArmourPoints;
        Text WeaponDamage;
        Text ActionPoints;

        Background Background;


        public UnitStatBoard(Vector2 position, HumanPlayer player, SpriteFont font) : base(position, Vector2.Zero, null)
        {
            this.player = player;

            Gladiator unit = player.selectedUnit;
            if(unit != null )
            {

                Background = new Background(position, 4);
                HealthPoints = new Text(position + new Vector2(10,2) * Renderer.SCALE,"HP: " + unit.healthPoints,font,Color.White);
                ExperiencePoints = new Text(position + new Vector2(10, 12) * Renderer.SCALE, "XP: " + unit.experiencePoints, font, Color.White);
                WeaponDamage = new Text(position + new Vector2(10, 22) * Renderer.SCALE, "DMG: " + unit.getDamageValue(), font, Color.White);

                ActionPoints = new Text(position + new Vector2(42, 2) * Renderer.SCALE, "Action points: " + unit.getActionPoints(), font, Color.White);
                MoveDistance = new Text(position + new Vector2(42, 12) * Renderer.SCALE, "Move distance: " + unit.moveDistance, font, Color.White);
                ArmourPoints = new Text(position + new Vector2(42, 22) * Renderer.SCALE, "Armour points: " + unit.ArmourPoints, font, Color.White);

            }
        }

        public void updateUnit()
        {
            if(player.selectedUnit != null) 
            {
                Gladiator unit = player.selectedUnit;
                HealthPoints.setText("HP: " + unit.healthPoints);
                ExperiencePoints.setText("XP: " + unit.experiencePoints);
                WeaponDamage.setText("DMG: " + unit.getDamageValue());
                ActionPoints.setText("Action points: " + unit.getActionPoints());
                MoveDistance.setText("Move distance: " + unit.moveDistance);
                ArmourPoints.setText("Armour points: " + unit.ArmourPoints);
                
            }

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if(player.selectedUnit != null)
            {
                Background.Draw(spriteBatch);
                HealthPoints.Draw(spriteBatch);
                ExperiencePoints.Draw(spriteBatch);
                WeaponDamage.Draw(spriteBatch);
                ActionPoints.Draw(spriteBatch);
                MoveDistance.Draw(spriteBatch);
                ArmourPoints.Draw(spriteBatch);
            }
       }
    }
}
