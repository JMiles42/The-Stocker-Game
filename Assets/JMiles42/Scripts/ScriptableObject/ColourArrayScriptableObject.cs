using JMiles42.Types;
using UnityEngine;

namespace JMiles42.ScriptableObjects
{
	[CreateAssetMenu(fileName = "Colour Array", menuName = ATTRIBUTE_PATH + "Colour Array", order = 0)]
	public class ColourArrayScriptableObject: ArrayScriptableObject<Colour>
	{}
}