using UnityEditor;

namespace JMiles42.Editor
{
	public abstract class Tab<T> where T: EditorWindow
	{
		public abstract string TabName{ get; }
		public abstract void DrawTab(Window<T> owner);
	}
}