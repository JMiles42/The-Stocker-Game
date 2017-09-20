namespace JMiles42.Maths
{
	public static class RandomStrings
	{
		public const string ALLALPHA = "abcdefghijklmnopqrstuvwxyz";
		public const string ALLNUMERIC = "1234567890";
		public const string SYMBOLS = "!@#$%^&*()-_=+`~,<.>/?;:'";
		public const string ALLCHARS = ALLALPHA + ALLNUMERIC;
		public const string NUMERICSYMBOLS = SYMBOLS + ALLNUMERIC;

		public static string GetRandomString(int length)
		{
			var s = "";
			var rand = new System.Random(System.DateTime.UtcNow.Millisecond);
			for (int i = 0; i < length; i++)
			{
				s += ALLCHARS[rand.Next(ALLCHARS.Length)];
			}
			return s;
		}

		public static string GetRandomString(int length, int seed)
		{
			var s = "";
			var rand = new System.Random(seed);
			for (int i = 0; i < length; i++)
			{
				s += ALLCHARS[rand.Next(ALLCHARS.Length)];
			}
			return s;
		}

		public static string GetRandomAltString(int length)
		{
			var s = "";
			var rand = new System.Random(System.DateTime.UtcNow.Millisecond);
			for (int i = 0; i < length; i++)
			{
				if (JMaths.IsOdd(i))
					s += ALLNUMERIC[rand.Next(ALLNUMERIC.Length)];
				else
					s += ALLALPHA[rand.Next(ALLALPHA.Length)];
			}
			return s;
		}

		public static string GetRandomAltString(int length, int seed)
		{
			var s = "";
			var rand = new System.Random(seed);
			for (var i = 0; i < length; i++)
			{
				if (JMaths.IsOdd(i))
					s += ALLNUMERIC[rand.Next(ALLNUMERIC.Length)];
				else
					s += ALLALPHA[rand.Next(ALLALPHA.Length)];
			}
			return s;
		}
	}
}