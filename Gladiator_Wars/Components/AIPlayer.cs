using Gladiator_Wars.Infastructure;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Gladiator_Wars.Components
{
    internal class AIPlayer : Player
    {

        public int[,] boardEvaliuationMap = new int[Level.BOARD_WIDTH, Level.BOARD_HEIGHT];

        public AIPlayer(Game game, Level level) : base(game, level)
        {

        }

        public override void Update(GameTime gameTime)
        {
            if(hasTurn && currentlevel.player1.units.Count > 0 && active != null) {
                EvaluteBoard();
                Move move = getBestMove();
                makeMove(move);
            }
            base.Update(gameTime);
        }

        public override void setActiveTile(Tile selectedTile)
        {
            base.setActiveTile(selectedTile);
            possibleMoves = currentlevel.getUnitMoves(active);
        }

        private void EvaluteBoard()
        {
            foreach(Gladiator gladiator in units)
            {
                EvaluateGladiatorPosition(gladiator, true);
            }

            foreach (Gladiator gladiator in currentlevel.player1.units)
            {
                EvaluateGladiatorPosition(gladiator, false);
            }
        }

        private void EvaluateGladiatorPosition(Gladiator gladiator, bool ally)
        {
            if (gladiator == active.unit) return;
            float team = ally ? -1 : 1;
            if (active.unit.type == Type.Tank) team = -1;
            else if (active.unit.type == Type.Bowman) team *= (team == 1 ? 2 : 1);

            int unitX = GameObject.convertPositionToBoardPosition(gladiator.boardPosition.position.X);
            int unitY = GameObject.convertPositionToBoardPosition(gladiator.boardPosition.position.Y);

            for (int x = -gladiator.weapon.range; x <= gladiator.weapon.range; x++)
            {
                for (int y = -gladiator.weapon.range; y <= gladiator.weapon.range; y++)
                {
                    if((x != 0 || y != 0) && unitX + x > 0 && unitX + x < Level.BOARD_WIDTH && unitY + y > 0 && unitY + y < Level.BOARD_HEIGHT) {
                        boardEvaliuationMap[unitX + x, unitY + y] = (int)(gladiator.calculateGivenDamage() * team);
                    }
                }
            }
        }

        private Move getBestMove()
        {
            PriorityQueue<Move, int> moves = new PriorityQueue<Move, int>();

            foreach (Move move in possibleMoves)
            {
                moves.Enqueue(move, evaluteMove(move));
            }

            return moves.Dequeue();
        }


        private int evaluteMove(Move move)
        {

            if (move.unitAction == Action.Attack) {
                int possibleDamageInflicted = move.endPos.unit.calculateRecievedDamage(move.unit.calculateGivenDamage());
                if (possibleDamageInflicted > move.endPos.unit.healthPoints) return -100;
                else return possibleDamageInflicted * -5;
            } 
            else if (move.unitAction == Action.Block) return calculateBlockEffectivnes(move);
            else if (move.unitAction == Action.Heal) return -10;

            Tile tile = move.endPos;
            int tileX = GameObject.convertPositionToBoardPosition(tile.position.X);
            int tileY = GameObject.convertPositionToBoardPosition(tile.position.Y);

            int sum = boardEvaliuationMap[tileX, tileY];
            int distanceToClosestEnemy = getDistanceToClosestEnemy(tile); //* (move.unit.type == Type.Bowman ? -1 : 1);
            int currentDistanceToClosestEnemy = getDistanceToClosestEnemy(active);

            int possibleDamageGiven;

            int enemyUnitsInRange = 0;


            for (int x = -move.unit.weapon.range; x <= move.unit.weapon.range; x++)
            {
                for (int y = -move.unit.weapon.range; y <= move.unit.weapon.range; y++)
                {
                    if((x != 0 || y != 0) && tileX + x > 0 && tileX + x < Level.BOARD_WIDTH && tileY + y > 0 && tileY + y < Level.BOARD_HEIGHT) {
                        if (currentlevel.Board[tileX + x, tileY + y].unit != null)
                        {
                            if(currentlevel.Board[tileX + x, tileY + y].unit.player != active.unit.player)
                            {
                                enemyUnitsInRange++;
                            }
                        }
                    }
                }
            }

            possibleDamageGiven = enemyUnitsInRange * (int)(-move.unit.calculateGivenDamage() * 0.15f);

            sum += (distanceToClosestEnemy - currentDistanceToClosestEnemy) + possibleDamageGiven;

            return sum;
        }

        private int calculateBlockEffectivnes(Move move)
        {
            Tile tile = move.endPos;
            int sumBlockedDamage = 0;
            foreach(Gladiator gladiator in currentlevel.player1.units)
            {
                int tileX = GameObject.convertPositionToBoardPosition(tile.position.X);
                int tileY = GameObject.convertPositionToBoardPosition(tile.position.Y);

                for (int x = - gladiator.weapon.range; x <= gladiator.weapon.range; x++)
                {
                    for (int y = -gladiator.weapon.range; y <= gladiator.weapon.range; y++)
                    {
                        if((x != 0 || y != 0) && tileX + x > 0 && tileX + x < Level.BOARD_WIDTH && tileY + y > 0 && tileY + y < Level.BOARD_HEIGHT) {
                            if (currentlevel.Board[tileX + x, tileY + y].unit != null)
                            {
                                if (currentlevel.Board[tileX + x, tileY + y].unit == move.unit);
                                {
                                    sumBlockedDamage += (int)(move.unit.ArmourPoints * 0.2f);
                                }
                            }
                        }
                    }
                }
            }
            return -sumBlockedDamage; 
        }

        private int getDistanceToClosestEnemy(Tile tile)
        {
            int distance = int.MaxValue;

            foreach (Gladiator gladiator in currentlevel.player1.units)
            {
                int currentDistance = (int)(tile.position - gladiator.position).Length();
                if (currentDistance < distance)
                {
                    distance = currentDistance;
                }
            }

            if (distance == int.MaxValue) throw new Exception("No enemy selected");
            return distance/32;
        }


    }
}
