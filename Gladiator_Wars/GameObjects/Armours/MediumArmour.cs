using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gladiator_Wars
{
    internal class MediumArmour : Armour
    {
        public MediumArmour(Quality quality) {
            weight = 50;
            setArmourQuality(quality);
            this.quality = quality;
        }

        public override void setArmourQuality(Quality quality)
        {
            switch (quality)
            {
                case Quality.common:
                    armourPoints = random.Next(2, 5);
                    break;
                case Quality.rare:
                    armourPoints = random.Next(5, 10);
                    break;
                case Quality.legendary:
                    armourPoints = random.Next(10, 15);
                    break;
                default:
                    throw new Exception("This type of Armour Quality does not exist.");
            }
            base.setArmourQuality(quality);
        }
    }
}
