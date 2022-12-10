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

        private Tile? active = null;
        private List<Tile>? possibleMoves = null;

        public HumanPlayer(Game game, Level level) : base(game, level) {
            CreateNewGladiator(0,0);
        }

        public override void Update(GameTime gameTime)
        {
            if (playerClicked())
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

            if (active == null) setActiveTile(selectedTile);
            else {
                if (active == selectedTile)
                {
                    resetActiveTile();
                }
                else if (possibleMoves.Contains(selectedTile))
                {
                    makeMove(selectedTile);
                }
            }

            updateTilesColor();
        }

        private void setActiveTile(Tile selectedTile) {
            if (selectedTile.unit != null) {
                active = selectedTile;
                possibleMoves = currentlevel.getUnitMoves(active);
            }
        }

        private void resetActiveTile()
        {
            active = null;
            possibleMoves = null;
        }

        private void makeMove(Tile next)
        {
            Move playerMove;
            if (next.unit != null) playerMove = new Move(active.unit, Action.Attack, next); 
            else playerMove = new Move(active.unit, Action.Move, next);
            currentlevel.addNextMove(playerMove);
            resetActiveTile();
        }

        private void updateTilesColor() {

            // Clear the tile colors
            for (int x = 0; x < Level.BOARD_WIDTH; x++)
            {
                for (int y = 0; y < Level.BOARD_HEIGHT; y++)
                {
                    currentlevel.Board[x, y].tint = Color.White;
                }
            }

            if(active != null) active.tint = Color.Yellow;
            if(possibleMoves!= null)
            {
                foreach (Tile tile in possibleMoves)
                {
                    if (tile.unit != null) tile.tint = Color.Red;
                    else tile.tint = Color.Green;
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
