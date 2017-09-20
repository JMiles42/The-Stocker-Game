using JMiles42.Components;
using UnityEngine;

namespace JMiles42.Systems.Components.Linker
{
	public abstract class ComponentLinker<T>: JMilesBehavior
	{
		public abstract T Link{ get; protected set; }
	}

	public abstract class EditorComponentLinker<T>: ComponentLinker<T>
	{
		[SerializeField] protected T link;

		public override T Link
		{
			get { return link; }
			protected set { link = value; }
		}
	}

	public abstract class ChildComponentLinker<T>: ComponentLinker<T>
	{
		[SerializeField] protected T link;

		public override T Link
		{
			get { return link != null? link : (link = GetComponentInChildren<T>()); }
			protected set { link = value; }
		}
	}

	public abstract class PerantComponentLinker<T>: ComponentLinker<T>
	{
		[SerializeField] protected T link;

		public override T Link
		{
			get { return link != null? link : (link = GetComponentInParent<T>()); }
			protected set { link = value; }
		}
	}
}