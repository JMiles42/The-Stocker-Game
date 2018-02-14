using ForestOfChaosLib;
using UnityEngine;

[CreateAssetMenu(fileName = "New Placeable Item", menuName = "SO/Placeable Item", order = 0)]
public class PlaceableItem: FoCsScriptableObject {
	public string Name;
	public GameObject Prefab;
}