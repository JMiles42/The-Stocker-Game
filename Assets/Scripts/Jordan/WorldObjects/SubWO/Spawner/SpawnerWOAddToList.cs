using ForestOfChaosLib.AdvVar.RuntimeRef.Components;

public class SpawnerWOAddToList: BaseAddToRTSet<SpawnerWO, SpawnerWOList>
{
	public override SpawnerWO Value => GetComponent<SpawnerWO>();
}