using System;
using System.Collections.Generic;
using System.IO;

namespace JMiles42.Systems.Localization
{
	public static partial class Localization
	{
		public class Language: IEquatable<Language>
		{
			public string LanguageName;
			public List<Page> Pages = null;
			public Page DefualtPage{ get; private set; }

			public bool Loaded
			{
				get { return Pages != null; }
				set
				{
					if (value)
						Load();
					else
						Unload();
				}
			}

			public string FilePath
			{
				get { return LangFilePath + "\\" + LanguageName; }
			}

			public Language(string languageName)
			{
				LanguageName = languageName;
				GetDefaultPage();
			}

			public Page FindPage(string name)
			{
				for (var i = 0; i < Pages.Count; i++)
				{
					if (Pages[i].PageName == name)
						return Pages[i];
				}
				return null;
			}

			public Page LoadPage(string name)
			{
				if (Directory.Exists(FilePath))
				{
					if (File.Exists(FilePath + "\\" + name + ".page"))
					{
						var page = new Page(this, name);
						Pages.Add(page);
						return page;
					}
				}
				return null;
			}

			private void GetDefaultPage()
			{
				if (Directory.Exists(FilePath))
				{
					if (File.Exists(FilePath + "\\" + DEFUALT_PAGE_NAME + ".page"))
					{
						DefualtPage = new Page(this, DEFUALT_PAGE_NAME);
					}
				}
			}

			public void GetPages()
			{
				if (Directory.Exists(ActiveLanguageFolderPath))
				{
					var pages = Directory.GetFiles(ActiveLanguageFolderPath);
					Pages = new List<Page>(pages.Length);
					foreach (var page in pages)
					{
						var str = page.Remove(0, ActiveLanguageFolderPath.Length + 1);
						if (!str.Contains(".meta"))
						{
							if (str == (DEFUALT_PAGE_NAME + ".page"))
							{
								if (DefualtPage == null)
									DefualtPage = new Page(this, str.Replace(".page", ""));
								Pages.Add(DefualtPage);
							}
							else
								Pages.Add(new Page(this, str.Replace(".page", "")));
						}
					}
				}
			}

			public void Load()
			{
				if (Pages == null)
					GetPages();
				if (Pages == null || Pages.Count == 0)
					return;
				foreach (var page in Pages)
				{
					page.Loaded = true;
				}
			}

			public void Unload()
			{
				if (!Loaded)
					return;
				foreach (var page in Pages)
				{
					page.Loaded = false;
				}
			}

			public static implicit operator Language(string input) { return new Language(input); }
			public static implicit operator string(Language input) { return input.LanguageName; }

			public override bool Equals(object obj)
			{
				var s = obj as string;
				if (s != null)
				{
					return LanguageName == s;
				}
				var language = obj as Language;
				if (language != null)
				{
					return LanguageName == language.LanguageName;
				}
				return false;
			}

			public bool Equals(Language other)
			{
				if (ReferenceEquals(null, other))
					return false;
				if (ReferenceEquals(this, other))
					return true;
				return string.Equals(LanguageName, other.LanguageName);
			}

			public override int GetHashCode() { return (LanguageName != null? LanguageName.GetHashCode() : 0); }

			public static bool operator ==(Language left, Language right) { return Equals(left, right); }
			public static bool operator !=(Language left, Language right) { return !Equals(left, right); }
		}
	}
}