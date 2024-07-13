using UnityEngine;

public interface IWeapon
{
    void Attack();

    static IWeapon CreateDefault()
    {
        return new Sword();
    }
}

public class Sword : IWeapon
{
    public void Attack()
    {
        Debug.Log("Sword Attack");
    }
}

public class Bow : IWeapon
{
    public void Attack()
    {
        Debug.Log("Bow Attack");
    }
}

public abstract class WeaponFactory : ScriptableObject
{
    protected IWeapon weapon;

    public abstract IWeapon ProvideWeapon();
}
