using Gladiator_Wars.Components;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gladiator_Wars
{
    internal class PhysicsEngine : GameComponent
    {
        private Level _currentLevel;

        public PhysicsEngine(Game game, Level currentLevel) : base(game)
        {
            _currentLevel = currentLevel;
        }

        public override void Update(GameTime gameTime)
        {

            moveObjects(gameTime);
            base.Update(gameTime);
        }

        private void moveObjects(GameTime gameTime)
        {
            foreach(GameObject gameObject in _currentLevel._currentScene.getSceneComponents())
            {
                if (gameObject is MovableObject) ((MovableObject)gameObject).MoveObject(gameTime);
            }
        }

    }
}
