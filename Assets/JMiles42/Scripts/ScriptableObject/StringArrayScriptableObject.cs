using UnityEngine;

namespace JMiles42.ScriptableObjects
{
	[CreateAssetMenu(fileName = "String Array", menuName = ATTRIBUTE_PATH + "String Array", order = 0)]
	public class StringArrayScriptableObject: ArrayScriptableObject<string>
	{}
}