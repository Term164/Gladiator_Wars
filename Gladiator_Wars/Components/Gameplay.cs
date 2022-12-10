﻿using Gladiator_Wars.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections;

namespace Gladiator_Wars
{

    internal class Gameplay : GameComponent
    {
        // Game Components
        private HumanPlayer player;
        private Player player2;
        private PhysicsEngine _physicsEngine;
        private Level _currentLevel;
        private Renderer _renderer;


        public Gameplay(Game game) : base(game)
        {
            _currentLevel = new Level(game);
            _renderer = new Renderer(game, _currentLevel);
            _physicsEngine = new PhysicsEngine(game, _currentLevel);
            player = new HumanPlayer(game, _currentLevel);
            player2 = new Player(game, _currentLevel);

            player.UpdateOrder = 0;
            player2.UpdateOrder = 1;
            _physicsEngine.UpdateOrder = 2;
            _currentLevel.UpdateOrder = 3;
            this.UpdateOrder = 4;
            _renderer.UpdateOrder = 5;

            // add components to game list
            game.Components.Add(player);
            game.Components.Add(player2);
            game.Components.Add(_physicsEngine);
            game.Components.Add(_currentLevel);
            game.Components.Add(this);
            game.Components.Add(_renderer);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
