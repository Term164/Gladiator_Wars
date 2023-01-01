using Gladiator_Wars.Infastructure;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Gladiator_Wars.Components
{
    enum PLAYER_STATE
    {
        ATTACK,
        MOVE,
        HEAL,
        DEFAULT
    }

    internal class HumanPlayer : Player
    {
        MouseState mouse;
        MouseState previousMouseState;
        public Gladiator selectedUnit;
        PLAYER_STATE CURRENT_STATE = PLAYER_STATE.DEFAULT;

        public HumanPlayer(Game game, Level level) : base(game, level) {
            Gladiator gladiator =  CreateNewGladiator(1,1);
            gladiator.Athletics = 10;
            gladiator.weapon = new Axe(Quality.common);
            gladiator.armour = new HeavyArmour(Quality.common);

            gladiator = CreateNewGladiator(2, 2);
            gladiator.Athletics = 5;
            gladiator.armour = new MediumArmour(Quality.common);
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
            
            if(CURRENT_STATE == PLAYER_STATE.DEFAULT)
            {
                if(selectedTile.unit != null)
                {
                    selectedUnit = selectedTile.unit;
                    GUIRenderer.unitStatBoard.updateUnit();
                }
            }
            else
            {
                foreach (Move move in possibleMoves)
                {
                    if (move.endPos == selectedTile)
                    {
                        makeMove(move);
                        break;
                    }
                }
            }

            updateTilesColor();
        }


        public override void setActiveTile(Tile tile)
        {
            base.setActiveTile(tile);
            selectedUnit = tile.unit;
            resetState();
            updateTilesColor();
            if (GUIRenderer.unitStatBoard != null) {
                GUIRenderer.resetButtons();
                GUIRenderer.unitStatBoard.updateUnit();
            }
            
        }

        public virtual void getUnitMoveOptions(object INFO)
        {
            if (CURRENT_STATE == PLAYER_STATE.MOVE)
                resetState();
            else
            {
                CURRENT_STATE = PLAYER_STATE.MOVE;
                possibleMoves = currentlevel.getAllUnitMovementMoves(active);
            }
            updateTilesColor();
        }

        public virtual void getUnitAttackOptions(object INFO)
        {

            if (CURRENT_STATE == PLAYER_STATE.ATTACK || !active.unit.attackPoint)
                resetState();
            else
            {
                CURRENT_STATE = PLAYER_STATE.ATTACK;
                possibleMoves = currentlevel.getAllUnitAttackMoves(active);
            }
            updateTilesColor();
        }

        public void unitBlock()
        {

        }

        public void getUnitHealOptions()
        {

        }

        private void resetState()
        {
            CURRENT_STATE = PLAYER_STATE.DEFAULT;
            possibleMoves = null;
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

            selectedUnit.boardPosition.tint = Color.Purple;
            

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
