namespace JMiles42.Systems.MenuManager
{
	public class AdvancedMenu<T, D>: Menu<T> where T: AdvancedMenu<T, D>
	{
		public static void Show(D data)
		{
			Instance.DealWithData(data);
			Open();
		}

		protected virtual void DealWithData(D data) {}

		public static void Hide(D data) { Close(); }
	}
}