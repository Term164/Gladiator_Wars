using Gladiator_Wars.Components;
using Gladiator_Wars.Infastructure;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Gladiator_Wars
{
    internal class Level : GameComponent
    {

        public static readonly int BOARD_HEIGHT = 7, BOARD_WIDTH = 12;
        // Queue of characters to determine play order
        public Scene _currentScene;
        public Tile[,] Board;

        private Stack<Move> nextMoveStack;
        private List<Move> gameMoves;

        public Player player1;
        public Player player2;

        public Level(Game game) : base(game)
        {
            _currentScene = new Scene();
            gameMoves = new List<Move>();
            nextMoveStack = new Stack<Move>();
            initLevel();
        }

        // Initialize level with gameObjects
        public void initLevel()
        {
            Board = new Tile[BOARD_WIDTH, BOARD_HEIGHT];
            for (int x = 0; x < BOARD_WIDTH; x++)
            {
                for (int y = 0; y < BOARD_HEIGHT; y++)
                {
                    Board[x, y] = new Tile(new Vector2(x * Tile.TILE_SIZE, y * Tile.TILE_SIZE), Board[x,y]);
                    _currentScene.addItem(Board[x, y]);
                }
            }
        }

        public void loadPlayerUnits(Player player)
        {
            player.CreateNewGladiator(2, 0);
        }


        public override void Update(GameTime gameTime)
        {
            if(nextMoveStack.Count > 0)
            {
                Move nextMove = nextMoveStack.Pop();
                gameMoves.Add(nextMove);
                executeMove(nextMove);
            }
            base.Update(gameTime);
        }

        private void executeMove(Move move)
        {
            switch(move.unitAction)
            {
                case Action.Move:
                    moveUnit(move);
                    break;
                case Action.Attack:
                    attackUnit(move);
                    break;
                default: break;
            }
        }

        public void addNextMove(Move move) {
            nextMoveStack.Push(move);
        }


        private void attackUnit(Move unitMove) {
            unitMove.unit.nextNode = unitMove.endPos;
            unitMove.unit.boardPosition.unit = null;
            unitMove.unit.boardPosition = unitMove.endPos;
            unitMove.endPos.unit.RemoveGladiator();
            unitMove.endPos.unit = unitMove.unit;
        }

        private void moveUnit(Move unitMove) {
            unitMove.unit.nextNode = unitMove.endPos;
            unitMove.unit.boardPosition.unit = null;
            unitMove.unit.boardPosition = unitMove.endPos;
            unitMove.endPos.unit = unitMove.unit;
        }

        public List<Tile> getUnitMoves(Tile activeTile)
        {
            List<Tile> possibleMoves = new List<Tile>();

            for (int x = -activeTile.unit.moveDistance; x <= activeTile.unit.moveDistance; x++)
            {

                for(int y = -activeTile.unit.moveDistance; y <= activeTile.unit.moveDistance; y++)
                {
                    int boardPositionX = GameObject.convertPositionToBoardPosition(activeTile.position.X + x*Tile.TILE_SIZE);
                    int boardPositionY = GameObject.convertPositionToBoardPosition(activeTile.position.Y + y*Tile.TILE_SIZE);

                    if (boardPositionX>= 0 && boardPositionX< BOARD_WIDTH
                        && boardPositionY >= 0 && boardPositionY < BOARD_HEIGHT
                        && !(x == 0 && y == 0))
                    {
                        if (Board[boardPositionX, boardPositionY].unit == null)
                            possibleMoves.Add(Board[boardPositionX, boardPositionY]);
                        else if(Board[boardPositionX, boardPositionY].unit.player != activeTile.unit.player)
                            possibleMoves.Add(Board[boardPositionX, boardPositionY]);
                    }
                }
            }
            return possibleMoves;
        }

        public void reset()
        {
            _currentScene.clear();
        }

        public void switchScene(Scene scene)
        {
            _currentScene = scene;
        }
    }
}
