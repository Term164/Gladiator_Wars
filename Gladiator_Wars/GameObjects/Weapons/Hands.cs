using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gladiator_Wars
{
    internal class Hands : Weapon
    {
        public Hands() {
            weight = 0;
            range = 1;
            durability = int.MaxValue;
            damage = 5;
        }
    }
}
