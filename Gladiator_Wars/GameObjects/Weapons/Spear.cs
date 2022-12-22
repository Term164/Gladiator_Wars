using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gladiator_Wars
{
    internal class Spear : Weapon
    {
        public Spear(Quality quality) {
            offsetRight = new Vector2(17,0);
            offsetLeft = new Vector2(9,36);
            rotation = MathF.PI/ 5;
            type = WeaponType.Melee;
            range = 2;
            weight = 25;
            setWeaponQuality(quality);
        }
    }
}
