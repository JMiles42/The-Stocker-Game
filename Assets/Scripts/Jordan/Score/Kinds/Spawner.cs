﻿using UnityEngine;

public class Spawner: ScorableObject
{
	[SerializeField]
	private float _scoreMultiplier;

	[SerializeField]
	private float _score;

	public override float ScoreMultiplier
	{
		get { return _scoreMultiplier; }
	}

	public override float Score
	{
		get { return _score; }
	}
}