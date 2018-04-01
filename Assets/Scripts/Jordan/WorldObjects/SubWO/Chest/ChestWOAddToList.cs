using ForestOfChaosLib.AdvVar.RuntimeRef.Components;

public class ChestWOAddToList: BaseAddToRTSet<ChestWO, ChestWOList>
{
	public override ChestWO Value => GetComponent<ChestWO>();
}