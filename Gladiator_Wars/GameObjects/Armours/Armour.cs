using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Gladiator_Wars
{
    internal class Armour
    {
        public static Random random = new Random();
        public int armourPoints;
        public int durability;
        public int weight;
        public Quality quality;
        public virtual void setArmourQuality(Quality quality)
        {
            switch (quality)
            {
                case Quality.common:
                    durability = random.Next(30, 50);
                    break;
                case Quality.rare:
                    durability = random.Next(50, 70);
                    break;
                case Quality.legendary:
                    durability = random.Next(70, 100);
                    break;
                default:
                    throw new Exception("This type of Armour Quality does not exist.");
            }
        }
    }

    
}
