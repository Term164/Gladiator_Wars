using Gladiator_Wars.Components;
using Gladiator_Wars.Infastructure;
using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Gladiator_Wars
{
    public enum GameState
    {
        MainMenu,
        HowToPlay,
        Leveling,
        Level,
        LevelLost
    }

    internal class Level : GameComponent
    {

        public static readonly int BOARD_HEIGHT = 9, BOARD_WIDTH = 16;
        public int levelNumber = 0;
        private List<List<Difficulty>> levels;
        private List<Difficulty> playerUnits = new List<Difficulty> {Difficulty.hard, Difficulty.hard, Difficulty.hard, Difficulty.medium, Difficulty.medium, Difficulty.medium };
        //private List<Difficulty> playerUnits = new List<Difficulty> {Difficulty.easy, Difficulty.easy, Difficulty.easy};
 
        public Scene _currentScene;
        public ArrayList GUI;

        public Queue<Gladiator> unitsPlayOrder;
        public Tile[,] Board;

        private Stack<Move> nextMoveStack;
        private List<Move> gameMoves;
        private Move _currentMove;

        public Player player1;
        public Player player2;

        public GameState levelGameState = GameState.MainMenu;

        public GladiatorFactory gladiatorFactory;
        public int seed = 10022;

        public Level(Game game) : base(game)
        {

            gladiatorFactory = new GladiatorFactory(seed, this);
            _currentScene = new Scene();
            gameMoves = new List<Move>();
            nextMoveStack = new Stack<Move>();
            unitsPlayOrder = new Queue<Gladiator>();
            GUI = new ArrayList();

            // ======================== DEFINE LEVELS =======================
            levels = new List<List<Difficulty>>(new List<Difficulty>[5]);
            levels[0] = new List<Difficulty> {Difficulty.easy, Difficulty.easy, Difficulty.easy };
            levels[1] = new List<Difficulty> {Difficulty.easy, Difficulty.easy, Difficulty.medium, Difficulty.medium };
            levels[2] = new List<Difficulty> {Difficulty.easy, Difficulty.easy, Difficulty.medium, Difficulty.medium, Difficulty.hard };
            levels[3] = new List<Difficulty> {Difficulty.medium, Difficulty.hard, Difficulty.hard,Difficulty.medium, Difficulty.medium };
            levels[4] = new List<Difficulty> {Difficulty.hard, Difficulty.hard, Difficulty.hard, Difficulty.boss };
        }

        private void loadLevelUnits(List<Gladiator> gladiators)
        {
            foreach (Gladiator gladiator in gladiators)
            {
                _currentScene.addItem(gladiator);
                gladiator.boardPosition.unit = gladiator;
            }
        }

        // Initialize level with gameObjects
        public void initLevel()
        {
            Board = new Tile[BOARD_WIDTH, BOARD_HEIGHT];
            for (int x = 0; x < BOARD_WIDTH; x++)
            {
                for (int y = 0; y < BOARD_HEIGHT; y++)
                {

                    if (x == 0)
                    {
                        if (y == 0) Board[x, y] = new Wall(new Vector2(x * Tile.TILE_SIZE, y * Tile.TILE_SIZE), Board[x, y],0, true, new Vector2(0, 0));
                        else if (y == BOARD_HEIGHT - 1) Board[x, y] = new Wall(new Vector2(x * Tile.TILE_SIZE, y * Tile.TILE_SIZE), Board[x, y], -MathF.PI/2, true, new Vector2(32, 0));
                        else Board[x, y] = new Wall(new Vector2(x * Tile.TILE_SIZE, y * Tile.TILE_SIZE+32), Board[x, y], -MathF.PI / 2, false, new Vector2(0, 0));
                    }
                    else if (x == BOARD_WIDTH - 1)
                    {
                        if (y == 0) Board[x, y] = new Wall(new Vector2(x * Tile.TILE_SIZE, y * Tile.TILE_SIZE), Board[x, y], MathF.PI/2, true, new Vector2(0,32));
                        else if (y == BOARD_HEIGHT -1 ) Board[x, y] = new Wall(new Vector2(x * Tile.TILE_SIZE, y * Tile.TILE_SIZE), Board[x, y], MathF.PI, true, new Vector2(32, 32));
                        else Board[x, y] = new Wall(new Vector2(x * Tile.TILE_SIZE, y * Tile.TILE_SIZE+32), Board[x, y], MathF.PI / 2, false, new Vector2(32, 32));
                    }
                    else if (y == 0)
                    {
                        Board[x, y] = new Wall(new Vector2(x * Tile.TILE_SIZE, y * Tile.TILE_SIZE), Board[x, y], 0, false, new Vector2(0, 0));
                    }
                    else if (y == BOARD_HEIGHT - 1)
                    {
                        Board[x, y] = new Wall(new Vector2(x * Tile.TILE_SIZE, y * Tile.TILE_SIZE), Board[x, y], MathF.PI, false, new Vector2(32, 32));
                    }
                    else
                        Board[x, y] = new Tile(new Vector2(x * Tile.TILE_SIZE, y * Tile.TILE_SIZE), Board[x,y]);
                    _currentScene.addItem(Board[x, y]);
                }
            }

            loadLevel(0);
        }

        public void loadLevel(int levelNumber)
        {
            if (levelNumber == 0) // If this is the first level create new units
            {
                loadLevelUnits(gladiatorFactory.generateGladiatorList(playerUnits, player1, 1));
                loadLevelUnits(gladiatorFactory.generateGladiatorList(levels[4],player2,1));
            }
            else // Copy surviving units from the previous battle
            {
                Gladiator newGladiator = gladiatorFactory.generateNewGladiator(Board[1, 1], player1, Difficulty.medium, false);
                player1.units.Add(newGladiator);
                _currentScene.addItem(newGladiator);
                loadLevelUnits(gladiatorFactory.generateGladiatorList(levels[levelNumber], player2, 1)); // Load new AI unitsI
                resetPlayerUnits();
            }

            createGladiatorQueue();
        }

        private void resetPlayerUnits()
        {
            Random random = new Random(seed);
            foreach(Gladiator gladiator in player1.units)
            {
                int abbilityPointsAllocation = gladiator.experiencePoints / 10;
                gladiator.experiencePoints = 0;
                while(abbilityPointsAllocation > 0)
                {
                    switch (random.Next(5))
                    {
                        case 0: gladiator.strength++;
                            break;
                        case 1: gladiator.toughness++;
                            break;
                        case 2: gladiator.athletics++;
                            break;
                        case 3: gladiator.dexterity++;
                            break;
                        case 4: gladiator.perception++;
                            break;
                        default: throw new Exception("Wrong");
                    }
                    abbilityPointsAllocation--;
                }

                gladiator.calculateHealthPoints();
                gladiator.calculateTotalWeight();
                gladiator.calcualteTotalArmourPoints();
                gladiator.CalculateMoveDistance();

                // ==================== RESET POSITIONS ===================
                int x;
                int y;
                do
                {
                    x = random.Next(1, 9);
                    y = random.Next(1, 8);
                }
                while (Board[x, y].unit != null);


                gladiator.boardPosition.unit = null;
                gladiator.boardPosition = Board[x,y];
                gladiator.position = gladiator.boardPosition.position;
                Board[x, y].unit = gladiator;
            }
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

            nextUnit.isDefending = false;
            nextUnit.player.setActiveTile(nextUnit.boardPosition);
            nextUnit.player.hasTurn = true;
            unitsPlayOrder.Enqueue(nextUnit);

        }


        public override void Update(GameTime gameTime)
        {
            if(_currentMove != null)
            {
                if(_currentMove.unit.nextNode == null)
                {
                    Gladiator gladiator = _currentMove.unit;

                    if (!gladiator.hasActionPoints())
                    {
                        gladiator.attackPoint = true;
                        gladiator.movePoint= true;
                        gladiator.player.hasTurn = false;
                        selectNextUnit();
                    }
                    else if(gladiator.player.active == null)
                    {
                        gladiator.player.setActiveTile(gladiator.boardPosition);
                    }
                    _currentMove = null;
                }
            }

            if(nextMoveStack.Count > 0)
            {
                Move nextMove = nextMoveStack.Pop();
                _currentMove = nextMove;
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
                case Action.Block:
                    blockUnit(move);
                    break;
                case Action.Heal:
                    healUnit(move);
                    break;
                default: break;
            }
        }

        public void addNextMove(Move move) {
            nextMoveStack.Push(move);
        }


        private void attackUnit(Move unitMove) {
            SoundManager.PlayMelleAttackSound();
            int givenDamage = unitMove.endPos.unit.calculateRecievedDamage(unitMove.unit.calculateGivenDamage()); 
            GUIRenderer.damageText.resetDamage((unitMove.endPos.unit.position + new Vector2(16,0))*Renderer.SCALE, givenDamage.ToString());
            unitMove.unit.experiencePoints += givenDamage;
            unitMove.unit.attackPoint = false;
            unitMove.unit.attack(unitMove.endPos.unit);
        }

        private void moveUnit(Move unitMove) {
            if (unitMove.unit.movePoint) unitMove.unit.movePoint = false;
            else unitMove.unit.attackPoint = false;

            unitMove.unit.experiencePoints += 10;
            unitMove.unit.nextNode = unitMove.endPos;
            unitMove.unit.boardPosition.unit = null;
            unitMove.unit.boardPosition = unitMove.endPos;
            unitMove.endPos.unit = unitMove.unit;
        }

        private void blockUnit(Move unitMove)
        {
            unitMove.unit.experiencePoints += 10;
            unitMove.unit.movePoint = false;
            unitMove.unit.attackPoint = false;
            unitMove.unit.Defend();
        }


        private void healUnit(Move unitMove)
        {
            if (unitMove.unit.movePoint) unitMove.unit.movePoint = false;
            else unitMove.unit.attackPoint = false;
            // Heal text
            
            unitMove.unit.experiencePoints += 10;
            unitMove.unit.Heal(unitMove.endPos.unit);
        }

        public List<Move> getUnitMoves(Tile activeTile)
        {

            List<Move> possibleMoves = new List<Move>();
            possibleMoves.AddRange(getAllUnitMovementMoves(activeTile));
            if (activeTile.unit.attackPoint) possibleMoves.AddRange(getAllUnitAttackMoves(activeTile));
            possibleMoves.Add(new Move(activeTile.unit, Action.Block, activeTile));
            possibleMoves.AddRange(GetAllUnitHealMoves(activeTile));
            return possibleMoves;
        }


        public List<Move> getAllUnitMovementMoves(Tile activeTile)
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

        public List<Move> getAllUnitAttackMoves(Tile activeTile)
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

        public List<Move> GetAllUnitHealMoves(Tile activeTile)
        {
            List<Move> possibleMoves = new List<Move>();
            if (activeTile.unit.hasHeald) return possibleMoves;

            for (int x = -1; x <= 1; x++)
            {

                for (int y = -1; y <= 1; y++)
                {
                    int boardPositionX = GameObject.convertPositionToBoardPosition(activeTile.position.X + x * Tile.TILE_SIZE);
                    int boardPositionY = GameObject.convertPositionToBoardPosition(activeTile.position.Y + y * Tile.TILE_SIZE);

                    if (boardPositionX >= 1 && boardPositionX < BOARD_WIDTH - 1
                        && boardPositionY >= 1 && boardPositionY < BOARD_HEIGHT - 1
                        && !(x == 0 && y == 0))
                    {
                        if (Board[boardPositionX, boardPositionY].unit != null)
                        {
                            if (Board[boardPositionX, boardPositionY].unit.player == activeTile.unit.player)
                            {
                                possibleMoves.Add(new Move(activeTile.unit, Action.Heal, Board[boardPositionX, boardPositionY]));
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
