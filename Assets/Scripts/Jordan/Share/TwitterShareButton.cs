using ForestOfChaosLib;
using ForestOfChaosLib.AdvVar;
using ForestOfChaosLib.FoCsUI.Button;

public class TwitterShareButton: FoCsBehavior
{
	private ScoreManager.ScoreData _scoreData;
	public StringReference Seed;
	public ButtonComponentBase TwitterShareBtn;

	private void OnEnable()
	{
		ScoreManager.OnScoreCalculated += OnScoreCalculated;
		TwitterShareBtn.onMouseClick += OnMouseClick;
	}

	private void OnMouseClick()
	{
		if(_scoreData != null)
			TwitterShare.ShareStockerData(Seed.Value, _scoreData.FinalScore, _scoreData.died, _scoreData.DeadScore);
	}

	private void OnScoreCalculated(ScoreManager.ScoreData scoreData)
	{
		_scoreData = scoreData;
	}

	private void OnDisable()
	{
		ScoreManager.OnScoreCalculated -= OnScoreCalculated;
		TwitterShareBtn.onMouseClick -= OnMouseClick;
	}
}