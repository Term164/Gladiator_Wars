using Gladiator_Wars.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Gladiator_Wars
{
    internal class Renderer : DrawableGameComponent
    {
        private SpriteBatch _spriteBatch;
        private Level _currentLevel;
        public static readonly float SCALE = 4;

        public Renderer(Game game, Level level) : base(game)
        {
            _currentLevel = level;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            
            foreach(GameObject entity in _currentLevel._currentScene.getSceneComponents())
            {
                entity.loadContent(Game.Content.Load<Texture2D>(entity.texturePath));
            }
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin(samplerState:SamplerState.PointClamp, transformMatrix:Matrix.CreateScale(SCALE));

            foreach(GameObject entity in _currentLevel._currentScene.getSceneComponents())
            {
                if(entity is Gladiator)
                {
                    if (((Gladiator)entity).player is HumanPlayer)
                    {
                        _spriteBatch.Draw(
                        entity.sprite.texture,
                        entity.position,
                        entity.sprite.sourceRectangle,
                        entity.tint,
                        0,
                        new Vector2(0,0),
                        1,SpriteEffects.FlipHorizontally,0);
                    }
                    else
                    {
                        _spriteBatch.Draw(
                        entity.sprite.texture,
                        entity.position,
                        entity.sprite.sourceRectangle,
                        entity.tint);
                    }
                }
                else
                {
                    _spriteBatch.Draw(
                    entity.sprite.texture,
                    entity.position,
                    entity.sprite.sourceRectangle,
                    entity.tint);
                }
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
