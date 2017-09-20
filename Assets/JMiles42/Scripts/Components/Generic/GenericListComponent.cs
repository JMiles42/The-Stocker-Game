using JMiles42.Attributes;
using JMiles42.Components;
using JMiles42.Maths;

namespace JMiles42.ScriptableObjects
{
	public abstract class GenericArrayComponent<T>: JMilesBehavior
	{
		[NoFoldout(true)] public T[] Data;

		public T First
		{
			get { return Data.Length == 0? default (T) : Data[0]; }
			set
			{
				if (Data.Length == 0)
					Data = new[] {value};
				else
					Data[0] = value;
			}
		}

		public T GetRandomEntry() { return Data[RandomMaster.Random.Next(0, Data.Length)]; }
		public static implicit operator T[](GenericArrayComponent<T> col) { return col.Data; }
	}

	//public abstract class GenericArrayNoFoldoutComponent<T>: JMilesBehavior {
	//    [NoFoldout(true)] public T[] Data;
	//
	//    public T First {
	//        get { return Data.Length == 0? default (T) : Data[0]; }
	//        set {
	//            if (Data.Length == 0)
	//                Data = new[] {value};
	//            else
	//                Data[0] = value;
	//        }
	//    }
	//
	//    public T GetRandomEntry() { return Data[RandomMaster.Random.Next(0, Data.Length)]; }
	//    public static implicit operator T[](GenericArrayNoFoldoutComponent<T> col) { return col.Data; }
	//}
}