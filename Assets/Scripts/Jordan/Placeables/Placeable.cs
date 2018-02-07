using JMiles42;
using JMiles42.Components;

public abstract class Placeable: JMilesBehavior {
	public virtual float GetMultiplyer() { return 0f; }

	public abstract int GetScore();
}