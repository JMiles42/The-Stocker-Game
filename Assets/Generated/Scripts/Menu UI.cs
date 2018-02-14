namespace ForestOfChaosLib.MenuManaging
{
	[System.Serializable]
	public partial class MenuManager
	{
		public ExitGameMenu ExitGameMenu;
		public GameTypeMenu GameTypeMenu;
		public InventoryMenu InventoryMenu;
		public LoadingMenu LoadingMenu;
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
		LoadingMenu,
		MainMenu,
		OptionsMenu,
		PlayGameMenu,
		ShopMenu,
		SpawnMenu,

	}	}
}
