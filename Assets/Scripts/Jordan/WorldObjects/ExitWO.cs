using ForestOfChaosLib.AdvVar;

public class ExitWO: WorldObject
{
	public ExitRef Reference;
	public BoolReference HasReference;

	private void OnDestroy()
	{
		Reference.Reference = null;
		HasReference.Value = false;
	}
}