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

        // GUI SPRITES
        public static Sprite regularButtonSprite;
        public static Sprite blockActionButtonSprite;
        public static Sprite healActionButtonSprite;
        public static Sprite attackActionButtonSprite;
        public static Sprite moveActionButtonSprite;
        public static Sprite defaultActionButton;

        public static Sprite backgroundSpriteCenter;
        public static Sprite backgroundSpriteLeft;
        public static Sprite backgroundSpriteRight;


        public static DamageText damageText;

        public GUIRenderer(Game game, Level level) : base(game)
        {
            _level = level;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Texture2D spriteSheetTexture = Game.Content.Load<Texture2D>("Assets/spritesheet");
            SpriteFont font = Game.Content.Load<SpriteFont>("Assets/Main");

            regularButtonSprite = new Sprite(spriteSheetTexture, new Rectangle(32, 43, 32, 11));
            
            blockActionButtonSprite = new Sprite(spriteSheetTexture, new Rectangle(35, 35, 32, 32));
            healActionButtonSprite = new Sprite(spriteSheetTexture, new Rectangle(35, 103, 32, 32));
            attackActionButtonSprite = new Sprite(spriteSheetTexture, new Rectangle(3*32, 2*32, 32, 32));
            moveActionButtonSprite = new Sprite(spriteSheetTexture, new Rectangle(137, 35, 32, 32));
            defaultActionButton = new Sprite(spriteSheetTexture, new Rectangle(2*32,0,32,32));

            backgroundSpriteCenter = new Sprite(spriteSheetTexture, new Rectangle(67,0,25,32));
            backgroundSpriteLeft = new Sprite(spriteSheetTexture, new Rectangle(64, 0, 3, 32));
            backgroundSpriteRight = new Sprite(spriteSheetTexture, new Rectangle(92, 0, 4, 32));


            damageText = new DamageText(new Vector2(0,0),"",font);
            _level.GUI.Add(damageText);

            //_level.GUI.Add(new Button(new Vector2(0,0), new Vector2(32,11), regularButtonSprite, font, "Play", null, null, false));
            //_level.GUI.Add(new Background(new Vector2(0,0), 4));
            //_level.GUI.Add(new Button(new Vector2(0,0), new Vector2(32,32),blockActionButtonSprite, font, "", null, null, false));
            //_level.GUI.Add(new Button(new Vector2(400,400), new Vector2(32,32), defaultActionButton, attackActionButtonSprite, 1, null, null, true));
            //_level.GUI.Add(new Text(new Vector2(400,400),"test", font, Color.Green));
            _level.GUI.Add(new UnitStatBoard(new Vector2(0, (Level.BOARD_HEIGHT - 1) * 32),_level.player1.active.unit,font));

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
