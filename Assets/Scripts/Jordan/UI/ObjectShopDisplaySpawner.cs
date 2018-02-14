using ForestOfChaosLib;
using ForestOfChaosLib.UnityScriptsExtensions;

public class ObjectShopDisplaySpawner: FoCs2DBehavior {
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