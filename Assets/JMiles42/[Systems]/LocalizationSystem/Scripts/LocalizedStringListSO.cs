using JMiles42.ScriptableObjects;
using UnityEngine;

namespace JMiles42.Systems.Localization
{
	[CreateAssetMenu(fileName = "new Localized String List", menuName = "Localization/Localized String List", order = 0)]
	public class LocalizedStringListSO: ArrayScriptableObject<LocalizedString>
	{}
}