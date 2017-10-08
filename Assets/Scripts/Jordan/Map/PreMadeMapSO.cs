﻿using UnityEngine;

[CreateAssetMenu(fileName = "PreMadeMap", menuName = "Map Generator/Pre Made Map", order = 0)]
public class PreMadeMapSO: MapGeneratorBase {
	public Map Map;

	public override Map GenerateMap() { return new Map(Map); }
}