namespace JMiles42.Systems.Localization
{
	[System.Serializable]
	public class LocalizedString
	{
		public string Page;
		public string Entry;

		public string GetData() { return GetData(Page, Entry); }

		public static string GetData(string page, string entry) { return Localization.GetEntry(page, entry); }

		public static implicit operator string(LocalizedString input) { return input.GetData(); }
	}
}