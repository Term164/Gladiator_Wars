﻿using Gladiator_Wars.Components;
using Gladiator_Wars.Infastructure;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace Gladiator_Wars
{
    enum Difficulty
    {
        easy,
        medium,
        hard,
        boss,
    }

    internal class GladiatorFactory
    {

        public Random random;
        WeaponFactory weaponFactory;
        ArmourFactory armourFactory;
        ShieldFactory shieldFactory;
        private Level level;   

        public GladiatorFactory(int seed, Level level)
        {
            random = new Random(seed);
            weaponFactory = new WeaponFactory(seed * 2147483647);
            armourFactory = new ArmourFactory(seed * 6700417);
            shieldFactory = new ShieldFactory(seed * 74142433);
            this.level = level;
        }

        public Gladiator generateNewGladiator(Tile boardPosition, Player player, Difficulty difficulty, bool ranged)
        {
            Gladiator gladiator = new Gladiator(boardPosition, player);

            Quality equipmentQuality;

            // =========================== WEAPON GENERATION ==============================
            switch (difficulty)
            {
                case Difficulty.easy: equipmentQuality = Quality.common; break;
                case Difficulty.medium: equipmentQuality = Quality.rare; break;
                case Difficulty.hard: equipmentQuality = Quality.legendary; break;
                case Difficulty.boss: equipmentQuality = Quality.legendary; break;
                default: throw new Exception("This quality difficulty does not exist");
            }

            gladiator.weapon = weaponFactory.GenerateWeapon(equipmentQuality, ranged);
            gladiator.armour = armourFactory.GenerateArmour(equipmentQuality, ranged);

            if (!ranged)
            {
                if(random.Next(100) > 70)
                {
                    gladiator.shield = shieldFactory.GenerateShield(equipmentQuality);
                }
            }

            // ========================== GLADIATOR STAT GENERATION =====================

            int min, range;
            switch (difficulty) {
                case Difficulty.easy: min = 0; range = 20; break;
                case Difficulty.medium: min = 10; range = 30; break;
                case Difficulty.hard: min = 20; range = 30; break;
                case Difficulty.boss: min = 50; range = 20; break;
                default: throw new Exception("This difficulty level does not exist.");
            }

            gladiator.strength = random.Next(min, min + range);
            gladiator.toughness = random.Next(min,min + range);
            gladiator.athletics = random.Next(min,min + range);
            gladiator.dexterity = random.Next(min,min + range);
            gladiator.perception = random.Next(min,min + range);

            // ======================== CALCULATING FINAL VALUES =========================

            gladiator.calculateHealthPoints();
            gladiator.calculateTotalWeight();
            gladiator.calcualteTotalArmourPoints();
            gladiator.CalculateMoveDistance();
            gladiator.assignUnitType();

            if (difficulty == Difficulty.boss) gladiator.isBoss = true;
            return gladiator;
        }

        public List<Gladiator> generateGladiatorList(List<Difficulty> difficultyList,Player player, int numOfArchers) {
            List<Gladiator> gladiators = new List<Gladiator>();
            for (int i = 0; i < difficultyList.Count; i++)
            {
                int x, y;
                do
                {
                    if(player is AIPlayer)
                    {
                        x = random.Next(9, 15);
                        y = random.Next(1, 8);
                    }
                    else
                    {
                        x = random.Next(1, 9);
                        y = random.Next(1, 8);
                    }
                } while (level.Board[x, y].unit != null);

                gladiators.Add(generateNewGladiator(level.Board[x,y], player, difficultyList[i], numOfArchers > 0));
                if(numOfArchers > 0) numOfArchers--;
           }
            return gladiators;
        }


    }
}
