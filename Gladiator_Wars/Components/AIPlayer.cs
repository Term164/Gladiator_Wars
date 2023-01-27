using Gladiator_Wars.Infastructure;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            if(hasTurn) {
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
            int team = ally ? -1 : 1;
            int unitX = GameObject.convertPositionToBoardPosition(gladiator.position.X);
            int unitY = GameObject.convertPositionToBoardPosition(gladiator.position.Y);

            for (int x = -gladiator.weapon.range; x < gladiator.weapon.range; x++)
            {
                for (int y = -gladiator.weapon.range; y < gladiator.weapon.range; y++)
                {
                    if(x != 0 && y != 0 && unitX + x > 0 && unitX + x < Level.BOARD_WIDTH && unitY + y > 0 && unitY + y < Level.BOARD_HEIGHT) {
                        boardEvaliuationMap[unitX+x,unitY+y] = gladiator.weapon.damage * team;
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

            if (move.unitAction == Action.Attack) return move.unit.weapon.damage * 2;

            Tile tile = move.endPos;
            int tileX = GameObject.convertPositionToBoardPosition(tile.position.X);
            int tileY = GameObject.convertPositionToBoardPosition(tile.position.Y);

            int sum = boardEvaliuationMap[tileX, tileY];
            int distanceToClosestEnemy = getDistanceToClosestEnemy(tile);
            int possibleDamageGiven;

            int enemyUnitsInRange = 0;

            foreach(Gladiator gladiator in currentlevel.player1.units)
            {
                if(((int)(tile.position - gladiator.position).Length()) < active.unit.weapon.range)
                {
                    enemyUnitsInRange++;
                }
            }

            possibleDamageGiven = enemyUnitsInRange * active.unit.weapon.damage;

            sum += distanceToClosestEnemy + possibleDamageGiven;

            return sum;
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
            return distance;
        }


    }
}
