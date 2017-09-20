using Generated;
using JMiles42.Components;
using UnityEngine;

namespace JMiles42.Systems.ComponentTags
{
	public class ComponentTag: JMilesBehavior
	{
		[SerializeField] private ComponentTagsEnum[] tags = new ComponentTagsEnum[0];

		public ComponentTagsEnum[] Tags
		{
			get { return tags; }
		}

		public override ComponentTagsEnum[] GetTags() { return Tags; }

		public bool ContainsTag(ComponentTagsEnum tagToCheck)
		{
			foreach (var t in tags)
			{
				if (t == tagToCheck)
					return true;
			}
			return false;
		}

		//public void OnEnable() {
		//    AddToList();
		//}
		//
		//public void OnDisable() {
		//    RemoveFromList();
		//}
		//
		//private void AddToList() {
		//    foreach (var t in Tags)
		//    {
		//        List<ComponentTag> list;
		//        GlobalTagDictionary.TryGetValue(t, out list);
		//        if (list == null)
		//        {
		//            list = new List<ComponentTag>(10) {this};
		//            GlobalTagDictionary[t] = list;
		//        }
		//        else
		//            list.Add(this);
		//    }
		//}
		//
		//private void RemoveFromList() {
		//    foreach (var t in Tags)
		//    {
		//        List<ComponentTag> list;
		//        GlobalTagDictionary.TryGetValue(t, out list);
		//        if (list == null)
		//        {
		//            list = new List<ComponentTag>(10);
		//            list.Remove(this);
		//            GlobalTagDictionary[t] = list;
		//        }
		//        else
		//            list.Remove(this);
		//    }
		//}
		//
		//public static Dictionary<ComponentTagsEnum, List<ComponentTag>> GlobalTagDictionary =
		//        new Dictionary<ComponentTagsEnum, List<ComponentTag>>((int) ComponentTagsEnum.COUNT);
		//
		//public static List<ComponentTag> GetComponentTagListFromTag(ComponentTagsEnum tag) {
		//    List<ComponentTag> list;
		//    GlobalTagDictionary.TryGetValue(tag, out list);
		//    return list;
		//}
		//
		//public static ComponentTag GetComponentTagFromTag(ComponentTagsEnum tag) {
		//    List<ComponentTag> list;
		//    GlobalTagDictionary.TryGetValue(tag, out list);
		//    if ((list != null) && (list.Count >= 1))
		//        return list[0];
		//    return null;
		//}
	}
}