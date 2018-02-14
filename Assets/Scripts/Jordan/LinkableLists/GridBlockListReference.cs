using System;
using ForestOfChaosLib.AdvVar;
using ForestOfChaosLib.AdvVar.Base;
using ForestOfChaosLib.Grid;
using UnityEngine;

[CreateAssetMenu(fileName = "Grid List Reference", menuName = "ADV Variables/Custom/Grid List", order = 2)]
[Serializable]
[AdvFolderName("Stocker")]
public class GridBlockListReference: AdvListReference<GridBlock>
{
	public Action OnMapFinishSpawning;

	public GridBlock GetBlock(GridPosition pos)
	{
		return Value?.Find(a => a.GridPosition == pos);
	}
}