using UnityEngine;
using UnityEngine.InputSystem;
using Code.Inventory;

public class GiveItem : MonoBehaviour
{
    public InventoryManager InventoryManager;
    public ItemDefinition ItemToGive;
    public int Amount = 1;


    public void Give()
    {
        if (InventoryManager == null || ItemToGive == null) return;
        bool ok = InventoryManager.TryAddItem(ItemToGive, Amount);
        Debug.Log(ok ? $"Añadido {ItemToGive.name}" : "Inventario lleno");
    }
}