using System.Collections.Generic;
using ForestOfChaosLib;
using ForestOfChaosLib.AdvVar;
using UnityEngine;

public class SetBoolOnEnable : FoCsBehavior
{
	public BoolReference Bool;

	private void OnEnable()
	{
		Bool.Value = true;
	}
}
