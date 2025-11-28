using UnityEngine;

namespace Code.Inventory
{
    public enum ItemType
    {
        Consumable,
        QuestItem,
        Misc
    }

    [CreateAssetMenu(fileName = "ItemDefinition", menuName = "Inventory/Item Definition")]
    public class ItemDefinition : ScriptableObject
    {
        public string Id = System.Guid.NewGuid().ToString();
        public Sprite Icon;
        public ItemType Type;
        public int MaxStack = 1;

        // Additional data for consumables or others
        public int HealAmount;
    }
}
