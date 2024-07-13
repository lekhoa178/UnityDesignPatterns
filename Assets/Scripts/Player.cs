using Sirenix.OdinInspector;
using System.Windows.Input;
using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField, Required] private EquipmentFactory equipmentFactory;
    [SerializeField] SpellStrategy[] spells;

    void CastSpell(int index)
    {
        spells[index].CastSpell(transform);
    }

    public void Attack() => equipmentFactory?.ProvideWeapon().Attack();
    public void Defend() => equipmentFactory?.ProvideShield().Defend();

}
