using System;
using System.Collections.Generic;
using JMiles42.Grid;

[Serializable]
public class TilePath: List<GridPosition>
{
	public TilePath(int size = 0)
		: base(size)
	{ }

	public TilePath(List<GridPosition> _path)
		: base(_path)
	{ }

	public TilePath(IEnumerable<GridPosition> _path)
		: base(_path)
	{ }

	public static implicit operator TilePath(GridPosition[] list) => new TilePath(list);
}