using UnityEngine;

namespace JMiles42.ScriptableObjects
{
	[CreateAssetMenu(fileName = "Float Array", menuName = ATTRIBUTE_PATH + "Float Array", order = 0)]
	public class FloatArrayScriptableObject: ArrayScriptableObject<float>
	{}
}