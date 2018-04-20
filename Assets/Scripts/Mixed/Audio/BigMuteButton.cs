using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigMuteButton : MonoBehaviour
{
	public AudioManager[] a_AudioManagers;
	public bool MuteAudio;
	// Use this for initialization
	void Start()
	{
		a_AudioManagers = FindObjectsOfType <AudioManager>();
		foreach (AudioManager am in a_AudioManagers)
		{
			am.EnableAudio = MuteAudio;
		}
	}
}
