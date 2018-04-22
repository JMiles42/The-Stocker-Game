namespace ForestOfChaosLib.MenuManaging
{
	public class CloseMenuFromPrefabRefDisable: FoCsBehavior
	{
		public Menu MenuToClose;

		private void OnDisable()
		{
			MenuToClose.CloseByRef();
		}
	}
}