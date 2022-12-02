using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections;
using System.Diagnostics;

namespace Gladiator_Wars.Components
{
    internal class Player : GameComponent
    {

        public Level currentlevel;

        public Player(Game game, Level level) : base(game)
        {
            this.currentlevel= level;
        }



        public override void Update(GameTime gameTime)
        {
            
            base.Update(gameTime);
        }
    }
}
