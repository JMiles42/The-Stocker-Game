using System;
using UnityEngine;

namespace JMiles42.Attributes
{
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Struct, Inherited = true)]
	public class ConditionalHideAttribute: PropertyAttribute
	{
		//The name of the bool field that will be in control
		public string ConditionalSourceField;

		//TRUE = Hide in inspector / FALSE = Disable in inspector
		public bool HideInInspector;

		public ConditionalHideAttribute(string conditionalSourceField)
		{
			ConditionalSourceField = conditionalSourceField;
			HideInInspector = false;
		}

		public ConditionalHideAttribute(string conditionalSourceField, bool hideInInspector)
		{
			ConditionalSourceField = conditionalSourceField;
			HideInInspector = hideInInspector;
		}
	}
}