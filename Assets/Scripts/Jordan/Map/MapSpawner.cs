using System;
using System.Collections.Generic;
using JMiles42;
using JMiles42.Components;
using JMiles42.Extensions;
using UnityEngine;

public class MapSpawner: JMilesBehavior
{
	public MapGeneratorBase Map;

	void Start()
	{
		var map = Map.GenerateMap();
		GridBlock.Blocks = new Dictionary<Vector2I, GridBlock>(map.Width * map.Height);
		for (int x = 0; x < map.Width; x++)
		{
			for (int y = 0; y < map.Height; y++)
			{
				var gO = GameObject.CreatePrimitive(PrimitiveType.Cube);
				const float num = 0.5f;
				gO.transform.position = new Vector3(x, 0 - num, y);
				gO.transform.localScale = Vector3.one * 0.9f;
				gO.transform.parent = transform;
				Destroy(gO.GetComponent<Collider>());
				var com = gO.AddComponent<GridBlock>();
				com.TileType = map[x, y].TyleType;
				com.GridPosition = new Vector2I(x, y);
				switch (map[x, y].TyleType)
				{
					case TileType.Nothing:
						gO.GetComponent<Renderer>().material.color = Color.black;
						gO.transform.localScale = Vector3.one.SetY(0.5f);
						break;
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
				GridBlock.Blocks.Add(com.GridPosition, com);
			}
		}
		CameraRangeLimiter.RecalculateRange();
	}
}