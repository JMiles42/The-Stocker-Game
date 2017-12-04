using System;
using JMiles42.AdvancedVariables;
using JMiles42.AdvancedVariables.Base;
using UnityEngine;

[CreateAssetMenu(fileName = "Grid List Variable", menuName = "ADV Variables/Custom/Grid List", order = 2)]
[Serializable]
[AdvancedFolderName("The Stocker Lists", 15)]
public class GridBlockListVariable: AdvancedListVariable<GridBlock>
{
	public Action OnMapFinishSpawning;
}

//[Serializable]
//public class GridBlockListReference: AdvancedReference<GridBlock, GridBlockListVariable>
//{ }