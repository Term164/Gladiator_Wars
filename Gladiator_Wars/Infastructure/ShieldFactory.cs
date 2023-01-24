using Gladiator_Wars.GameObjects;
using System;

namespace Gladiator_Wars.Infastructure
{
    internal class ShieldFactory
    {
        Random random;

        public ShieldFactory(int seed) {
            random = new Random(seed);
        }

        public Shield GenerateShield (Quality quality)
        {
            int armourType = random.Next(2);
            if(armourType == 0 ) {return new SmallShield(quality);}
            else { return new BigShield(quality);}
        }
    }
}
