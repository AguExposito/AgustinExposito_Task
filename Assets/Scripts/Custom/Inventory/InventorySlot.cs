namespace Code.Inventory
{
    [System.Serializable]
    public struct InventorySlot
    {
        public ItemDefinition Item;
        public int Amount;

        public bool IsEmpty => Item == null;

        public InventorySlot(ItemDefinition item, int amount = 1)
        {
            Item = item;
            Amount = amount;
        }
    }
}
