using UnityEngine;

public class TwitterShare {
	public static string TWITTER_URL = "http://twitter.com/intent/tweet";

	public static string TWITTER_LANG = "en";
	public static string TWITTER_SHARE_TEXT = "This is a test ignore me! YAAAAY: {0} YAAY for API learning";


	public static void TweetTweet(string score) {
		Application.OpenURL(TWITTER_URL + "?text=" + WWW.EscapeURL(string.Format(TWITTER_SHARE_TEXT, score)));
	}

	public static void TweetTweet(string str, object score) {
		Application.OpenURL(TWITTER_URL + "?text=" + WWW.EscapeURL(string.Format(str, score)));
	}

	public static string STOCKER_URL = "https://jrpcollins.itch.io/the-stocker";
	public static string STOCKER_SHARE_TEXT = "I just scored {0}";

	public static void ShareStockerData(string seed, int score, bool died, int diedScore)
	{
		string message;

		if(died)
			message = $"I just scored a {score} on the seed {seed}, Unfortunately the adventurer died with a score of {diedScore}. Feel free to challenge my score!";
		else
			message = $"I just scored a {score} on the seed {seed}. Feel free to challenge my score!";

		//if(died)
		//	message = $"I just scored a {score}, Unfortunately the adventurer died with a score of {diedScore}. Feel free to challenge my score!";
		//else
		//	message = $"I just scored a {score}. Feel free to challenge my score!";

		Application.OpenURL(TWITTER_URL + "?text=" + WWW.EscapeURL(message) + "&url="+WWW.EscapeURL(STOCKER_URL));
	}
}