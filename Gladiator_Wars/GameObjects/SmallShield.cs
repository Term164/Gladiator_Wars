using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gladiator_Wars.GameObjects
{
    internal class SmallShield : Shield
    {
        public SmallShield(Quality quality) {
            weight = 10;
            armourPoints = 2;
            setShieldQuality(quality);
        }
    }
}
