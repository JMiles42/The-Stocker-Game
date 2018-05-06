namespace ForestOfChaosLib.MenuManaging
{
	[System.Serializable]
	public partial class MenuManager
	{
		public CreditsMenu CreditsMenu;
		public ExitGameMenu ExitGameMenu;
		public GameTypeMenu GameTypeMenu;
		public MainMenu MainMenu;
		public OptionsMenu OptionsMenu;
		public PlayGameMenu PlayGameMenu;
		public SpawnMenu SpawnMenu;
		public TutorialPlayGameMenu TutorialPlayGameMenu;

	public enum MenuTypes {
		None,
		CreditsMenu,
		ExitGameMenu,
		GameTypeMenu,
		MainMenu,
		OptionsMenu,
		PlayGameMenu,
		SpawnMenu,
		TutorialPlayGameMenu,

	}	}
}
