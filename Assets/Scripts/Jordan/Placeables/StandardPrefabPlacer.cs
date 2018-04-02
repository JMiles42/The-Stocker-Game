using ForestOfChaosLib.AdvVar.RuntimeRef;
using ForestOfChaosLib.Grid;
using UnityEngine;

[StockerFolder]
public class StandardPrefabPlacer: Placer
{
	public GridBlockListReference GridBlockList;
	public TransformRTRef PlaceableParent;
	public WorldObject Prefab;
	private WorldObject spawnedObject;
	private Vector3 OldDirection = Vector3.zero;

	public override void StartPlacing(Player player, GridPosition pos, Vector3 worldPos)
	{
		if(spawnedObject == null)
			spawnedObject = Instantiate(Prefab, worldPos, Quaternion.identity);
		spawnedObject.gameObject.SetActive(true);
		spawnedObject.transform.position = worldPos;
	}

	public override void UpdatePosition(Player player, GridPosition pos, Vector3 worldPos, bool IsWalkingToPlace)
	{
		if(spawnedObject == null)
			return;

		if(!IsWalkingToPlace)
			OldDirection = (player.Position - worldPos).normalized;
		spawnedObject.transform.position = player.Position - OldDirection;
	}

	public override void CancelPlacement()
	{
		spawnedObject?.gameObject.SetActive(false);
	}

	public override void ApplyPlacement(Player player, GridBlock block, Vector3 worldPos)
	{
		spawnedObject.transform.position = block.GridPosition.WorldPosition;
		spawnedObject.SetupObject(block);

		spawnedObject.transform.SetParent(PlaceableParent.Reference);
		WorldObjectList.Add(spawnedObject);

		spawnedObject = null;
	}

	public override void ForcePlaceAt(GridBlock pos)
	{
		if(spawnedObject == null)
			spawnedObject = Instantiate(Prefab, pos.Position, Quaternion.identity);
		ApplyPlacement(null, pos, pos.Position);
	}
}