﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gladiator_Wars
{
    internal class Axe : Weapon
    {
        public Axe(WeaponQuality quality) {
            type = WeaponType.Melee;
            range = 1;
            weight = 30;
            setWeaponQuality(quality);
        }
    }
}