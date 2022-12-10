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

        public static readonly int BOARD_HEIGHT = 7, BOARD_WIDTH = 14;
        public Scene _currentScene;
        public Tile[,] Board;

        private Stack<Move> nextMoveStack;
        private List<Move> gameMoves;

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

            Gladiator gladiator1 = new Gladiator(new Vector2(2 * Tile.TILE_SIZE,0), Board[2, 0]);
            Board[2, 0].unit = gladiator1;
            _currentScene.addItem(gladiator1);


            Gladiator gladiator2 = new Gladiator(new Vector2(0, 0), Board[0, 0]);
            Board[0, 0].unit = gladiator2;
            _currentScene.addItem(gladiator2);
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
                default: break;
            }
        }

        public void addNextMove(Move move) {
            nextMoveStack.Push(move);
        }

        public void moveUnit(Move unitMove) {
            unitMove.unit.nextNode = unitMove.endPos;
            unitMove.unit.boardPosition.unit = null;
            //Board[(int)unitMove.unit.boardPosition.X, (int)unitMove.unit.boardPosition.Y].unit = null;
            unitMove.unit.boardPosition = unitMove.endPos;
            //Board[(int)unitMove.endPos.X, (int)unitMove.endPos.Y].unit = unitMove.unit;
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

                    if (boardPositionX + x >= 0 && boardPositionX + x < BOARD_WIDTH
                        && boardPositionY + y >= 0 && boardPositionY + y < BOARD_HEIGHT
                        && !(x == 0 && y == 0))
                    {
                        if (Board[boardPositionX, boardPositionY].unit == null)
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
