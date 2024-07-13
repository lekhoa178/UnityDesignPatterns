using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

[CreateAssetMenu(fileName = "GenericShieldFactory", menuName = "Shield Factory/Shield")]
public class GenericShieldFactory : ShieldFactory
{
    public override IShield ProvideShield()
    {
        if (shield == null)
            shield = new Shield();

        return shield;
    }
}