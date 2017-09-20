using Generated;
using JMiles42.Systems.Components.Linker;
using JMiles42.Systems.ComponentTags;
using UnityEngine;

namespace JMiles42.HiddenClasses
{
	public class JMBe: MonoBehaviour
	{
		#region GetComponentAdvanced
		/// <summary>
		/// Its GetComponent, but also checks for attached Linkers
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public T GetComponentAdvanced<T>() where T: class
		{
			return gameObject.GetComponentAdvanced<T>();
		}

		/// <summary>
		/// Its GetComponentInChildren, but also checks for attached Linkers
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public T GetComponentInChildrenAdvanced<T>() where T: class
		{
			return gameObject.GetComponentInChildrenAdvanced<T>();
		}

		/// <summary>
		/// Its GetComponentInParent, but also checks for attached Linkers
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public T GetComponentInParentAdvanced<T>() where T: class
		{
			return gameObject.GetComponentInParentAdvanced<T>();
		}

		/// <summary>
		/// Its GetComponents, but also checks for attached Linkers
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public T[] GetComponentsAdvanced<T>() where T: class
		{
			return gameObject.GetComponentsAdvanced<T>();
		}

		/// <summary>
		/// Its GetComponentsInChildren, but also checks for attached Linkers
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public T[] GetComponentsInChildrenAdvanced<T>() where T: class
		{
			return gameObject.GetComponentsInChildrenAdvanced<T>();
		}

		/// <summary>
		/// Its GetComponentsInParent, but also checks for attached Linkers
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public T[] GetComponentsInParentAdvanced<T>() where T: class
		{
			return gameObject.GetComponentsInParentAdvanced<T>();
		}
		#endregion

		#region ComponentTag
		public bool HasTags() { return GetComponentAdvanced<ComponentTag>() != null; }

		public bool ContainTags(ComponentTagsEnum tag)
		{
			if (!HasTags())
				return false;
			return GetComponentAdvanced<ComponentTag>().ContainsTag(tag);
		}

		public virtual ComponentTagsEnum[] GetTags()
		{
			if (!HasTags())
				return null;
			return GetComponentAdvanced<ComponentTag>().Tags;
		}
		#endregion
	}
}