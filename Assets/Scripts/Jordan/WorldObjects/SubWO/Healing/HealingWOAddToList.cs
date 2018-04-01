using ForestOfChaosLib.AdvVar.RuntimeRef.Components;

public class HealingWOAddToList: BaseAddToRTSet<HealingWO, HealingWOList>
{
	public override HealingWO Value => GetComponent<HealingWO>();
}