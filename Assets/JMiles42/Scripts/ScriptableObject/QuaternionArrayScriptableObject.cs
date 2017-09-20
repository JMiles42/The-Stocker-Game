using UnityEngine;

namespace JMiles42.ScriptableObjects
{
	[CreateAssetMenu(fileName = "Quaternion Array", menuName = ATTRIBUTE_PATH + "Quaternion Array", order = 0)]
	public class QuaternionArrayScriptableObject: ArrayScriptableObject<Quaternion>
	{}
}