using JMiles42.ScriptableObjects.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Placeable Item", menuName = "SO/Placeable Item List", order = 0)]
public class PlaceableItemList: ArrayScriptableObject<PlaceableItem> {}