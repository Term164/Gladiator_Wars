﻿using Gladiator_Wars.Components;
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

        public static readonly int BOARD_HEIGHT = 9, BOARD_WIDTH = 16;
        // Queue of characters to determine play order
        public Queue<Gladiator> unitsPlayOrder;
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
            unitsPlayOrder = new Queue<Gladiator>();
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
                    
                    if(x == 0 || x == BOARD_WIDTH-1 || y == 0 || y == BOARD_HEIGHT-1) 
                        Board[x, y] = new Wall(new Vector2(x * Tile.TILE_SIZE, y * Tile.TILE_SIZE), Board[x, y]);
                    else
                        Board[x, y] = new Tile(new Vector2(x * Tile.TILE_SIZE, y * Tile.TILE_SIZE), Board[x,y]);
                    _currentScene.addItem(Board[x, y]);
                }
            }
        }

        public void loadPlayerUnits(Player player)
        {
            player.CreateNewGladiator(2, 1);
            createGladiatorQueue();
        }

        private void createGladiatorQueue()
        {
            List<Gladiator> allBoardUnits = new List<Gladiator>();
            allBoardUnits.AddRange(player1.units);
            allBoardUnits.AddRange(player2.units);
            allBoardUnits.Sort();
            foreach (Gladiator gladiator in allBoardUnits)
            {
                unitsPlayOrder.Enqueue(gladiator);
            }
            
            selectNextUnit();
        }

        private void selectNextUnit()
        {
            Gladiator nextUnit = unitsPlayOrder.Dequeue();
            while (!nextUnit.alive)
            {
                nextUnit = unitsPlayOrder.Dequeue();
            }

            nextUnit.player.setActiveTile(nextUnit.boardPosition);
            nextUnit.player.hasTurn = true;
            unitsPlayOrder.Enqueue(nextUnit);

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

            Gladiator gladiator = move.unit;

            if (!gladiator.hasActionPoints())
            {
                gladiator.attackPoint = true;
                gladiator.movePoint= true;
                gladiator.player.hasTurn = false;
                selectNextUnit();
            }
            else
            {
                move.unit.player.setActiveTile(move.unit.boardPosition);
            }

        }

        public void addNextMove(Move move) {
            nextMoveStack.Push(move);
        }


        private void attackUnit(Move unitMove) {
            unitMove.unit.attackPoint = false;
            unitMove.unit.attack(unitMove.endPos.unit);
        }

        private void moveUnit(Move unitMove) {
            if (unitMove.unit.movePoint) unitMove.unit.movePoint = false;
            else unitMove.unit.attackPoint = false;

            unitMove.unit.nextNode = unitMove.endPos;
            unitMove.unit.boardPosition.unit = null;
            unitMove.unit.boardPosition = unitMove.endPos;
            unitMove.endPos.unit = unitMove.unit;
        }

        public List<Move> getUnitMoves(Tile activeTile)
        {

            List<Move> possibleMoves = new List<Move>();
            possibleMoves.AddRange(getAllUnitMovementMoves(activeTile));
            if (activeTile.unit.attackPoint) possibleMoves.AddRange(getAllUnitAttackMoves(activeTile));
            return possibleMoves;
        }


        private List<Move> getAllUnitMovementMoves(Tile activeTile)
        {
            List<Move> possibleMoves = new List<Move>();

            for (int x = -activeTile.unit.moveDistance; x <= activeTile.unit.moveDistance; x++)
            {

                for (int y = -activeTile.unit.moveDistance; y <= activeTile.unit.moveDistance; y++)
                {
                    int boardPositionX = GameObject.convertPositionToBoardPosition(activeTile.position.X + x * Tile.TILE_SIZE);
                    int boardPositionY = GameObject.convertPositionToBoardPosition(activeTile.position.Y + y * Tile.TILE_SIZE);

                    if (boardPositionX >= 1 && boardPositionX < BOARD_WIDTH - 1
                        && boardPositionY >= 1 && boardPositionY < BOARD_HEIGHT - 1
                        && !(x == 0 && y == 0))
                    {
                        if (Board[boardPositionX, boardPositionY].unit == null)
                        {
                            possibleMoves.Add(new Move(activeTile.unit, Action.Move, Board[boardPositionX, boardPositionY]));
                        }
                            
                    }
                }
            }
            return possibleMoves;
        }

        private List<Move> getAllUnitAttackMoves(Tile activeTile)
        {
            List<Move> possibleMoves = new List<Move>();

            for (int x = -activeTile.unit.weapon.range; x <= activeTile.unit.weapon.range; x++)
            {

                for (int y = -activeTile.unit.weapon.range; y <= activeTile.unit.weapon.range; y++)
                {
                    int boardPositionX = GameObject.convertPositionToBoardPosition(activeTile.position.X + x * Tile.TILE_SIZE);
                    int boardPositionY = GameObject.convertPositionToBoardPosition(activeTile.position.Y + y * Tile.TILE_SIZE);

                    if (boardPositionX >= 1 && boardPositionX < BOARD_WIDTH - 1
                        && boardPositionY >= 1 && boardPositionY < BOARD_HEIGHT - 1
                        && !(x == 0 && y == 0))
                    {
                        if (Board[boardPositionX, boardPositionY].unit != null)
                        {
                            if (Board[boardPositionX, boardPositionY].unit.player != activeTile.unit.player)
                            {
                                possibleMoves.Add(new Move(activeTile.unit, Action.Attack, Board[boardPositionX, boardPositionY]));
                            }
                            
                        }

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
