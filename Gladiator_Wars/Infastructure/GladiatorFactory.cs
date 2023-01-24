using Gladiator_Wars.Components;
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

        public GladiatorFactory(int seed)
        {
            random = new Random(seed);
            weaponFactory = new WeaponFactory(seed*2147483647);
            armourFactory = new ArmourFactory(seed*6700417);
            shieldFactory = new ShieldFactory(seed * 74142433);
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

            gladiator.strength = random.Next(min,range);
            gladiator.toughness = random.Next(min,range);
            gladiator.athletics = random.Next(min,range);
            gladiator.dexterity = random.Next(min,range);
            gladiator.perception = random.Next(min,range);

            // ======================== CALCULATING FINAL VALUES =========================

            gladiator.calculateHealthPoints();
            gladiator.calculateTotalWeight();
            gladiator.calcualteTotalArmourPoints();

            return gladiator;
        }

        public List<Gladiator> generateGladiatorList(List<Difficulty> difficultyList) {
            return null;
        }


    }
}
