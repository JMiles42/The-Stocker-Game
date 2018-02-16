using ForestOfChaosLib.AdvVar;
using ForestOfChaosLib.AdvVar.RuntimeRef;
using ForestOfChaosLib.Grid;
using UnityEngine;

[AdvFolderName("Stocker")]
public class ExitPlacer: Placer
{
	public TransformRTRef PlaceableParent;
	public ExitWorldObject Prefab;
	private ExitWorldObject spawnedObject;
	public ExitRef Reference;

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
		if(spawnedObject == null)
			return;
		spawnedObject.transform.position = worldPos;
	}

	public override void CancelPlacement()
	{
		spawnedObject?.gameObject.SetActive(false);
	}

	public override void ApplyPlacement(Player player, GridPosition pos, Vector3 worldPos)
	{
		spawnedObject.transform.position = pos.WorldPosition;
		Reference.Reference = spawnedObject;

		spawnedObject = null;
	}
}