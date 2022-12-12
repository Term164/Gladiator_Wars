using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gladiator_Wars
{
    public enum WeaponType
    {
        Melee,
        Ranged,
        Hybrid
    }

    public enum WeaponQuality
    {
        common,
        rare,
        legendary
    }

    internal class Weapon : GameObject
    {
        private static Random random = new Random();
        public int damage;
        public int durability;
        public int weight;
        public int range;
        public WeaponType type;

        public void setWeaponQuality(WeaponQuality quality)
        {
            switch(quality)
            {
                case WeaponQuality.common:
                    damage = random.Next(5,10);
                    durability = random.Next(30,50);
                    break;
                case WeaponQuality.rare:
                    damage = random.Next(10,20);
                    durability= random.Next(50,70);
                    break;
                case WeaponQuality.legendary:
                    damage = random.Next(20,30);
                    durability= random.Next(70,100);
                    break;
                default:
                    throw new Exception("This type of Weapon Quality does not exist.");
            }
        }
    }
}
