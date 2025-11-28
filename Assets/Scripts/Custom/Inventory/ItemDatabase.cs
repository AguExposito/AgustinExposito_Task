using System.Collections.Generic;
using UnityEngine;

namespace Code.Inventory
{
    public static class ItemDatabase
    {
        private static Dictionary<string, ItemDefinition> idLookup;

        private static void Init()
        {
            if (idLookup != null) return;
            idLookup = new Dictionary<string, ItemDefinition>();
            var all = Resources.LoadAll<ItemDefinition>(string.Empty);
            foreach (var item in all)
            {
                if (!idLookup.ContainsKey(item.Id))
                {
                    idLookup.Add(item.Id, item);
                }
            }
        }

        public static ItemDefinition GetById(string id)
        {
            Init();
            idLookup.TryGetValue(id, out var item);
            return item;
        }
    }
}
