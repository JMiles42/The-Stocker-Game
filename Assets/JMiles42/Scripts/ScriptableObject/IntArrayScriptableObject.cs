using UnityEngine;

namespace JMiles42.ScriptableObjects
{
	[CreateAssetMenu(fileName = "Int Array", menuName = ATTRIBUTE_PATH + "Int Array", order = 0)]
	public class IntArrayScriptableObject: ArrayScriptableObject<int>
	{}
}