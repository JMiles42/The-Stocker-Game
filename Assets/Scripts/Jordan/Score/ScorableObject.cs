using ForestOfChaosLib;

public abstract class ScorableObject: FoCsBehavior
{
	public ScorableObjectList ScorableObjectList;

	public virtual void OnEnable()
	{
		ScorableObjectList.Add(this);
	}

	public virtual void OnDisable()
	{
		ScorableObjectList.Remove(this);
	}

	public abstract float ScoreMultiplier { get; }

	public abstract float Score { get; }
}