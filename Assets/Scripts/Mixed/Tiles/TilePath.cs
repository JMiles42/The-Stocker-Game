using System;
using System.Collections;
using System.Collections.Generic;
using JMiles42;
using JMiles42.Extensions;

[Serializable]
public class TilePath: IEnumerable<Vector2I>
{
	public List<Vector2I> Path = new List<Vector2I>(0);

	public int Length
	{
		get { return Path.Count; }
	}

	public TilePath(int size = 0)
	{
		Path = new List<Vector2I>(size);
	}

	public TilePath(List<Vector2I> _path)
	{
		Path = _path;
	}

	public TilePath(IEnumerable<Vector2I> _path)
	{
		Path = new List<Vector2I>(_path);
	}

	public Vector2I this[int index]
	{
		get
		{
			if(Path.InRange(index))
				return Path[index];
			return Vector2I.MaxInt;
		}
		set
		{
			if(Path.InRange(index))
				Path[index] = value;
		}
	}

	public static implicit operator List<Vector2I>(TilePath tP)
	{
		return tP.Path;
	}

	public static implicit operator TilePath(Vector2I[] list)
	{
		return new TilePath(list);
	}

	public static implicit operator TilePath(List<Vector2I> list)
	{
		return new TilePath(list);
	}

	public IEnumerator<Vector2I> GetEnumerator()
	{
		return Path.GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}

	public void Add(Vector2I vector2I)
	{
		Path.Add(vector2I);
	}
}