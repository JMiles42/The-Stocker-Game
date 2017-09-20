using JMiles42.Generics;
using UnityEngine;

[CreateAssetMenu(fileName = "GridMaster", menuName = "SO/Master/GridMaster", order = 0)]
public class GridMaster: SingletonScriptableObject<GridMaster>
{
	public float GridSize = 1;
	public float StartingPoint = 0;
}