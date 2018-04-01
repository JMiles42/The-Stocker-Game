using System;
using ForestOfChaosLib.AdvVar.RuntimeRef;
using ForestOfChaosLib.Grid;
using UnityEngine;

[CreateAssetMenu(fileName = "Grid List Reference", menuName = "ADV Variables/Custom/Grid List", order = 2)]
[Serializable]
[StockerFolder]
public class GridBlockListReference: RunTimeList<GridBlock>
{
	public int WallCount;
	public int FloorCount;

	public Action OnMapFinishSpawning;

	public GridBlock GetBlock(GridPosition pos)
	{
		return Items?.Find(a => a.GridPosition == pos);
	}
}