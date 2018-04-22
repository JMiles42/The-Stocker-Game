using ForestOfChaosLib;
using ForestOfChaosLib.FoCsUI.Image;

public class ScoreTextComponent: FoCsBehavior
{
	public TextComponentBase Text;

	private void OnEnable()
	{
		ScoreManager.OnScoreCalculated += OnScoreCalculated;
		Text.TextData = "";
	}

	private void OnScoreCalculated(ScoreManager.ScoreData scoreData)
	{
		Text.TextData = scoreData.ToString();
	}

	private void OnDisable()
	{
		ScoreManager.OnScoreCalculated -= OnScoreCalculated;
	}
}