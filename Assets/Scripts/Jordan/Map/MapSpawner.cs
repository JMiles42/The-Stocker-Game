using System;
using System.Collections.Generic;
using JMiles42;
using JMiles42.Components;
using JMiles42.Extensions;
using JMiles42.UnityInterfaces;
using UnityEngine;

public class MapSpawner: JMilesBehavior, IEventListening
{
	public MapReferance Map;
	public GridBlockListVariable GridBlock;

	void BuildMapArt()
	{
		var map = Map.BuiltMap;

		GridBlock.Value = new List<GridBlock>(map.Length);

		Transform.DestroyChildren();

		for(int x = 0; x < map.Width; x++)
		{
			for(int y = 0; y < map.Height; y++)
			{
				var gO = GameObject.CreatePrimitive(PrimitiveType.Cube);
				const float num = 0.5f;
				gO.transform.position = new Vector3(x, 0 - num, y);
				gO.transform.localScale = Vector3.one * 0.9f;
				gO.transform.parent = transform;
				Destroy(gO.GetComponent<Collider>());
				var com = gO.AddComponent<GridBlock>();
				com.TileType = map[x, y].TileType;
				com.GridPosition = new Vector2I(x, y);
				switch(map[x, y].TileType)
				{
					case TileType.Floor:
						gO.GetComponent<Renderer>().material.color = Color.green;
						break;
					case TileType.Wall:
						gO.GetComponent<Renderer>().material.color = Color.blue;
						gO.transform.localScale = Vector3.one.SetY(1.5f);
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}
				GridBlock.Add(gO.GetComponent<GridBlock>());
			}
		}
		GridBlock.OnMapFinishSpawning.Trigger();
	}

	public void OnEnable()
	{
		Map.OnMapUpdate += BuildMapArt;
	}

	public void OnDisable()
	{
		Map.OnMapUpdate -= BuildMapArt;
	}
}