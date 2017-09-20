using UnityEngine;

namespace JMiles42.ScriptableObjects
{
	[CreateAssetMenu(fileName = "GameObject Array", menuName = ATTRIBUTE_PATH + "GameObject Array", order = 0)]
	public class GameObjectArrayScriptableObject: ArrayScriptableObject<GameObject>
	{}
}