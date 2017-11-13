using JMiles42.ScriptableObjects;
using UnityEngine;

namespace JMiles42.Systems.Item {
	[CreateAssetMenu(fileName = "New Item List", menuName = "JMiles42/Item System/Item List", order = 0)]
	public class ItemList: ArrayScriptableObject<Item> {}
}