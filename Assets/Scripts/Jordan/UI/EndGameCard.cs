using ForestOfChaosLib;
using ForestOfChaosLib.AdvVar;
using ForestOfChaosLib.FoCsUI.Image;
using UnityEngine;
using UnityEngine.UI;

public class EndGameCard : FoCsBehavior
{
	public BoolReference EndGame;
	public TextComponentBase ScoreTxt;
	public TextComponentBase DeadTxt;
	public TextComponentBase ScoreDeadTxt;
	public Image Display;
	public Sprite DeadImg;
	public Sprite AliveImg;

	private void OnEnable()
	{
		ScoreManager.OnScoreCalculated += OnScoreCalculated;
		ScoreTxt.TextData ="";
		DeadTxt.TextData = "";
		ScoreDeadTxt.TextData = "";
		EndGame.OnValueChange += OnValueChange;
		Display.gameObject.SetActive(false);
	}

	private void OnValueChange()
	{
		Display.gameObject.SetActive(true);
	}

	private void OnScoreCalculated(ScoreManager.ScoreData obj)
	{
		ScoreTxt.TextData = obj.FinalScore.ToString();
		DeadTxt.TextData = obj.died?
			"Yes" :
			"No";
		ScoreDeadTxt.TextData = obj.died?
			obj.FinalScore.ToString() :
			"N/A";
		Display.sprite = obj.died?
			DeadImg :
			AliveImg;
	}

	private void OnDisable()
	{
		ScoreManager.OnScoreCalculated -= OnScoreCalculated;
		EndGame.OnValueChange -= OnValueChange;
	}
}
