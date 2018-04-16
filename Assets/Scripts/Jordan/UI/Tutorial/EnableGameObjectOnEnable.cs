using ForestOfChaosLib;
using UnityEngine;

public class EnableGameObjectOnEnable : FoCsBehavior
{
	public GameObject ToEnable;

	void OnEnable()
	{
		ToEnable.SetActive(true);
	}
}
