using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace JMiles42.Systems.Components.Linker
{
	public static class GameobjectLinkerExt
	{
		/// <summary>
		/// This is will use the normal GetComponent but will check for a Linker first
		/// </summary>
		/// <typeparam name="T">Type to find</typeparam>
		/// <param name="GO">Object to look on</param>
		/// <returns>The first found Component, or null if none exist</returns>
		public static T GetComponentAdvanced<T>(this GameObject GO) where T: class
		{
			if (GO.GetComponent<ComponentLinker<T>>())
			{
				return GO.GetComponent<ComponentLinker<T>>().Link;
			}
			return GO.GetComponent<T>();
		}

		/// <summary>
		/// This is will use the normal GetComponent but will check for a Linker first
		/// </summary>
		/// <typeparam name="T">Type to find</typeparam>
		/// <param name="GO">Object to look on</param>
		/// <returns>The first found Component, or null if none exist</returns>
		public static T GetComponentInChildrenAdvanced<T>(this GameObject GO) where T: class
		{
			if (GO.GetComponentInChildren<ComponentLinker<T>>())
			{
				return GO.GetComponentInChildren<ComponentLinker<T>>().Link;
			}
			return GO.GetComponentInChildren<T>();
		}

		/// <summary>
		/// This is will use the normal GetComponent but will check for a Linker first
		/// </summary>
		/// <typeparam name="T">Type to find</typeparam>
		/// <param name="GO">Object to look on</param>
		/// <returns>The first found Component, or null if none exist</returns>
		public static T GetComponentInParentAdvanced<T>(this GameObject GO) where T: class
		{
			if (GO.GetComponentInParent<ComponentLinker<T>>())
			{
				return GO.GetComponentInParent<ComponentLinker<T>>().Link;
			}
			return GO.GetComponentInParent<T>();
		}

		/// <summary>
		/// This is will use the normal GetComponents but will also check for Linkers
		/// </summary>
		/// <typeparam name="T">Type to find</typeparam>
		/// <param name="GO">Object to look on</param>
		/// <returns>The first found Component, or null if none exist</returns>
		public static T[] GetComponentsAdvanced<T>(this GameObject GO) where T: class
		{
			var list = new List<T>(GO.GetComponents<T>());
			list.AddRange(GO.GetComponents<ComponentLinker<T>>().Select(t => t.Link));
			return list.ToArray();
		}

		/// <summary>
		/// This is will use the normal GetComponents but will also check for Linkers
		/// </summary>
		/// <typeparam name="T">Type to find</typeparam>
		/// <param name="GO">Object to look on</param>
		/// <returns>The first found Component, or null if none exist</returns>
		public static T[] GetComponentsInChildrenAdvanced<T>(this GameObject GO) where T: class
		{
			var list = new List<T>(GO.GetComponentsInChildren<T>());
			list.AddRange(GO.GetComponentsInChildren<ComponentLinker<T>>().Select(t => t.Link));
			return list.ToArray();
		}

		/// <summary>
		/// This is will use the normal GetComponents but will also check for Linkers
		/// </summary>
		/// <typeparam name="T">Type to find</typeparam>
		/// <param name="GO">Object to look on</param>
		/// <returns>The first found Component, or null if none exist</returns>
		public static T[] GetComponentsInParentAdvanced<T>(this GameObject GO) where T: class
		{
			var list = new List<T>(GO.GetComponentsInParent<T>());
			list.AddRange(GO.GetComponentsInParent<ComponentLinker<T>>().Select(t => t.Link));
			return list.ToArray();
		}
	}
}