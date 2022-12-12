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
        public static readonly float SCALE = 4;
        private Dictionary<string, Sprite> spriteLookupDictionary;

        public Renderer(Game game, Level level) : base(game)
        {
            _currentLevel = level;
            spriteLookupDictionary = new Dictionary<string, Sprite>();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Texture2D spriteSheetTexture = Game.Content.Load<Texture2D>("Assets/spritesheet");

            // Define all sprites for all drawable objects in the game;
            spriteLookupDictionary["Gladiator"] = new Sprite(spriteSheetTexture, new Rectangle(1,102,32,32));
            spriteLookupDictionary["Tile"] = new Sprite(spriteSheetTexture, new Rectangle(103, 69, 32, 32));


            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin(samplerState:SamplerState.PointClamp, transformMatrix:Matrix.CreateScale(SCALE));

            foreach(GameObject entity in _currentLevel._currentScene.getSceneComponents())
            {
                Sprite entitySprite = spriteLookupDictionary[entity.GetType().Name];
                if(entity is Gladiator)
                {
                    if (((Gladiator)entity).player is HumanPlayer)
                    {
                        _spriteBatch.Draw(
                        entitySprite.texture,
                        entity.position,
                        entitySprite.sourceRectangle,
                        entity.tint,
                        0,
                        new Vector2(0,0),
                        1,SpriteEffects.FlipHorizontally,0);
                    }
                    else
                    {
                        _spriteBatch.Draw(
                        entitySprite.texture,
                        entity.position,
                        entitySprite.sourceRectangle,
                        entity.tint);
                    }
                }
                else
                {
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
    }
}
