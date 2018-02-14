using ForestOfChaosLib;

public abstract class Placeable: FoCsBehavior {
	public virtual float GetMultiplyer() { return 0f; }

	public abstract int GetScore();
}