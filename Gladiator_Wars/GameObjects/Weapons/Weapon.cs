using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gladiator_Wars
{
    public enum Quality
    {
        common,
        rare,
        legendary
    }

    public enum WeaponType
    {
        Melee,
        Ranged,
        Hybrid
    }

    internal class Weapon : GameObject
    {
        private static Random random = new Random();
        public int damage;
        public int durability;
        public int weight;
        public int range;
        public WeaponType type;
        public Quality quality;

        public void setWeaponQuality(Quality quality)
        {
            switch(quality)
            {
                case Quality.common:
                    damage = random.Next(6,12);
                    durability = random.Next(30,50);
                    this.quality = Quality.common;
                    break;
                case Quality.rare:
                    damage = random.Next(11,22);
                    durability= random.Next(50,70);
                    this.quality = Quality.rare;
                    break;
                case Quality.legendary:
                    damage = random.Next(20,30);
                    durability= random.Next(70,100);
                    this.quality = Quality.legendary;
                    break;
                default:
                    throw new Exception("This type of Weapon Quality does not exist.");
            }
        }
    }
}
