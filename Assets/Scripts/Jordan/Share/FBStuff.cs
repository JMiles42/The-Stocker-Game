using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;
using JMiles42.Extensions;
using UnityEngine;

public class FBStuff : MonoBehaviour
{
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

		FB.LogInWithPublishPermissions(null, FacebookLoginResult);
	}

	public ILoginResult Result;

	private void FacebookLoginResult(ILoginResult result)
	{
		Result = result;
		if (result.AccessToken.IsNotNull())
			Debug.Log($"{Result.AccessToken}" +
			          $"{Result.AccessToken.TokenString}" +
			          $"{Result.AccessToken.ExpirationTime}" +
			          $"{Result.AccessToken.LastRefresh}" +
			          $"{Result.AccessToken.Permissions}" +
			          $"{Result.AccessToken.UserId}");
	}
}