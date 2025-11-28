using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Code.Inventory.UI
{
    public class InventoryUI : MonoBehaviour
    {
        [Header("References")]
        public InventoryManager InventoryManager;
        public GameObject SlotPrefab;
        public Transform SlotsParent; // should have GridLayoutGroup

        private Inventory Inventory => InventoryManager.Inventory;
        private InventorySlotUI[] slotUIs;

        private void Awake()
        {
            BuildSlots();
            Inventory.OnInventoryChanged += Refresh;
            Refresh();
        }

        private void OnDestroy()
        {
            if (Inventory != null)
                Inventory.OnInventoryChanged -= Refresh;
        }

        private void BuildSlots()
        {
            int size = Inventory.Slots.Count;
            slotUIs = new InventorySlotUI[size];
            for (int i = 0; i < size; i++)
            {
                var go = Instantiate(SlotPrefab, SlotsParent);
                var slotUI = go.GetComponent<InventorySlotUI>();
                slotUI.Init(this, i);
                slotUIs[i] = slotUI;
            }
        }

        private void Refresh()
        {
            for (int i = 0; i < slotUIs.Length; i++)
            {
                slotUIs[i].SetSlot(Inventory.Slots[i]);
            }
        }

        // Drag state shared
        public bool IsDragging { get; private set; }
        public int DragOriginIndex { get; private set; }
        public ItemDefinition DragItemDef { get; private set; }
        public int DragAmount { get; private set; }
        private Image dragImage;

        public void BeginDrag(int originIndex, Sprite icon, int amount)
        {
            var slot = Inventory.Slots[originIndex];
            if (slot.IsEmpty) return;

            IsDragging = true;
            DragOriginIndex = originIndex;
            DragItemDef = slot.Item;
            DragAmount = slot.Amount;

            // create ghost image under root canvas
            var canvas = GetComponentInParent<UnityEngine.Canvas>();
            dragImage = new GameObject("DragImage").AddComponent<Image>();
            dragImage.transform.SetParent(canvas.transform, false);
            dragImage.raycastTarget = false;
            dragImage.sprite = icon;
            // set fixed size to avoid overly large icon
            var rt = dragImage.rectTransform;
            rt.sizeDelta = new Vector2(64, 64);
            dragImage.preserveAspect = true;
        }

        public void UpdateDrag(Vector2 screenPos)
        {
            if (dragImage != null)
            {
                dragImage.transform.position = screenPos;
            }
        }

        public void EndDrag()
        {
            IsDragging = false;
            Destroy(dragImage?.gameObject);
            dragImage = null;
        }

        public void SwapSlots(int targetIndex)
        {
            Inventory.Swap(DragOriginIndex, targetIndex);
        }
    }
}
