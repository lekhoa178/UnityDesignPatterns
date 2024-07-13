using UnityEngine;

[CreateAssetMenu(fileName = "BowFactory", menuName = "Weapon Factory/Bow")]
public class BowFactory : WeaponFactory
{
    public override IWeapon ProvideWeapon()
    {
        if (weapon == null)
            weapon = new Bow();

        return weapon;
    }
}