using System;
using System.Collections;
using System.Collections.Generic;
using ForestOfChaosLib.AdvVar;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
	public bool EnableAudio;
	public AudioSource AudioSource;
	public List<AudioClip> ListOfAudioClips;
	//public AudioClip AudioClip;

	private int LastNumberGenerated; //the last number generated
	private int PlayCount; //Number of times a clip as played
	private int randomNumber;
	private bool IsClipReady;

	public int WaitTime;
	public int MaxWaitTime;

	public BoolReference Event;

	public enum AudioUseageType
	{
		//Is going to be played in Update
		Background,
		//is going to be played when called
		Agent,
	}

	public AudioUseageType MyAudioUseageType;

	void OnEnable()
	{
		if (Event)
		{
			Event.OnValueChange += OnValueChange;
		}
		
		//adding Audio source if none is there
		AudioSource = GetComponent<AudioSource>();
		if (AudioSource == null)
		{
			this.gameObject.AddComponent<AudioSource>();
			AudioSource = GetComponent<AudioSource>();
		}
	}

	private void OnValueChange()
	{
		if (Event.Value)
		{
			//THIS IS WHERE YOU PLAY AUDIO 
			PlayObjectAudio();
		}
	}

	void OnDisable()
	{
		if (Event)
		{
			Event.OnValueChange -= OnValueChange;
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (EnableAudio)
		{
			if (MyAudioUseageType == AudioUseageType.Background)
			{
				PlayBackgroundAudio();
			}
		}



	} //END UPDATE

	public void PlayBackgroundAudio()
	{
		if (IsClipReady)
		{
			PlayAudio();
			IsClipReady = false;
		}

		if (!AudioSource.isPlaying)
		{
			PickATrack();
			if (WaitTime >= MaxWaitTime)
			{
				IsClipReady = true;
				WaitTime = 0;
			}
			else
			{
				WaitTime++;
			}

		}
	}

	public void PlayObjectAudio()
	{
		PickATrack();
		PlayAudio();
	}

	public void PickATrack()
	{
		randomNumber = GenerateRandomNumber();
		bool IsTrackSet = false;
		while (!IsTrackSet)
		{
			if (CompareNumber(LastNumberGenerated, randomNumber))
			{
				AudioSource.clip = ListOfAudioClips[randomNumber];
				LastNumberGenerated = randomNumber;
				IsTrackSet = true;
			}
			else
			{
				randomNumber = GenerateRandomNumber();
				IsTrackSet = false;
			}
		}
	}

	public int GenerateRandomNumber()
	{
		return Random.Range(0, ListOfAudioClips.Count);
	}

	public bool CompareNumber(int LastNumber, int NewNumber)
	{
		if (NewNumber == LastNumber)
		{
			return false;
		}
		return true;
	}

	public void PlayAudio()
	{
		AudioSource.Play();
	}

}
