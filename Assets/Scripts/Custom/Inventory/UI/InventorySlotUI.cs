using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

namespace Code.Inventory.UI
{
    public class InventorySlotUI : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
    {
        [Header("UI")]
        public Image IconImage;
        public TextMeshProUGUI AmountText;

        private InventoryUI inventoryUI;
        private int slotIndex;

        public void Init(InventoryUI ui, int index)
        {
            inventoryUI = ui;
            slotIndex = index;
        }

        public void SetSlot(InventorySlot slot)
        {
            if (slot.IsEmpty)
            {
                IconImage.enabled = false;
                AmountText.text = "";
            }
            else
            {
                IconImage.enabled = true;
                IconImage.sprite = slot.Item.Icon;
                AmountText.text = slot.Amount > 1 ? slot.Amount.ToString() : "";
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            // Right-click use
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                var slot = inventoryUI.InventoryManager.Inventory.Slots[slotIndex];
                if (!slot.IsEmpty && slot.Item.Type == ItemType.Consumable)
                {
                    inventoryUI.InventoryManager.UseSlot(slotIndex);
                }
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            var slot = inventoryUI.InventoryManager.Inventory.Slots[slotIndex];
            if (slot.IsEmpty) return;
            inventoryUI.BeginDrag(slotIndex, slot.Item.Icon, slot.Amount);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!inventoryUI.IsDragging) return;
            inventoryUI.UpdateDrag(eventData.position);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            inventoryUI.EndDrag();
        }

        public void OnDrop(PointerEventData eventData)
        {
            if (!inventoryUI.IsDragging) return;
            inventoryUI.SwapSlots(slotIndex);
        }
    }
}
