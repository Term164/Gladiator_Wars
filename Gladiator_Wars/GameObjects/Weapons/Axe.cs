

using Microsoft.Xna.Framework;

namespace Gladiator_Wars
{
    internal class Axe : Weapon
    {
        public Axe(Quality quality) {
            offsetRight = new Vector2(16,-2);
            offsetLeft = new Vector2(-8, -2);
            type = WeaponType.Melee;
            range = 1;
            weight = 30;
            setWeaponQuality(quality);
        }
    }
}
