using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gladiator_Wars
{
    internal class Bow : Weapon
    {
        public Bow(Quality quality) {
            type = WeaponType.Ranged;
            range = 4;
            weight = 5;
            setWeaponQuality(quality);
        }
    }
}
