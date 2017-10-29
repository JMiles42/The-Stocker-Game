namespace JMiles42.Systems.MenuManager
{
	[System.Serializable]
	public partial class MenuManager
	{
		public ExitGameMenu ExitGameMenu;
		public InventoryMenu InventoryMenu;
		public PlayGameMenu PlayGameMenu;
		public SpawnMenu SpawnMenu;

	public enum MenuTypes {
		None,
		ExitGameMenu,
		InventoryMenu,
		PlayGameMenu,
		SpawnMenu,

	}	}
}
