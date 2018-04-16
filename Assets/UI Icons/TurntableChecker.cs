using ForestOfChaosLib;
using UnityEngine;

public class TurntableChecker : FoCsBehavior
{
	public GameObject Prefab;
	void Start()
	{
		if(Turntable.InstanceNull)
			Instantiate(Prefab);
	}
}
