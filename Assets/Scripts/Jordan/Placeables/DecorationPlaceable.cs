using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorationPlaceable: Placeable {
	public override float GetMultiplyer() { return 0.01f; }
	public override int GetScore() { return 0; }
}