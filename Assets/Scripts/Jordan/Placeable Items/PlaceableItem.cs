using JMiles42;
using UnityEngine;

[CreateAssetMenu(fileName = "New Placeable Item", menuName = "SO/Placeable Item", order = 0)]
public class PlaceableItem: JMilesScriptableObject {
	public string Name;
	public GameObject Prefab;
}