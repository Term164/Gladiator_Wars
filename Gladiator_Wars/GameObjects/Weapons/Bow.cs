using Microsoft.Xna.Framework;

namespace Gladiator_Wars
{
    internal class Bow : Weapon
    {
        public Bow(Quality quality) {
            offsetRight = new Vector2(12, 6);
            offsetLeft = new Vector2(20, 32);
            type = WeaponType.Ranged;
            range = 4;
            weight = 5;
            setWeaponQuality(quality);
        }
    }
}
