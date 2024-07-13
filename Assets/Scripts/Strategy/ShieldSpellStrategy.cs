using UnityEngine;
using UnityUtils;

[CreateAssetMenu(fileName = "ShieldSpellStrategy", menuName = "Spells/ShieldSpellStrategy")]
public class ShieldSpellStrategy : SpellStrategy
{
    public GameObject shieldPrefab;
    public float duration = 10f;

    public override void CastSpell(Transform origin)
    {
        GameObject shield = Instantiate(shieldPrefab, origin.position.With(y: -1.5f), Quaternion.identity, origin);
        Destroy(shield, duration);
    }
}