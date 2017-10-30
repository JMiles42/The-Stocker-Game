using JMiles42.ScriptableObjects;
using UnityEngine;

[CreateAssetMenu(fileName = "New Placeable Item", menuName = "SO/Placeable Item List", order = 0)]
public class PlaceableItemList: ArrayScriptableObject<PlaceableItem> {}