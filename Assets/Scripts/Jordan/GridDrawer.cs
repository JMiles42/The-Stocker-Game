using JMiles42.Components;
using UnityEngine;

public class GridDrawer: JMilesBehavior
{
	public int X = 20;
	public int Y = 20;

	public void Start()
	{
		if (GridMaster.InstanceCheckNull)
			return;

		for (int x = 0; x < X; x++)
		{
			for (int y = 0; y < Y; y++)
			{
				var o = GameObject.CreatePrimitive(PrimitiveType.Cube);
				o.name = string.Format("x:{0} y:{1}", x, y);
				o.transform.position = new Vector3((x * GridMaster.Instance.GridSize) + GridMaster.Instance.StartingPoint,
												   0,
												   (y * GridMaster.Instance.GridSize) + GridMaster.Instance.StartingPoint);
			}
		}
	}
}