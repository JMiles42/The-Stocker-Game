using ForestOfChaosLib.AdvVar;
using ForestOfChaosLib.AdvVar.RuntimeRef;
using ForestOfChaosLib.Grid;
using UnityEngine;

[StockerFolder]
public class ExitPlacer: Placer
{
	public TransformRTRef PlaceableParent;
	public ExitWO Prefab;
	private ExitWO spawnedObject;
	public ExitRef Reference;
	public BoolReference HasReference;
	private Vector3 OldDirection = Vector3.zero;

	public override void StartPlacing(Player player, GridPosition pos, Vector3 worldPos)
	{
		if(Reference.Reference != null)
			return;

		if(spawnedObject == null)
			spawnedObject = Instantiate(Prefab, worldPos, Quaternion.identity);
		spawnedObject.gameObject.SetActive(true);
		spawnedObject.transform.position = worldPos;
	}

	public override void UpdatePosition(Player player, GridPosition pos, Vector3 worldPos, bool IsWalkingToPlace)
	{
		if(Reference.Reference != null)
			return;

		if(spawnedObject == null)
			return;

		if(!IsWalkingToPlace)
			OldDirection = (player.Position - worldPos).normalized;
		spawnedObject.transform.position = player.Position - OldDirection;
	}

	public override void CancelPlacement()
	{
		if(Reference.Reference != null)
			return;
		spawnedObject?.gameObject.SetActive(false);
	}

	public override void ApplyPlacement(Player player, GridBlock block, Vector3 worldPos)
	{
		if(Reference.Reference != null)
			return;
		spawnedObject.transform.position = block.GridPosition.WorldPosition;
		Reference.Reference = spawnedObject;

		spawnedObject.SetupObject(block);
		spawnedObject.transform.SetParent(PlaceableParent.Reference);
		WorldObjectList.Add(spawnedObject);

		HasReference.Value = true;
		spawnedObject = null;
	}

	public override void ForcePlaceAt(GridBlock pos)
	{
		if(spawnedObject == null)
			spawnedObject = Instantiate(Prefab, pos.Position, Quaternion.identity);
		ApplyPlacement(null, pos, pos.Position);
	}
}