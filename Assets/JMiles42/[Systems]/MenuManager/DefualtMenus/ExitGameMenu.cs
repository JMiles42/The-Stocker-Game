using UnityEngine;

namespace JMiles42.Systems.MenuManager
{
	public class ExitGameMenu: SimpleMenu<ExitGameMenu>
	{
		public void Quit() { Application.Quit(); }
	}
}