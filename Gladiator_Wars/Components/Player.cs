using Gladiator_Wars.Infastructure;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;

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
            this.currentlevel= level;

            // Register player to level
            if (level.player1 == null) level.player1 = this;
            else {
                // Initialize AI units
                level.player2 = this;
                level.loadPlayerUnits(this);
            }
            

        }

        public virtual void setActiveTile(Tile selectedTile)
        {
            if (selectedTile.unit != null)
            {
                active = selectedTile;
                possibleMoves = currentlevel.getUnitMoves(active);
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

        public Gladiator CreateNewGladiator(int boardX, int boardY)
        {
            Gladiator unit = new Gladiator(new Vector2(boardX * Tile.TILE_SIZE, boardY * Tile.TILE_SIZE), currentlevel.Board[boardX,boardY], this);
            currentlevel.Board[boardX, boardY].unit = unit;
            currentlevel._currentScene.addItem(unit);
            return unit;
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
                    CreateNewGladiator(Level.BOARD_WIDTH-2, Level.BOARD_HEIGHT-2);
                }
            }
            base.Update(gameTime);
        }
    }
}
