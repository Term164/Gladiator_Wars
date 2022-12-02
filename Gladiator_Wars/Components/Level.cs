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
                    Board[x, y] = new Tile(new Vector2(x * Tile.TILE_SIZE, y * Tile.TILE_SIZE), new Vector2(x, y));
                    _currentScene.addItem(Board[x, y]);
                }
            }

            Gladiator gladiator = new Gladiator(new Vector2(0, 0), new Vector2(0, 0));
            Board[0, 0].unit = gladiator;
            _currentScene.addItem(gladiator);
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
            unitMove.unit.nextNode = new Vector2(unitMove.endPos.X*Tile.TILE_SIZE, unitMove.endPos.Y*Tile.TILE_SIZE);
            Board[(int)unitMove.unit.boardPosition.X, (int)unitMove.unit.boardPosition.Y].unit = null;
            unitMove.unit.boardPosition = unitMove.endPos;
            Board[(int)unitMove.endPos.X, (int)unitMove.endPos.Y].unit = unitMove.unit;
        }

        public List<Tile> getUnitMoves(Tile activeTile)
        {
            List<Tile> possibleMoves = new List<Tile>();

            for(int x = -activeTile.unit.moveDistance; x <= activeTile.unit.moveDistance; x++)
            {
                for(int y = -activeTile.unit.moveDistance; y <= activeTile.unit.moveDistance; y++)
                {
                    if(activeTile.boardPosition.X + x >= 0 && activeTile.boardPosition.X + x < BOARD_WIDTH
                        && activeTile.boardPosition.Y + y >= 0 && activeTile.boardPosition.Y + y < BOARD_HEIGHT
                        && !(x == 0 && y == 0))
                    {
                        possibleMoves.Add(Board[(int)activeTile.boardPosition.X + x, (int)activeTile.boardPosition.Y + y]);
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
