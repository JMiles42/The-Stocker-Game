using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAudioTest : MonoBehaviour
{
	public event Action OnPlayAudio;

	public AudioManager AudioManager;
	// Update is called once per frame


	void Start()
	{
		AudioManager = GetComponent<AudioManager>();
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.P))
		{
			if (OnPlayAudio != null)
			{
				OnPlayAudio();
			}
		}
	}
}
