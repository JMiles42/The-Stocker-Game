using ForestOfChaosLib.Grid;
using UnityEngine;

public interface IPlacer
{
	void StartPlacing(GridPosition pos, Vector3 worldPos);
	void UpdatePosition(GridPosition pos, Vector3 worldPos);
	void CancelPlacement();
	void ApplyPlacement(GridPosition pos, Vector3 worldPos);
}