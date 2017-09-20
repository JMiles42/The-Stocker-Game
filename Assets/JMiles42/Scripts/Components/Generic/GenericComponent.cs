using JMiles42.Attributes;
using JMiles42.Components;

namespace JMiles42.ScriptableObjects
{
	public class GenericComponent<T>: JMilesBehavior
	{
		[NoFoldout] public T Data;

		public static implicit operator T(GenericComponent<T> col) { return col.Data; }
	}
}