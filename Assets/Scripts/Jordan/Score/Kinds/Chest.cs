using System.Collections.Generic;
using JMiles42.Systems.Item;
using UnityEngine;

public class Chest: ScorableObject
{
	public List<Item> Items;

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