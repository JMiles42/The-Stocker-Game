using ForestOfChaosLib;
using ForestOfChaosLib.AdvVar;
using ForestOfChaosLib.AdvVar.RuntimeRef;
using ForestOfChaosLib.Grid;
using UnityEngine;

[AdvFolderName("Stocker")]
public class StandardPrefabPlacer: FoCsScriptableObject, IPlacer
{
	public GameObject Prefab;
	public TransformRTRef PlaceableParent;
	private GameObject spawnedObject;

	public void StartPlacing(GridPosition pos, Vector3 worldPos)
	{
		if(spawnedObject == null)
			spawnedObject = Instantiate(Prefab, worldPos, Quaternion.identity);
		else
		{
			spawnedObject.SetActive(true);
			spawnedObject.transform.position = worldPos;
		}
	}

	public void UpdatePosition(GridPosition pos, Vector3 worldPos)
	{
		if(spawnedObject == null)
			return;
		spawnedObject.transform.position = worldPos;
	}

	public void CancelPlacement()
	{
		spawnedObject?.SetActive(false);
	}

	public void ApplyPlacement(GridPosition pos, Vector3 worldPos)
	{ }
}
