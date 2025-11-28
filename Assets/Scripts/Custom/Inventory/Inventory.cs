using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Inventory
{
    public class Inventory
    {
        public event Action OnInventoryChanged;

        private readonly List<InventorySlot> slots_;
        public IReadOnlyList<InventorySlot> Slots => slots_;

        public Inventory(int size = 24)
        {
            slots_ = new List<InventorySlot>(size);
            for (int i = 0; i < size; i++)
            {
                slots_.Add(new InventorySlot());
            }
        }

        public bool Add(ItemDefinition item, int amount = 1)
        {
            // try stack first
            for (int i = 0; i < slots_.Count; i++)
            {
                if (slots_[i].Item == item && slots_[i].Amount < item.MaxStack)
                {
                    var slot = slots_[i];
                    slot.Amount = Mathf.Min(slot.Amount + amount, item.MaxStack);
                    slots_[i] = slot;
                    OnInventoryChanged?.Invoke();
                    return true;
                }
            }
            // find empty slot
            for (int i = 0; i < slots_.Count; i++)
            {
                if (slots_[i].IsEmpty)
                {
                    slots_[i] = new InventorySlot(item, amount);
                    OnInventoryChanged?.Invoke();
                    return true;
                }
            }
            return false; // inventory full
        }

        public bool Remove(ItemDefinition item, int amount = 1)
        {
            int remaining = amount;
            for (int i = 0; i < slots_.Count; i++)
            {
                if (slots_[i].Item == item && !slots_[i].IsEmpty)
                {
                    var slot = slots_[i];
                    int take = Mathf.Min(slot.Amount, remaining);
                    slot.Amount -= take;
                    remaining -= take;
                    if (slot.Amount <= 0)
                    {
                        slot = new InventorySlot();
                    }
                    slots_[i] = slot;
                    if (remaining == 0)
                    {
                        OnInventoryChanged?.Invoke();
                        return true;
                    }
                }
            }
            // not enough items, rollback not implemented (could be improved)
            OnInventoryChanged?.Invoke();
            return remaining == 0;
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= slots_.Count) return;
            slots_[index] = new InventorySlot();
            OnInventoryChanged?.Invoke();
        }

        public void Swap(int indexA, int indexB)
        {
            if (indexA == indexB) return;
            if (indexA < 0 || indexA >= slots_.Count) return;
            if (indexB < 0 || indexB >= slots_.Count) return;
            (slots_[indexA], slots_[indexB]) = (slots_[indexB], slots_[indexA]);
            OnInventoryChanged?.Invoke();
        }

        public void Use(int index)
        {
            if (index < 0 || index >= slots_.Count) return;
            if (slots_[index].IsEmpty) return;
            // simply remove one for now; actual effect handled elsewhere
            var slot = slots_[index];
            slot.Amount--;
            if (slot.Amount <= 0)
                slot = new InventorySlot();
            slots_[index] = slot;
            OnInventoryChanged?.Invoke();
        }

        public void NotifyChanged()
        {
            OnInventoryChanged?.Invoke();
        }

        public void SetSlot(int index, InventorySlot slot)
        {
            if (index < 0 || index >= slots_.Count) return;
            slots_[index] = slot;
        }

        public void ClearAll()
        {
            for (int i = 0; i < slots_.Count; i++)
            {
                slots_[i] = new InventorySlot();
            }
            NotifyChanged();
        }
    }
}
