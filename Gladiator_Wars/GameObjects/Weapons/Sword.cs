using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gladiator_Wars
{
    internal class Sword : Weapon
    {
        public Sword(Quality quality) {
            type = WeaponType.Melee;
            range = 1;
            weight = 20;
            setWeaponQuality(quality);
        }
    }
}
