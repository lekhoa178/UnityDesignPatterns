using UnityEngine;

[CreateAssetMenu(fileName = "EquipmentFactory", menuName = "Equipment Factory")]
public class EquipmentFactory : ScriptableObject
{
    public WeaponFactory WeaponFactory;
    public ShieldFactory ShieldFactory;

    public IWeapon ProvideWeapon()
    {
        return WeaponFactory != null ? WeaponFactory.ProvideWeapon() : IWeapon.CreateDefault();
    }

    public IShield ProvideShield()
    {
        return ShieldFactory != null ? ShieldFactory.ProvideShield() : IShield.CreateDefault();
    }
}