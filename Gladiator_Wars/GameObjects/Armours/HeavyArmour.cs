using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gladiator_Wars
{
    internal class HeavyArmour : Armour
    {
        public HeavyArmour(Quality quality) {
            weight = 80;
            setArmourQuality(quality);
            this.quality = quality;
        }

        public override void setArmourQuality(Quality quality)
        {
            switch (quality)
            {
                case Quality.common:
                    armourPoints = random.Next(5, 8);
                    break;
                case Quality.rare:
                    armourPoints = random.Next(9, 15);
                    break;
                case Quality.legendary:
                    armourPoints = random.Next(16, 22);
                    break;
                default:
                    throw new Exception("This type of Armour Quality does not exist.");
            }
        }
    }
}
