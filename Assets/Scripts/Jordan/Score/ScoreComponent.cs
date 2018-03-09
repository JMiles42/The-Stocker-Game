using ForestOfChaosLib;
using UnityEngine;

public class ScoreComponent: FoCsBehavior
{
	public ScoreManager Manger;

	void Update()
	{
		if(Input.GetKey(KeyCode.Space))
		{
			Manger.CalculateScore();
		}
	}
}