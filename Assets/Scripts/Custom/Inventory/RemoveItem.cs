using UnityEngine;
using Code.Inventory;

public class RemoveItem : MonoBehaviour
{
    public InventoryManager InventoryManager;
    public ItemDefinition ItemToRemove;
    public int Amount = 1;

    public void Remove()
    {
        if (InventoryManager == null || ItemToRemove == null) return;
        bool ok = InventoryManager.TryRemoveItem(ItemToRemove, Amount);
        Debug.Log(ok ? $"Se removieron {Amount} de {ItemToRemove.name}" : "No había suficientes items para remover");
    }
}
