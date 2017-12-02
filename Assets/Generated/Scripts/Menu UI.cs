namespace JMiles42.Systems.MenuManager
{
	[System.Serializable]
	public partial class MenuManager
	{
		public ExitGameMenu ExitGameMenu;
		public GameTypeMenu GameTypeMenu;
		public InventoryMenu InventoryMenu;
		public MainMenu MainMenu;
		public OptionsMenu OptionsMenu;
		public PlayGameMenu PlayGameMenu;
		public ShopMenu ShopMenu;
		public SpawnMenu SpawnMenu;

	public enum MenuTypes {
		None,
		ExitGameMenu,
		GameTypeMenu,
		InventoryMenu,
		MainMenu,
		OptionsMenu,
		PlayGameMenu,
		ShopMenu,
		SpawnMenu,

	}	}
}
