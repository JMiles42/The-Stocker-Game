using UnityEngine;

namespace JMiles42.Attributes
{
	public class ResetableAttribute: PropertyAttribute
	{
		public object defaultData;

		public ResetableAttribute(object _defaultData) { defaultData = _defaultData; }
	}
}