using JMiles42.ScriptableObjects;
using UnityEngine;

namespace JMiles42.Systems.Item
{
	[CreateAssetMenu(fileName = "New Item", menuName = "JMiles42/Item System/Item", order = 0)]
	public class Item: JMilesScriptableObject
	{
#if ITEM_NAME_LOCALIZED
        public LocalizedString Name;
#else
		public string Name;
#endif
		public int Cost;
		public string Description;
		public Texture Icon;
#if ITEM_WEIGHT_ENABLED
        public int Weight;
#endif
	}
}