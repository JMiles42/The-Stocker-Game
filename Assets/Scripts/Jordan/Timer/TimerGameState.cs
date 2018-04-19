using ForestOfChaosLib;
using ForestOfChaosLib.AdvVar;
using UnityEngine;

public class TimerGameState: FoCsBehavior
{
	public BoolVariable GameActive;
	public FloatReference Timer;
	public ScoreManager ScoreM;
	public BoolVariable TimerCounting;
	public BoolVariable GameEnd;

	private void OnEnable()
	{
		Timer.OnValueChange += OnValueChange;
	}

	private void OnValueChange()
	{
		if(Timer.Value <= 0)
			GameOver();
	}

	private void GameOver()
	{
		Debug.Log("Game over man, Game over");
		ScoreM.CalculateScore();
		TimerCounting.Value = false;
		GameEnd.Value = true;
	}

	private void OnDisable()
	{
		Timer.OnValueChange -= OnValueChange;
	}
}