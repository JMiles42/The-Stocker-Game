using System;
using System.Collections.Generic;
using Facebook.Unity;
using JMiles42.Extensions;
using JMiles42.Systems.Screenshot;
using UnityEngine;

public class FBStuff: MonoBehaviour
{
#if FB
	public string auth;

	// Use this for initialization
	void Start()
	{
		FB.Init(OnInitComplete, OnHideUnity, auth);
	}

	private void OnHideUnity(bool isunityshown)
	{
		Debug.Log($"OnHideUnity{isunityshown}");
	}

	private void OnInitComplete()
	{
		Debug.Log("OnInitComplete");
		FB.ActivateApp();

		FB.LogInWithPublishPermissions(new List<string> {"publish_actions"}, FacebookLoginResult);
	}

	public void ShareLink(
			string link = "http://www.jmiles42.com",
			string title = "This is my website",
			string desc = "This is an Facebook API Test, Please Ignore.")
	{
		FB.ShareLink(new Uri(link), title, desc);
	}

	public void ScreenShoot(string imageName = "another Image.png")
	{
		ScreenShoot(Screenshot.TakeScreenShot(new ScreenShotArgs(Camera.main)), imageName);
	}

	public void ScreenShoot(byte[] picture, string imageName = "another Image.png")
	{
		var www = new WWWForm();
		www.AddBinaryData("image", picture, imageName);
		FB.API("me/photos", HttpMethod.POST, APIcallback, www);
	}

	private void APIcallback(IGraphResult result)
	{ }

	public ILoginResult Result;

	private void FacebookLoginResult(ILoginResult result)
	{
		Result = result;
		if(result.AccessToken.IsNotNull())
			Debug.Log($"{Result.AccessToken}" +
					  $"{Result.AccessToken.TokenString}" +
					  $"{Result.AccessToken.ExpirationTime}" +
					  $"{Result.AccessToken.LastRefresh}" +
					  $"{Result.AccessToken.Permissions}" +
					  $"{Result.AccessToken.UserId}");
	}
#endif
}