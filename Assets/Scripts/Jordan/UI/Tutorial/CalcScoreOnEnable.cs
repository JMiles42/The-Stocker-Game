using ForestOfChaosLib;

public class CalcScoreOnEnable: FoCsBehavior
{
	public ScoreManager Score;

	private void OnEnable()
	{
		Score.CalculateScore();
	}
}