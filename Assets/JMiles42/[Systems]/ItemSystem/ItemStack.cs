using JMiles42.Attributes;
using UnityEngine;

namespace JMiles42.Systems.Item
{
    [System.Serializable]
    public class ItemStack
    {
        [Half10Line, SerializeField] private Item item;
        [Half01Line, SerializeField] private int amount;

        public Item Item
        {
            get { return item; }
            set { item = value; }
        }

        public int Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        public ItemStack()
        {
            this.amount = 1;
            this.item = null;
        }

        public ItemStack(Item item)
        {
            this.amount = 1;
            this.item = item;
        }

        public ItemStack(int amount, Item item)
        {
            this.amount = amount;
            this.item = item;
        }

        public static implicit operator Item(ItemStack input) { return input.item; }
        public static implicit operator ItemStack(Item input) { return new ItemStack(input); }
    }
}