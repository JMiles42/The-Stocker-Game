using System.Collections.Generic;
using Generated;
using UnityEngine;

namespace JMiles42.Systems.ComponentTags
{
	public static class ComponentTagGameobjectExt
	{
		public static bool DoesHaveComponentTag(this GameObject GO)
		{
			if (GO.GetComponent<ComponentTag>())
				return true;
			if (GO.GetComponent<ComponentTagReferance>())
				return true;
			return false;
		}

		public static ComponentTag GetComponentTag(this GameObject GO)
		{
			if (GO.GetComponent<ComponentTag>())
				return GO.GetComponent<ComponentTag>();
			if (GO.GetComponent<ComponentTagReferance>())
				return GO.GetComponent<ComponentTagReferance>().TagReferance;
			return null;
		}

		private static ComponentTag[] GetFinalTagsFromComponents(ComponentTagsEnum tagToFind, ComponentTag[] foundComponents)
		{
			var tags = new List<ComponentTag>();
			foreach (var component in foundComponents)
			{
				if (component.ContainsTag(tagToFind))
					tags.Add(component);
			}
			return tags.ToArray();
		}

		public static ComponentTag[] GetComponentTagsInChildren(this GameObject GO, ComponentTagsEnum tagToFind)
		{
			var c = GO.GetComponentsInChildren<ComponentTag>();
			return GetFinalTagsFromComponents(tagToFind, c);
		}

		public static ComponentTag[] GetComponentTagsInParent(this GameObject GO, ComponentTagsEnum tagToFind)
		{
			var c = GO.GetComponentsInParent<ComponentTag>();
			return GetFinalTagsFromComponents(tagToFind, c);
		}
	}
}