using Gladiator_Wars.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Gladiator_Wars
{
    internal class Renderer : DrawableGameComponent
    {
        private SpriteBatch _spriteBatch;
        private Level _currentLevel;
        public static float SCALE = 1;
        private Dictionary<string, Sprite> spriteLookupDictionary;

        public Renderer(Game game, Level level) : base(game)
        {
            _currentLevel = level;
            spriteLookupDictionary = new Dictionary<string, Sprite>();

            calculateScale();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Texture2D spriteSheetTexture = Game.Content.Load<Texture2D>("Assets/spritesheet");

            // Define all sprites for all drawable objects in the game;
            spriteLookupDictionary["Gladiator"] = new Sprite(spriteSheetTexture, new Rectangle(2 * 32, 3 * 32, 32, 32));
            spriteLookupDictionary["Tile"] = new Sprite(spriteSheetTexture, new Rectangle(1 * 32, 3 * 32, 32, 32));
            spriteLookupDictionary["Wall"] = new Sprite(spriteSheetTexture, new Rectangle(1 * 32, 4 * 32, 32, 32));
            spriteLookupDictionary["CornerWall"] = new Sprite(spriteSheetTexture, new Rectangle(0, 3 * 32, 32, 32));

            // Armour Sprites
            spriteLookupDictionary["HeavyArmour"] = new Sprite(spriteSheetTexture, new Rectangle(4 * 32, 2 * 32, 32, 32));
            spriteLookupDictionary["MediumArmour"] = new Sprite(spriteSheetTexture, new Rectangle(4 * 32, 0, 32, 32));
            spriteLookupDictionary["LightArmour"] = new Sprite(spriteSheetTexture, new Rectangle(4 * 32, 1 * 32, 32, 32));

            // Weapon Sprites
            spriteLookupDictionary["Axe"] = new Sprite(spriteSheetTexture, new Rectangle(32, 0, 31, 32));
            spriteLookupDictionary["Bow"] = new Sprite(spriteSheetTexture, new Rectangle(0, 32, 32, 32));
            spriteLookupDictionary["Hands"] = new Sprite(spriteSheetTexture, new Rectangle(0, 0, 0, 0));
            spriteLookupDictionary["Spear"] = new Sprite(spriteSheetTexture, new Rectangle(32, 2 * 32, 32, 32));
            spriteLookupDictionary["Sword"] = new Sprite(spriteSheetTexture, new Rectangle(2 * 32, 2 * 32, 32, 32));

            //Shield Sprites
            spriteLookupDictionary["SmallShield"] = new Sprite(spriteSheetTexture, new Rectangle(0, 2 * 32, 32, 32));
            spriteLookupDictionary["BigShield"] = new Sprite(spriteSheetTexture, new Rectangle(3 * 32, 1 * 32, 32, 32));


            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            calculateScale();

            _spriteBatch.Begin(samplerState:SamplerState.PointClamp, transformMatrix:Matrix.CreateScale(SCALE));

            foreach(GameObject entity in _currentLevel._currentScene.getSceneComponents())
            {
                Sprite entitySprite = spriteLookupDictionary[entity.GetType().Name];

                if(entity is Gladiator)
                {
                    RenderGladiator((Gladiator)entity);
                }else if (entity is Wall)
                {
                    RenderWall((Wall) entity);
                }
                else {
                    _spriteBatch.Draw(
                    entitySprite.texture,
                    entity.position,
                    entitySprite.sourceRectangle,
                    entity.tint);
                }
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }

        private void RenderWall(Wall wall)
        {
            Sprite entitySprite;
            if (wall.isCorner) entitySprite = spriteLookupDictionary["CornerWall"];
            else entitySprite = spriteLookupDictionary["Wall"];

            _spriteBatch.Draw(
                        entitySprite.texture,
                        wall.position,
                        entitySprite.sourceRectangle,
                        wall.tint,
                        wall.rotation,
                        wall.origin,
                        1, SpriteEffects.None, 0);
        }

        private void RenderGladiator(Gladiator gladiator)
        {
            Sprite entitySprite = spriteLookupDictionary["Gladiator"];
            SpriteEffects team = gladiator.player is HumanPlayer ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

            

            _spriteBatch.Draw(
                entitySprite.texture,
                gladiator.position,
                entitySprite.sourceRectangle,
                gladiator.tint,
                0,
                new Vector2(0, 0),
                1, team, 0);

            if (gladiator.armour != null) RenderArmour(gladiator, team);
            if (gladiator.weapon != null) RenderWeapon(gladiator, team);
            if (gladiator.shield != null) RenderShield(gladiator, team);
        }

        private void RenderArmour(Gladiator gladiator, SpriteEffects team)
        {
            Sprite entitySprite = spriteLookupDictionary[gladiator.armour.GetType().Name];

            _spriteBatch.Draw(
                entitySprite.texture,
                gladiator.position,
                entitySprite.sourceRectangle,
                gladiator.armour.tint,
                0,
                new Vector2(0, 0),
                1, team, 0);
        }

        private void RenderWeapon(Gladiator gladiator, SpriteEffects team)
        {
            float rotation = 0;
            if(gladiator.weapon is Spear) {
                rotation = team == SpriteEffects.FlipHorizontally ? 0 : -MathF.PI + MathF.PI/10;
            }else if(gladiator.weapon is Bow){
                rotation = team == SpriteEffects.FlipHorizontally ? 0 : -MathF.PI;
            }
            
            Sprite entitySprite = spriteLookupDictionary[gladiator.weapon.GetType().Name];
            Vector2 offset = team == SpriteEffects.FlipHorizontally ? gladiator.weapon.offsetRight : gladiator.weapon.offsetLeft;

            _spriteBatch.Draw(
                entitySprite.texture,
                gladiator.position + offset,
                entitySprite.sourceRectangle,
                gladiator.weapon.tint,
                gladiator.weapon.rotation + rotation,
                new Vector2(0, 0),
                0.8f, SpriteEffects.None, 0);
        }

        private void RenderShield(Gladiator gladiator, SpriteEffects team)
        {
            Sprite entitySprite = spriteLookupDictionary[gladiator.shield.GetType().Name];
            Vector2 offset = team == SpriteEffects.FlipHorizontally ? gladiator.shield.offsetRight : gladiator.shield.offsetLeft;

            _spriteBatch.Draw(
                entitySprite.texture,
                gladiator.position + offset,
                entitySprite.sourceRectangle,
                gladiator.shield.tint,
                0,
                new Vector2(0, 0),
                0.8f, SpriteEffects.None, 0);
        }


        private void calculateScale()
        {
            int w = GraphicsDevice.Viewport.Width;
            SCALE = w / (float)((Level.BOARD_WIDTH) * Tile.TILE_SIZE);
        }

    }
}
