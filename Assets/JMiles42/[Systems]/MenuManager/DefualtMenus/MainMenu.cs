namespace JMiles42.Systems.MenuManager
{
	public class MainMenu: SimpleMenu<MainMenu>
	{
		public void OnPlayPressed() { GameStartMenu.Show(); }

		public void OnOptionsPressed() { OptionsMenu.Show(); }

		public override void OnBackPressed() { ExitGameMenu.Show(); }
	}
}