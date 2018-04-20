namespace ForestOfChaosLib.MenuManaging
{
	[System.Serializable]
	public partial class MenuManager
	{
		public CreditsMenu CreditsMenu;
		public ExitGameMenu ExitGameMenu;
		public GameTypeMenu GameTypeMenu;
		public InventoryMenu InventoryMenu;
		public LoadingMenu LoadingMenu;
		public MainMenu MainMenu;
		public OptionsMenu OptionsMenu;
		public PlayGameMenu PlayGameMenu;
		public ShopMenu ShopMenu;
		public SpawnMenu SpawnMenu;
		public TutorialPlayGameMenu TutorialPlayGameMenu;

	public enum MenuTypes {
		None,
		CreditsMenu,
		ExitGameMenu,
		GameTypeMenu,
		InventoryMenu,
		LoadingMenu,
		MainMenu,
		OptionsMenu,
		PlayGameMenu,
		ShopMenu,
		SpawnMenu,
		TutorialPlayGameMenu,

	}	}
}
