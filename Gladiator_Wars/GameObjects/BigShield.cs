using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gladiator_Wars
{
    internal class BigShield : Shield
    {
        public BigShield(Quality quality) {
            weight = 30;
            armourPoints = 6;
            setShieldQuality(quality);
        }
    }
}
