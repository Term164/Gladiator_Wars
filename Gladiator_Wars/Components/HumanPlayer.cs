using Gladiator_Wars.Infastructure;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gladiator_Wars.Components
{
    internal class HumanPlayer : Player
    {
        MouseState mouse;
        MouseState previousMouseState;

        public HumanPlayer(Game game, Level level) : base(game, level) {
            Gladiator gladiator =  CreateNewGladiator(1,1);
            gladiator.Athletics = 10;

            gladiator = CreateNewGladiator(2, 2);
            gladiator.Athletics = 5;
        }

        public override void Update(GameTime gameTime)
        {
            if (playerClicked() && hasTurn)
            {
                if (mouse.X > 0 && mouse.X < Level.BOARD_WIDTH * Tile.TILE_SIZE * Renderer.SCALE
                    && mouse.Y > 0 && mouse.Y < Level.BOARD_HEIGHT * Tile.TILE_SIZE * Renderer.SCALE)
                {
                    int boardCoordX = (int)(mouse.X / Renderer.SCALE / Tile.TILE_SIZE);
                    int boardCoordY = (int)(mouse.Y / Renderer.SCALE / Tile.TILE_SIZE);
                    boardClick(new Vector2(boardCoordX, boardCoordY));
                }
            }
            base.Update(gameTime);
        }

        private void boardClick(Vector2 boardClickPosition) {
            Tile selectedTile = currentlevel.Board[(int)boardClickPosition.X, (int)boardClickPosition.Y];

            // TODO: Remove this if once the UI is established
            foreach (Move move in possibleMoves)
            {
                if (move.endPos == selectedTile) {
                    makeMove(move);
                    break;
                }
            }

            //if (active == null) setActiveTile(selectedTile);
            //else {
            //    if (active == selectedTile)
            //    {
            //        //resetActiveTile();
            //    }
            //    else if (possibleMoves.Contains(selectedTile))
            //    {
            //        makeMove(selectedTile);
            //    }
            //}

            updateTilesColor();
        }


        public override void setActiveTile(Tile tile)
        {
            base.setActiveTile(tile);
            updateTilesColor();
        }


        private void updateTilesColor() {

            // Clear the tile colors
            for (int x = 0; x < Level.BOARD_WIDTH; x++)
            {
                for (int y = 0; y < Level.BOARD_HEIGHT; y++)
                {
                    //currentlevel.Board[x, y].tint = Color.SandyBrown;
                    currentlevel.Board[x, y].tint = Color.White;
                }
            }

            if(active != null) active.tint = Color.Yellow;
            if(possibleMoves!= null)
            {
                foreach (Move move in possibleMoves)
                {
                    if (move.unitAction == Action.Attack) move.endPos.tint = Color.Red;
                    else move.endPos.tint = Color.Green;
                }
            }
            

        }

        private bool playerClicked() {
            // Check for player clicks on the game board
            mouse = Mouse.GetState();
            if (mouse.LeftButton == ButtonState.Released && previousMouseState.LeftButton == ButtonState.Pressed) {
                previousMouseState = mouse;
                return true;
            }
            previousMouseState = mouse;
            return false;
        }
    }
}
