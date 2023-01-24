using Gladiator_Wars.GameObjects;
using Gladiator_Wars.Infastructure;
using Microsoft.Xna.Framework;
using System.Collections.Generic;


namespace Gladiator_Wars.Components
{
    internal class Player : GameComponent
    {

        public Level currentlevel;
        public List<Gladiator> units;

        public Tile? active = null;
        public List<Move>? possibleMoves = null;

        public bool hasTurn;

        public Player(Game game, Level level) : base(game)
        {
            units = new List<Gladiator>();
            currentlevel= level;

            // Register player to level
            if (level.player1 == null) level.player1 = this;
            else {
                // Initialize AI units
                level.player2 = this;
                level.loadPlayerUnits();
            }
            

        }

        public virtual void setActiveTile(Tile selectedTile)
        {
            if (selectedTile.unit != null)
            {
                active = selectedTile;
            }
        }

        private void resetActiveTile()
        {
            active = null;
            possibleMoves = null;
        }

        public void makeMove(Move move)
        {
            currentlevel.addNextMove(move);
            resetActiveTile();
        }

        public void RemoveGladiator(Gladiator gladiator)
        {
            units.Remove(gladiator);
            currentlevel._currentScene.removeItem(gladiator);
        }

        public override void Update(GameTime gameTime)
        {
            if(this == currentlevel.player2)
            {
                if(units.Count < 1)
                {

                    //CreateNewGladiator(Level.BOARD_WIDTH-2, Level.BOARD_HEIGHT-2);
                }
            }
            base.Update(gameTime);
        }
    }
}
