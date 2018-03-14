using ForestOfChaosLib.FoCsUI.Button;

namespace ForestOfChaosLib.MenuManaging
{
	public class OpenMenuFromPrefabRefButton: FoCsBehavior
	{
		public ButtonComponentBase Button;
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
			Button = GetComponentInChildren<ButtonComponentBase>();
		}
	}
}