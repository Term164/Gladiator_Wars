using Gladiator_Wars.Components;
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

        Random random;
        WeaponFactory weaponFactory;

        public GladiatorFactory(int seed)
        {
            random = new Random(seed);
            weaponFactory = new WeaponFactory(seed*2147483647);
        }

        public Gladiator generateNewGladiator(Tile boardPosition, Player player, Difficulty difficulty, bool ranged)
        {
            Gladiator gladiator = new Gladiator(boardPosition, player);

            Quality equipmentQuality;

            switch (difficulty)
            {
                case Difficulty.easy: equipmentQuality = Quality.common; break;
                case Difficulty.medium: equipmentQuality = Quality.rare; break;
                case Difficulty.hard: equipmentQuality = Quality.legendary; break;
                case Difficulty.boss: equipmentQuality = Quality.legendary; break;
                default: throw new Exception("This quality difficulty does not exist");
            }

            gladiator.weapon = weaponFactory.GenerateWeapon(equipmentQuality, ranged);

            return gladiator;
        }

        public List<Gladiator> generateGladiatorList(List<Difficulty> difficultyList) {
            return null;
        }


    }
}
