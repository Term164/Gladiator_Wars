using System;

namespace Gladiator_Wars.Infastructure
{
    internal class ArmourFactory
    {
        Random random;

        public ArmourFactory(int seed)
        {
           random = new Random(seed);
        }

        public Armour GenerateArmour(Quality quality, bool ranged)
        {
            if (ranged) {
                return new LightArmour(quality);
            }
            int armourType = random.Next(3);
            switch(armourType)
            {
                case 0: return new LightArmour(quality);
                case 2: return new MediumArmour(quality);
                case 3: return new HeavyArmour(quality);
                default: throw new Exception("Wrong type when generating new armour.");
            }
        }
    }
}
