using JMiles42.Components;
using JMiles42.Extensions;
using UnityEngine;

public class ObjectShopDisplaySpawner: JMiles2DBehavior {
	public PlaceableUIItem Prefab;
	public PlaceableItemList Items;

	void Start() {
		foreach (var placeableItem in Items.Data) {
			var go = Instantiate(Prefab);
			go.Button.ButtonText = placeableItem.Name;
			go.Transform.SetParent(Transform);
			go.Transform.ResetLocalPosRotScale();
			go.Item = placeableItem;
		}
	}
}