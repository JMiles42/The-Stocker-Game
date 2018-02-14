using ForestOfChaosLib.AdvVar;
using ForestOfChaosLib.Generics;

public class ScoreCalculator: Singleton<ScoreCalculator>
{
	public ScorableObjectList ScorableObjectList;
	public IntReference Score;

	public static void GenerateScore()
	{
		Instance.GenerateScoreInternal();
	}

	private void GenerateScoreInternal()
	{
		
	}
}