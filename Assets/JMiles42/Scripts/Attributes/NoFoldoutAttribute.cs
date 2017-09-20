using UnityEngine;

namespace JMiles42.Attributes
{
	public class NoFoldoutAttribute: PropertyAttribute
	{
		public bool ShowVariableName{ get; private set; }

		public NoFoldoutAttribute(bool showName = true) { ShowVariableName = showName; }
	}

	[System.Serializable]
	public class NoFoldoutClass
	{}
}