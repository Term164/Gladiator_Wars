using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace Gladiator_Wars

{
    internal class GUIRenderer : DrawableGameComponent
    {
        private Level _level;
        private SpriteBatch _spriteBatch;

        public GUIRenderer(Game game, Level level) : base(game)
        {
            _level = level;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Texture2D spriteSheetTexture = Game.Content.Load<Texture2D>("Assets/spritesheet");
            SpriteFont font = Game.Content.Load<SpriteFont>("Assets/Main");

            Sprite regularButtonSprite = new Sprite(spriteSheetTexture, new Rectangle(1, 46, 32, 11));
            Sprite blockActionButtonSprite = new Sprite(spriteSheetTexture, new Rectangle(35, 35, 32, 32));
            Sprite healActionButtonSprite = new Sprite(spriteSheetTexture, new Rectangle(35, 103, 32, 32));
            Sprite attackActionButtonSprite = new Sprite(spriteSheetTexture, new Rectangle(69, 69, 32, 32));
            Sprite moveActionButtonSprite = new Sprite(spriteSheetTexture, new Rectangle(137, 35, 32, 32));

            //_level.GUI.Add(new Button(new Vector2(0,0), new Vector2(32,11), regularButtonSprite, font, "Play", null, null, false));
            //_level.GUI.Add(new Button(new Vector2(0,0), new Vector2(32,32),blockActionButtonSprite, font, "", null, null, false));
            base.LoadContent();
        }        

        public override void Update(GameTime gameTime)
        {
            foreach (GraphicsObject GUIElement in _level.GUI)
            {
                GUIElement.Update();
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: Matrix.CreateScale(1));

            foreach (GraphicsObject GUIElement in _level.GUI)
            {
                GUIElement.Draw(_spriteBatch);
            }
            _spriteBatch.End();


            base.Draw(gameTime);
        }

    }
}
