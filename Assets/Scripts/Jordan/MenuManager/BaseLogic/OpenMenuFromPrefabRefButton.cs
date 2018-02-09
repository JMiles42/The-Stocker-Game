using JMiles42.JUI.Button;

namespace JMiles42.Systems.MenuManaging
{
	public class OpenMenuFromPrefabRefButton: JMilesBehavior
	{
		public ButtonClickEventBase Button;
		public Menu MenuToOpen;

		public void Open()
		{
			MenuToOpen.OpenByRef();
		}

		private void OnEnable()
		{
			if(Button)
				Button.onMouseClick += Open;
		}

		private void OnDisable()
		{
			if(Button)
				Button.onMouseClick -= Open;
		}

		private void Reset()
		{
			Button = GetComponentInChildren<ButtonClickEventBase>();
		}
	}
}