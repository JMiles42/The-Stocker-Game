using JMiles42.Attributes;

namespace JMiles42.Components
{
	public class GenericComponent<T>: JMilesBehavior
	{
		[NoFoldout] public T Data;

		public static implicit operator T(GenericComponent<T> col) { return col.Data; }
	}
}