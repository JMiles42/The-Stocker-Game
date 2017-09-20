using UnityEngine;

namespace JMiles42.ScriptableObjects
{
	[CreateAssetMenu(fileName = "Transform Array", menuName = ATTRIBUTE_PATH + "Transform Array", order = 0)]
	public class TransformArrayScriptableObject: ArrayScriptableObject<Transform>
	{}
}