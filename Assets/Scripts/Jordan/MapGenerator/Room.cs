using System;
using ForestOfChaosLib.Grid;
using ForestOfChaosLib.Utilities.Enums;

[Serializable]
public class Room
{
	public Direction_NSEW enteringCorridor;
	public int roomHeight = 0;
	public int roomWidth = 0;
	public GridPosition Position = GridPosition.Zero;
}