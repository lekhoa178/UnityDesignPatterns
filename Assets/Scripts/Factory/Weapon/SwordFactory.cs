using UnityEngine;

[CreateAssetMenu(fileName = "SwordFactory", menuName = "Weapon Factory/Sword")]
public class SwordFactory : WeaponFactory
{
    public override IWeapon ProvideWeapon()
    {
        if (weapon == null)
            weapon = new Bow();

        return weapon;
    }
}
