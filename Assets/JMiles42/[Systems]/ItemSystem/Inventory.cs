using System.Collections.Generic;
using JMiles42.Attributes;
using UnityEngine;

namespace JMiles42.Systems.Item
{
    [CreateAssetMenu(fileName = "New Inventory", menuName = "JMiles42/Item System/Inventory", order = 0)]
    public class Inventory: ScriptableObject
    {
#pragma warning disable 649
        [SerializeField,NoFoldout(false)] private List<ItemStack> items;
#pragma warning restore 649
        public List<ItemStack> Items
        {
            get { return items; }
        }

        public bool InventoryContains(Item item)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].Item == item)
                    return true;
            }
            return false;
        }

        public bool InventoryContains(ItemStack item)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].Item == item.Item && items[i].Amount >= item.Amount)
                    return true;
            }
            return false;
        }
    }
}