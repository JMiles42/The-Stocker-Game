using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FBInputManageas: MonoBehaviour
{
	[SerializeField]
	private FBStuff _fbStuff;

	public FBStuff FBStuff
	{
		get { return _fbStuff ?? (_fbStuff = GetComponent<FBStuff>()); }
		set { _fbStuff = value; }
	}

	// Use this for initialization
	void Start()
	{ }

	// Update is called once per frame
	void Update()
	{
		if(Input.GetKey(KeyCode.Space))
		{
			FBStuff.ScreenShoot();
		}
	}
}