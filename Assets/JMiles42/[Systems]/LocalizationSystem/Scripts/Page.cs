using System.Collections.Generic;
using System.IO;
using JMiles42.Utilities;
using UnityEngine;
using Language = JMiles42.Systems.Localization.Localization.Language;

namespace JMiles42.Systems.Localization
{
	public class Page
	{
		public Language MyLanguage = null;
		public string PageName;
		public Dictionary<string, string> Data = null;

		public bool Loaded
		{
			get { return Data != null; }
			set
			{
				if (value)
					Load();
				else
					Unload();
			}
		}

		public string PageFilePath
		{
			get { return Application.streamingAssetsPath + '\\' + Localization.LANGUAGE_FOLDER + '\\' + MyLanguage.LanguageName + "\\" + PageName + ".page"; }
		}

		public Page(Language lang, string pageName)
		{
			PageName = pageName;
			MyLanguage = lang;
		}

		public Page(string pageName)
		{
			PageName = pageName;
			MyLanguage = Localization.ActiveLanguage;
		}

		public Page(Page page) { PageName = page.PageName; }

		public void Load()
		{
			if (Data == null)
			{
				if (File.Exists(PageFilePath))
				{
					Data = new Dictionary<string, string>();

					using (var stream = new StreamReader(PageFilePath))
					{
						while (!stream.EndOfStream)
						{
							var str = stream.ReadLine();

							if (string.IsNullOrEmpty(str) || !str.Contains("|"))
							{
								Data.Add(str, "");
							}
							else
							{
								var data = str.Split('|');
								Data.Add(data[0], str.Remove(0, data[0].Length + 1));
							}
						}
					}
				}
			}
		}

		public void Unload()
		{
			if (Data != null)
			{
				Data.Clear();
				Data = null;
			}
		}

		public string GetEntry(string entry)
		{
			bool b = Loaded;

			//var str = "";
			using (new ActionOnDispose(() => Loaded = b))
			{
				Loaded = true;
				if (Data.ContainsKey(entry))
				{
					if (string.IsNullOrEmpty(Data[entry]))
						return " ";
					return Data[entry];
				}
				return "";
			}
		}

		public static implicit operator Page(string input) { return new Page(input); }
	}
}