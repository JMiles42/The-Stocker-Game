using System.Collections.Generic;
using JMiles42;
using JMiles42.Components;
using UnityEngine;

public class GridDrawer: JMilesBehavior {
	public GridMaster Master;
	public int X = 20;
	public int Y = 20;

	void Start() {
		GridBlock.Blocks = new Dictionary<Vector2I, GridBlock>(X * Y);
		for (int x = 0; x < X; x++) {
			for (int y = 0; y < Y; y++) {
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
	}
}