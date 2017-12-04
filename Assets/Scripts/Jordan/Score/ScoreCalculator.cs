using JMiles42.AdvancedVariables;
using JMiles42.Generics;

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