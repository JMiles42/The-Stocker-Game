using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawnerPlaceable: Placeable {
	public override float GetMultiplyer() { return 3f; }
	public override int GetScore() { return 0; }
}