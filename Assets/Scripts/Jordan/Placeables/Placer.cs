using System;
using ForestOfChaosLib;
using ForestOfChaosLib.AdvVar;
using ForestOfChaosLib.Grid;
using UnityEngine;

public abstract class Placer: FoCsScriptableObject, IPlacer
{
	public StringVariable Name = "WorldObject";
	public StringVariable Description = "This is a thing you can place in the world";

	public abstract void StartPlacing(Player player, GridPosition pos, Vector3 worldPos);
	public abstract void UpdatePosition(Player player, GridPosition pos, Vector3 worldPos, bool IsWalkingToPlace);
	public abstract void CancelPlacement();
	public abstract void ApplyPlacement(Player player, GridPosition pos, Vector3 worldPos);

	public Action OnApplyPlacement { get; set; }
}