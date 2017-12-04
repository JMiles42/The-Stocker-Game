using JMiles42.Components;

public abstract class ScorableObject: JMilesBehavior
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