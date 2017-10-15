using System.Linq;
using JMiles42.Extensions;
using JMiles42.Generics;
using UnityEngine;

public class CameraRangeLimiter: Singleton<CameraRangeLimiter>
{
	public Vector3 minPos;
	public Vector3 maxPos;
	public float minOverhang = 15;
	public float maxOverhang = 5;

	public static Vector3 MinPos
	{
		get { return Instance.minPos; }
		set { Instance.minPos = value; }
	}

	public static Vector3 MaxPos
	{
		get { return Instance.maxPos; }
		set { Instance.maxPos = value; }
	}

	public static float MinOverhang
	{
		get { return Instance.minOverhang; }
		set { Instance.minOverhang = value; }
	}

	public static float MaxOverhang
	{
		get { return Instance.maxOverhang; }
		set { Instance.maxOverhang = value; }
	}

	public static void RecalculateRange()
	{
		var f = GridBlock.Blocks.First();
		var l = GridBlock.Blocks.Last();

		var pos = Vector3.one.SetY(0);

		//MinPos = f.Value.Position - (Vector3.one.SetY(0) * MinOverhang);
		//MaxPos = l.Value.Position + (Vector3.one.SetY(0) * MaxOverhang);

		MinPos = f.Value.Position - pos.SetX(pos.x * -MinOverhang).SetZ(pos.z * MinOverhang);
		MaxPos = l.Value.Position + pos.SetX(pos.x * MaxOverhang);//.SetZ(pos.z * -MaxOverhang);
	}
}