

using Microsoft.Xna.Framework;

namespace Gladiator_Wars.GameObjects
{
    internal class SmallShield : Shield
    {
        public SmallShield(Quality quality) {
            offsetRight = new Vector2(-5,5);
            offsetLeft = new Vector2(13,5);
            weight = 10;
            armourPoints = 2;
            setShieldQuality(quality);
        }
    }
}
