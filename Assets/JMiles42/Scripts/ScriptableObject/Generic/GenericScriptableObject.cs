using JMiles42.Attributes;

namespace JMiles42.ScriptableObjects
{
	public class GenericScriptableObject<T>: JMilesScriptableObject
	{
		[NoFoldout] public T Data;

		public static implicit operator T(GenericScriptableObject<T> col) { return col.Data; }
	}
}