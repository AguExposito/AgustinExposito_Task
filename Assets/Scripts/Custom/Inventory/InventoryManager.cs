using Code.Player;
using UnityEngine;

namespace Code.Inventory
{
    public class InventoryManager : MonoBehaviour
    {
        public int InventorySize = 24;
        public Inventory Inventory { get; private set; }

        private void Awake()
        {
            Inventory = new Inventory(InventorySize);
        }

        public bool TryAddItem(ItemDefinition item, int amount = 1)
        {
            return Inventory.Add(item, amount);
        }

        public void UseSlot(int index)
        {
            var slot = Inventory.Slots[index];
            if (slot.IsEmpty) return;
            switch (slot.Item.Type)
            {
                case ItemType.Consumable:
                    // Example: heal 20 HP
                    var playerStats = GetComponent<PlayerStats>();
                    if (playerStats != null)
                    {
                        playerStats.Heal(slot.Item.HealAmount);
                    }
                    break;
                case ItemType.QuestItem:
                    // Equip logic placeholder
                    Debug.Log($"Equip {slot.Item.name}");
                    break;
            }
            Inventory.Use(index);
        }
    }
}
