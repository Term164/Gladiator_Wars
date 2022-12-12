using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gladiator_Wars
{
    internal class Spear : Weapon
    {
        public Spear(WeaponQuality quality) {
            type = WeaponType.Melee;
            range = 2;
            weight = 25;
            setWeaponQuality(quality);
        }
    }
}
