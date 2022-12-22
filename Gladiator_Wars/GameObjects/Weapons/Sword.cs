using Microsoft.Xna.Framework;

namespace Gladiator_Wars
{
    internal class Sword : Weapon
    {
        public Sword(Quality quality) {
            offsetRight = new Vector2(15, -2);
            offsetLeft = new Vector2(-8, -1);
            type = WeaponType.Melee;
            range = 1;
            weight = 20;
            setWeaponQuality(quality);
        }
    }
}
