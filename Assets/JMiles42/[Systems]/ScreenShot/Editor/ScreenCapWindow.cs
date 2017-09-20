using JMiles42.Editor;
using UnityEditor;

namespace JMiles42.Systems.Screenshot
{
	public class ScreenCapWindow: TabedWindow<ScreenCapWindow>
	{
		private Tab<ScreenCapWindow>[] _tabs = {
								  new ScreenshotTaker(),
								  new TimelapseTaker(),
							  };

		public override Tab<ScreenCapWindow>[] Tabs
		{
			get { return _tabs; }
		}

		private const string Title = "Screen Capture Window";

		[MenuItem("JMiles42/" + Title)]
		private static void Init()
		{
			GetWindow();
			window.titleContent.text = Title;
		}
	}
}