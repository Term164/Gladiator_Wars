using System;

namespace Gladiator_Wars
{
    internal class WeaponFactory
    {
        Random random;
        public WeaponFactory(int seed) {
            random = new Random(seed);
        }

        public Weapon GenerateWeapon(Quality quality, bool ranged)
        {
            if(ranged) {
                return new Bow(quality);
            }
            else
            {
                int weaponType = random.Next(3);
                switch(weaponType)
                {
                    case 0: return new Sword(quality);
                    case 1: return new Axe(quality);
                    case 2: return new Spear(quality);
                    default: throw new Exception("Wrong type when generating new melee weapon.");
                }
            }
        }
    }
}
