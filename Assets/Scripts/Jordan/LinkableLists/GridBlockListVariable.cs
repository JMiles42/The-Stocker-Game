using System;
using JMiles42.AdvVar;
using JMiles42.AdvVar.Base;
using UnityEngine;

[CreateAssetMenu(fileName = "Grid List Variable", menuName = "ADV Variables/Custom/Grid List", order = 2)]
[Serializable]
[AdvFolderName("The Stocker Lists", 15)]
public class GridBlockListVariable: AdvListVariable<GridBlock>
{
	public Action OnMapFinishSpawning;
}