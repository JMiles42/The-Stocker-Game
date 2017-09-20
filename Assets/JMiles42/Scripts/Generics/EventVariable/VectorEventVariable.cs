using System;
using UnityEngine;

namespace JMiles42.Events
{
	[Serializable]
	public class Vector2EventVariable: GenericEventVariable<Vector2>
	{}

	[Serializable]
	public class Vector3EventVariable: GenericEventVariable<Vector3>
	{}

	[Serializable]
	public class Vector4EventVariable: GenericEventVariable<Vector4>
	{}

	[Serializable]
	public class QuaternionEventVariable: GenericEventVariable<Quaternion>
	{}
}