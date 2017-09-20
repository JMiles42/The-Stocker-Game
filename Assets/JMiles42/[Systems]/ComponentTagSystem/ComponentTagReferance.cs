using Generated;
using JMiles42.Components;

namespace JMiles42.Systems.ComponentTags
{
	public class ComponentTagReferance: JMilesBehavior
	{
		public ComponentTag TagReferance;

		public bool ContainsTag(ComponentTagsEnum tagToCheck) { return TagReferance.ContainsTag(tagToCheck); }

		public static implicit operator ComponentTag(ComponentTagReferance input) { return input.TagReferance; }
	}
}