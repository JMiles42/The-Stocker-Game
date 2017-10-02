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
}