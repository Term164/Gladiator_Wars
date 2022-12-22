

using Microsoft.Xna.Framework;

namespace Gladiator_Wars
{
    internal class BigShield : Shield
    {
        public BigShield(Quality quality) {
            offsetRight = new Vector2(-5, 5);
            offsetLeft = new Vector2(11, 5);
            weight = 30;
            armourPoints = 6;
            setShieldQuality(quality);
        }
    }
}
