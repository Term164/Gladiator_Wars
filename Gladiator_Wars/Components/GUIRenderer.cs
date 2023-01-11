using Gladiator_Wars.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections;

namespace Gladiator_Wars

{
    internal class GUIRenderer : DrawableGameComponent
    {
        private Level _level;
        private SpriteBatch _spriteBatch;
        HumanPlayer player;

        // GUI SPRITES
        Sprite menuBackgroundSprite;
        Sprite regularButtonSprite;
        Sprite blockActionButtonSprite;
        Sprite healActionButtonSprite;
        Sprite attackActionButtonSprite;
        Sprite moveActionButtonSprite;
        Sprite defaultActionButton;
        public static Sprite backgroundSpriteCenter;
        public static Sprite backgroundSpriteLeft;
        public static Sprite backgroundSpriteRight;

        // GUI FONTS
        SpriteFont font;
        SpriteFont statFont;
        SpriteFont MenuFont;

        // GUI ELEMENTS
        public static DamageText damageText;
        public static UnitStatBoard unitStatBoard;

        public static Button attackButton;
        public static Button moveButton;
        public static Button blockButton;
        public static Button healButton;

        public ArrayList MainMenu;
        public ArrayList LevelUI;
        public ArrayList RulesMenu;
        public ArrayList SettingsMenu;

        public GUIRenderer(Game game, Level level) : base(game)
        {
            _level = level;
            player = (HumanPlayer)_level.player1;
            MainMenu = new ArrayList();
            LevelUI = new ArrayList();
            RulesMenu = new ArrayList();
            SettingsMenu = new ArrayList();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Texture2D spriteSheetTexture = Game.Content.Load<Texture2D>("Assets/spritesheet");
            Texture2D backgroundTexture = Game.Content.Load<Texture2D>("Assets/backgroundAnimation");
            font = Game.Content.Load<SpriteFont>("Assets/Main");
            statFont = Game.Content.Load<SpriteFont>("Assets/stats");
            MenuFont = Game.Content.Load<SpriteFont>("Assets/Menu");

            //menuBackgroundSprite = new Sprite(backgroundTexture, new Rectangle(0, 0, 1200, 720));
            menuBackgroundSprite = new AnimatedSprite(backgroundTexture, new Vector2(2,3), new Vector2(1200,720), 11);

            regularButtonSprite = new Sprite(spriteSheetTexture, new Rectangle(32, 43, 32, 11));
            
            blockActionButtonSprite = new Sprite(spriteSheetTexture, new Rectangle(2*32, 1*32, 32, 32));
            healActionButtonSprite = new Sprite(spriteSheetTexture, new Rectangle(3*32, 3*32, 32, 32));
            attackActionButtonSprite = new Sprite(spriteSheetTexture, new Rectangle(3*32, 2*32, 32, 32));
            moveActionButtonSprite = new Sprite(spriteSheetTexture, new Rectangle(4*32, 3 * 32, 32, 32));
            defaultActionButton = new Sprite(spriteSheetTexture, new Rectangle(2*32,0,32,32));

            backgroundSpriteCenter = new Sprite(spriteSheetTexture, new Rectangle(67,0,25,32));
            backgroundSpriteLeft = new Sprite(spriteSheetTexture, new Rectangle(64, 0, 3, 32));
            backgroundSpriteRight = new Sprite(spriteSheetTexture, new Rectangle(92, 0, 4, 32));


            //_level.GUI.Add(new Button(new Vector2(0,0), new Vector2(32,11), regularButtonSprite, font, "Play", null, null, false));
            //_level.GUI.Add(new Background(new Vector2(0,0), 4));
            //_level.GUI.Add(new Button(new Vector2(0,0), new Vector2(32,32),blockActionButtonSprite, font, "", null, null, false));
            //_level.GUI.Add(new Button(new Vector2(400,400), new Vector2(32,32), defaultActionButton, attackActionButtonSprite, 1, null, null, true));
            //_level.GUI.Add(new Text(new Vector2(400,400),"test", font, Color.Green));

            CreateMainMenu();
            CreateLevelUI();
            CreateRulesMenu();
            CreateSettingsMenu();

            _level.GUI = MainMenu;

            base.LoadContent();
            SoundManager.PlayBackgroundMusic();
        }

        private void CreateRulesMenu()
        {
            int screenWidth = GraphicsDevice.Viewport.Width;

            GraphicsObject background = new GraphicsObject(new Vector2(0, 0), new Vector2(1200, 720), menuBackgroundSprite);
            background.size = 1.6f;
            background.color = Color.Gray;
            RulesMenu.Add(background);

            // Menu Title
            string title = "RULES";
            Vector2 titleSize = MenuFont.MeasureString(title);
            RulesMenu.Add(new Text(new Vector2(screenWidth / 2 - titleSize.X / 2, 20), title, MenuFont, Color.White));

            string descriptionString = "DESCRIPTION:\n\nGladiator wars is a turn based strategy game with roguelike elements.\nIt is heavily inspired by the game Battle Brothers and focuses on the figthing part of the game.";
            RulesMenu.Add(new Text(new Vector2(screenWidth / 4, 200), descriptionString, statFont, Color.White));

            string rulesString = "HOW TO PLAY:\n\n" +
                                 "Gladiator wars is a single player turn based strategy game\n" +
                                 "where you and the AI take turns controlling their gladiators.\n\n" +
                                 "Each gladiator has 2 action points that can be used to attack, move, block or heal.\n" +
                                 "Moving costs 1 action point and is limited by the gladiator characteristics.\n" +
                                 "Attacking costs 1 action point but can only be used once per turn.\n" +
                                 "Blocking costs 1 action point and it immediatly ends your turn.\n" +
                                 "Healing also costs one turn but does not end your turn (heal ally or yourself).\n" +
                                 "There is also a skip option if you just want to skip that turn.\n\n" +
                                 "Gladiator turn order is determined by the initative and weight of each gladiator.\n" +
                                 "Each player must finish the turn of the current gladiator before using another.";
            RulesMenu.Add(new Text(new Vector2(screenWidth / 4, 320), rulesString, statFont, Color.White));

            Button backButton = new Button(new Vector2(screenWidth / 2 - 32 * 4, 768), new Vector2(32, 11), regularButtonSprite, font, "Back", backToMainMenu, null, false);
            backButton.size = 8;
            RulesMenu.Add(backButton);

        }

        private void CreateSettingsMenu()
        {
            int screenWidth = GraphicsDevice.Viewport.Width;

            GraphicsObject background = new GraphicsObject(new Vector2(0, 0), new Vector2(1200, 720), menuBackgroundSprite);
            background.size = 1.6f;
            background.color = Color.Gray;
            SettingsMenu.Add(background);

            // Menu Title
            string title = "SETTINGS";
            Vector2 titleSize = MenuFont.MeasureString(title);
            SettingsMenu.Add(new Text(new Vector2(screenWidth / 2 - titleSize.X / 2, 20), title, MenuFont, Color.White));

            string descriptionString = "VOLUME:";
            SettingsMenu.Add(new Text(new Vector2(screenWidth / 4, 200), descriptionString, statFont, Color.White));

            Button toggleVolume = new Button(new Vector2(screenWidth / 4 + 100, 200 - 8), new Vector2(32, 32), defaultActionButton, healActionButtonSprite, 1, null, null, true);
            toggleVolume.defaultColor = Color.Red;
            SettingsMenu.Add(toggleVolume);

            Button backButton = new Button(new Vector2(screenWidth / 2 - 32 * 4, 768), new Vector2(32, 11), regularButtonSprite, font, "Back", backToMainMenu, null, false);
            backButton.size = 8;
            SettingsMenu.Add(backButton);

        }

        private void CreateMainMenu()
        {
            int screenWidth = GraphicsDevice.Viewport.Width;

            GraphicsObject background = new GraphicsObject(new Vector2(0, 0), new Vector2(1200, 720), menuBackgroundSprite);
            background.size = 1.6f;
            MainMenu.Add(background);

            // Menu Title
            string title = "GLADIATOR WARS";
            Vector2 titleSize = MenuFont.MeasureString(title);
            MainMenu.Add(new Text(new Vector2(screenWidth/2 - titleSize.X/2, 20), title, MenuFont, Color.White));

            // menu buttons
            Button continueButton = new Button(new Vector2(screenWidth / 2 - 32*4, 256), new Vector2(32, 11), regularButtonSprite, font, "Continue", null, null, false);
            continueButton.size = 8;
            MainMenu.Add(continueButton);

            Button newGameButton = new Button(new Vector2(screenWidth / 2 - 32 * 4, 384), new Vector2(32, 11), regularButtonSprite, font, "New Game", startNewGame, null, false);
            newGameButton.size = 8;
            MainMenu.Add(newGameButton);

            Button howToPlayButton = new Button(new Vector2(screenWidth / 2 - 32 * 4, 512), new Vector2(32, 11), regularButtonSprite, font, "Rules", switchToRules, null, false);
            howToPlayButton.size = 8;
            MainMenu.Add(howToPlayButton);

            Button settingsButton = new Button(new Vector2(screenWidth / 2 - 32 * 4, 640), new Vector2(32, 11), regularButtonSprite, font, "Settings", openSettingsMenu, null, false);
            settingsButton.size = 8;
            MainMenu.Add(settingsButton);

            Button exitButton = new Button(new Vector2(screenWidth / 2 - 32 * 4, 768), new Vector2(32, 11), regularButtonSprite, font, "Exit", exitGame, null, false);
            exitButton.size = 8;
            MainMenu.Add(exitButton);


        }

        public virtual void startNewGame(object INFO)
        {
            _level.GUI = LevelUI;
        }

        public virtual void openSettingsMenu(object INFO)
        {
            _level.GUI = SettingsMenu;
        }

        public virtual void switchToRules(object INFO)
        {
            _level.GUI = RulesMenu;
        }

        public virtual void backToMainMenu(object INFO)
        {
            _level.GUI = MainMenu;
        }

        public virtual void exitGame(object INFO)
        {
            Game.Exit();
        }

        private void CreateLevelUI()
        {
            damageText = new DamageText(new Vector2(0, 0), "", font);
            LevelUI.Add(damageText);

            unitStatBoard = new UnitStatBoard(new Vector2(Tile.TILE_SIZE * Renderer.SCALE, Tile.TILE_SIZE * (Level.BOARD_HEIGHT - 1) * Renderer.SCALE - 5), player, statFont);
            LevelUI.Add(unitStatBoard);
            // BUTTONS
            attackButton = new Button(new Vector2(Tile.TILE_SIZE * 5 * Renderer.SCALE, Tile.TILE_SIZE * (Level.BOARD_HEIGHT - 1) * Renderer.SCALE + 5), new Vector2(32, 32), defaultActionButton, attackActionButtonSprite, 3, player.getUnitAttackOptions, null, true);
            blockButton = new Button(new Vector2(Tile.TILE_SIZE * 6 * Renderer.SCALE, Tile.TILE_SIZE * (Level.BOARD_HEIGHT - 1) * Renderer.SCALE + 5), new Vector2(32, 32), defaultActionButton, blockActionButtonSprite, 3, null, null, false);
            healButton = new Button(new Vector2(Tile.TILE_SIZE * 7 * Renderer.SCALE, Tile.TILE_SIZE * (Level.BOARD_HEIGHT - 1) * Renderer.SCALE + 5), new Vector2(32, 32), defaultActionButton, healActionButtonSprite, 3, null, null, true);
            moveButton = new Button(new Vector2(Tile.TILE_SIZE * 8 * Renderer.SCALE, Tile.TILE_SIZE * (Level.BOARD_HEIGHT - 1) * Renderer.SCALE + 5), new Vector2(32, 32), defaultActionButton, moveActionButtonSprite, 3, player.getUnitMoveOptions, null, true);
            LevelUI.Add(attackButton);
            LevelUI.Add(blockButton);
            LevelUI.Add(healButton);
            LevelUI.Add(moveButton);
        }

        public static void resetButtons()
        {
            attackButton.isToggled = false;
            moveButton.isToggled = false;
            healButton.isToggled = false;
        }

        public override void Update(GameTime gameTime)
        {
            foreach (GraphicsObject GUIElement in _level.GUI)
            {
                GUIElement.Update(gameTime);
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
