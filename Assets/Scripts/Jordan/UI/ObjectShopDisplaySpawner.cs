using JMiles42.Components;
using UnityEngine;

public class ObjectShopDisplaySpawner: JMiles2DBehavior {
	public PlaceableUIItem Prefab;
	public PlaceableItemList Items;

	void Start() {
		foreach (var placeableItem in Items.Data) {
			var go = Instantiate(Prefab);
			go.Button.ButtonText = placeableItem.Name;
			go.Transform.SetParent(Transform);
			go.Item = placeableItem;
		}
	}
}