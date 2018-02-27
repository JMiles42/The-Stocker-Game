using System;
using ForestOfChaosLib.Grid;
using UnityEngine;

public interface IPlacer
{
	Action OnApplyPlacement { get; set; }
	void StartPlacing(Player player, GridPosition pos, Vector3 worldPos);
	void UpdatePosition(Player player, GridPosition pos, Vector3 worldPos, bool IsWalkingToPlace);
	void CancelPlacement();
	void ApplyPlacement(Player player, GridBlock block, Vector3 worldPos);
	void ForcePlaceAt(GridBlock pos);
}