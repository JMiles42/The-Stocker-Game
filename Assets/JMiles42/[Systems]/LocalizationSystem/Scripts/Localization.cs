using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace JMiles42.Systems.Localization
{
	public static partial class Localization
	{
		public const string DEFUALT_PAGE_NAME = "0_Defualt";
		public const string DEFUALT_NO_ENTRY = "no_vaild_entry";
		public const string DEFUALT_LANGUAGE_NAME_ENTRY = "language_name";
		public const string DEFUALT_GAME_NAME_ENTRY = "game_title";

		public const string LANGUAGE_FOLDER = "Localization";
		private static int s_activeLanguageIndex;

		public static int ActiveLanguageIndex
		{
			get { return s_activeLanguageIndex; }
			set
			{
				s_activeLanguageIndex = value;
				WriteActiveLanguage();
			}
		}

		public static List<Language> Languages = new List<Language> {"eng"};

		public static Language ActiveLanguage
		{
			get
			{
				if (Languages == null)
					GetLanguages();

				if (ActiveLanguageIndex >= 0 && ActiveLanguageIndex < Languages.Count)
					return Languages[ActiveLanguageIndex];
				return Languages[(ActiveLanguageIndex = 0)];
			}
		}

		public static string ActiveLanguageFolderPath
		{
			get { return LangFilePath + "\\" + ActiveLanguage; }
		}

		public static string LangFilePath
		{
			get { return Application.streamingAssetsPath + "\\" + LANGUAGE_FOLDER; }
		}

		static Localization()
		{
			GetLanguages();
			LoadActiveLanguage();
		}

		public static void GetLanguages()
		{
			if (Directory.Exists(LangFilePath))
			{
				var langs = Directory.GetDirectories(LangFilePath);
				Languages = new List<Language>(langs.Length);
				foreach (var lang in langs)
					Languages.Add(lang.Remove(0, LangFilePath.Length + 1));
			}
		}

		public static void LoadPages()
		{
			foreach (var language in Languages)
			{
				language.Load();
			}
		}

		public static void UnloadPages()
		{
			foreach (var language in Languages)
			{
				language.Unload();
			}
		}

		public static void WriteActiveLanguage()
		{
			if (Directory.Exists(LangFilePath))
			{
				using (var stream = new StreamWriter(LangFilePath + "\\" + "active.lang"))
				{
					stream.Write("//Don't add anything to this file, it could cause errors\n");
					stream.Write(ActiveLanguageIndex);
				}
			}
		}

		public static void LoadActiveLanguage()
		{
			if (Directory.Exists(LangFilePath))
			{
				if (File.Exists(LangFilePath + "\\" + "active.lang"))
				{
					using (var stream = new StreamReader(LangFilePath + "\\" + "active.lang"))
					{
						stream.ReadLine();
						int.TryParse(stream.ReadLine(), out s_activeLanguageIndex);
						if (ActiveLanguageIndex < 0 || ActiveLanguageIndex >= Languages.Count)
							ActiveLanguageIndex = 0;
					}
				}
			}
		}

		public static string GetEntry(string pageName, string entry)
		{
			var page = ActiveLanguage.FindPage(pageName);
			if (page == null)
			{
				return ReturnNullEntryString(pageName, entry);
			}
			var str = page.GetEntry(entry);
			if (string.IsNullOrEmpty(str))
			{
				return ReturnNullEntryString(pageName, entry);
			}
			return str;
		}

		private static string ReturnNullEntryString(string pageName, string entry)
		{
			var page = ActiveLanguage.FindPage(DEFUALT_PAGE_NAME);
			if (page == null)
			{
				return string.Format("Unable to find: {0} in page: {1}", entry, pageName);
			}
			return string.Format(page.GetEntry(DEFUALT_NO_ENTRY), entry, pageName);
		}
	}
}