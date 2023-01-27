using Gladiator_Wars.Components;
using Gladiator_Wars.Infastructure;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using System.IO;
using System.Text.Json;

namespace Gladiator_Wars
{

    internal class Gameplay : GameComponent
    {
        public static string settingsFilePath = "settings.ini";
        // Game Components
        private HumanPlayer player;
        private AIPlayer player2;
        private PhysicsEngine _physicsEngine;
        private Level _currentLevel;
        private Renderer _renderer;
        private GUIRenderer _guiRenderer;
        private SoundManager soundManager;

        public Gameplay(Game game) : base(game)
        {
            _currentLevel = new Level(game);
            player = new HumanPlayer(game, _currentLevel);
            player2 = new AIPlayer(game, _currentLevel);
            _physicsEngine = new PhysicsEngine(game, _currentLevel);
            _renderer = new Renderer(game, _currentLevel);
            _guiRenderer = new GUIRenderer(game, _currentLevel);

            if (File.Exists(settingsFilePath))
            {
                Settings.gameSettings = LoadSettings();
            }

            soundManager = new SoundManager(game);

            player.UpdateOrder = 0;
            player2.UpdateOrder = 1;
            _physicsEngine.UpdateOrder = 2;
            _currentLevel.UpdateOrder = 3;
            this.UpdateOrder = 4;
            _renderer.UpdateOrder = 5;
            _guiRenderer.UpdateOrder = 6;

            // add components to game list
            game.Components.Add(player);
            game.Components.Add(player2);
            game.Components.Add(_physicsEngine);
            game.Components.Add(_currentLevel);
            game.Components.Add(this);
            game.Components.Add(_renderer);
            game.Components.Add(_guiRenderer);
        }

        public override void Update(GameTime gameTime)
        {
            
            if (player2.units.Count == 0 && _currentLevel.levelGameState == GameState.Level) // USER WINS
            {
                _currentLevel.levelGameState = GameState.Leveling;
                _currentLevel.levelNumber++;
                _currentLevel.loadLevel(_currentLevel.levelNumber);
            }
            else if (player.units.Count == 0 && _currentLevel.levelGameState == GameState.Level )// AI WINS
            {
                _currentLevel.levelGameState = GameState.LevelLost; // TODO: Make you lost screen with 2 buttons restart and main menu
                _currentLevel.levelNumber = 0;
                _currentLevel.reset();
                _currentLevel.GUI = _guiRenderer.LoseScreen;
            }
            base.Update(gameTime);
            
        }
        private Settings LoadSettings()
        {
            var FileContents = File.ReadAllText(settingsFilePath);
            return JsonSerializer.Deserialize<Settings>(FileContents);
        }
    }
}
