using JMiles42.Extensions;
using JMiles42.Generics;
using UnityEngine;

public class Player: Singleton<Player>
{
	public void OnEnable() { GameplayInputManager.Instance.OnBlockPressed += InstanceOnOnBlockPressed; }

	public void OnDisable() { GameplayInputManager.Instance.OnBlockPressed -= InstanceOnOnBlockPressed; }

	private void InstanceOnOnBlockPressed(GridBlock gridBlock, bool leftClick)
	{
		if (gridBlock.IsNotNull() && gridBlock.TileType == TileType.Floor)
		{
			Transform.position = ((Vector3) gridBlock.GridPosition).FromX_Y2Z().SetY(0.5f);
		}
	}
}