using Code.Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Inventory
{
    public class InventoryManager : MonoBehaviour
    {
        public int InventorySize = 24;
        public Inventory Inventory { get; private set; }
        private const string SaveKey = "player_inventory";

        private void Awake()
        {
            Inventory = new Inventory(InventorySize);
            LoadInventory();
        }

        private void Update()
        {
            // Press F9 to clear saved inventory
            if (Keyboard.current != null && Keyboard.current.f9Key.wasPressedThisFrame)
            {
                ClearInventorySave();
            }
        }

        public bool TryAddItem(ItemDefinition item, int amount = 1)
        {
            return Inventory.Add(item, amount);
        }

        public bool TryRemoveItem(ItemDefinition item, int amount = 1)
        {
            return Inventory.Remove(item, amount);
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

        public void ClearInventorySave()
        {
            PlayerPrefs.DeleteKey(SaveKey);
            Inventory = new Inventory(InventorySize);
            Inventory.NotifyChanged();
            Debug.Log("Inventario guardado eliminado");
        }

        #region Save / Load
        [System.Serializable]
        private struct SlotSave
        {
            public string id;
            public int amount;
        }

        [System.Serializable]
        private struct InventorySave
        {
            public SlotSave[] slots;
        }

        public void SaveInventory()
        {
            var save = new InventorySave { slots = new SlotSave[Inventory.Slots.Count] };
            for (int i = 0; i < Inventory.Slots.Count; i++)
            {
                var s = Inventory.Slots[i];
                save.slots[i] = new SlotSave { id = s.Item ? s.Item.Id : string.Empty, amount = s.Amount };
            }
            string json = JsonUtility.ToJson(save);
            PlayerPrefs.SetString(SaveKey, json);
            PlayerPrefs.Save();
        }

        public void LoadInventory()
        {
            if (!PlayerPrefs.HasKey(SaveKey)) return;
            string json = PlayerPrefs.GetString(SaveKey);
            var save = JsonUtility.FromJson<InventorySave>(json);
            if (save.slots == null) return;
            for (int i = 0; i < save.slots.Length && i < Inventory.Slots.Count; i++)
            {
                var data = save.slots[i];
                if (!string.IsNullOrEmpty(data.id))
                {
                    var item = ItemDatabase.GetById(data.id);
                    if (item != null)
                    {
                        Inventory.SetSlot(i, new InventorySlot(item, data.amount));
                    }
                }
            }
            Inventory.NotifyChanged();
        }
        private void OnApplicationQuit()
        {
            SaveInventory();
        }
        #endregion
    }
}
