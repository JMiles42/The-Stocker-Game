using ForestOfChaosLib;
using UnityEngine;

public class MaterialRandomizer : FoCsBehavior
{
	public Material[] Materials;

	private void Start()
	{
		var c = GetComponent<Renderer>();
		c.material = Materials[Random.Range(0, Materials.Length)];
	}
}
