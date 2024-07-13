using Sirenix.OdinInspector;
using System.Data.SqlTypes;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Mememo/Item")]
public class Item : ScriptableObject
{
    public string itemName;
    [Required] public Sprite sprite;

    // Targeting Strategy
    // Factory for creating a new instance of the item
    // Other Item details, prefabs, etc.
}
