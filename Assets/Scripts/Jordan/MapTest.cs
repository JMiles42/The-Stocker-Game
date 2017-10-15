using System.Collections.Generic;
using JMiles42;
using JMiles42.Components;
using UnityEngine;

public class MapTest: JMilesBehavior
{
	public Map map;

	void Start()
	{
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
				com.GridPosition = new Vector2I(x, y);
				GridBlock.Blocks.Add(com.GridPosition, com);
			}
		}
		CameraRangeLimiter.RecalculateRange();
	}
}