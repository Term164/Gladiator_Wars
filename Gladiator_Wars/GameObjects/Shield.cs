using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Gladiator_Wars
{
    internal class Shield
    {
        private static Random random = new Random();
        public int weight;
        public int durability;
        public int armourPoints;
        public Quality quality;

        public void setShieldQuality(Quality quality)
        {
            switch (quality)
            {
                case Quality.common:
                    durability = random.Next(30, 50);
                    this.quality = Quality.common;
                    break;
                case Quality.rare:
                    durability = random.Next(50, 70);
                    this.quality = Quality.rare;
                    break;
                case Quality.legendary:
                    durability = random.Next(70, 100);
                    this.quality = Quality.legendary;
                    break;
                default:
                    throw new Exception("This type of Weapon Quality does not exist.");
            }
        }

    }


}
