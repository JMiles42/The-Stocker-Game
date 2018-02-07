using JMiles42.AdvVar;
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