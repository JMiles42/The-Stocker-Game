using System;
using System.Collections;
using System.Collections.Generic;
using JMiles42;
using JMiles42.Extensions;

[Serializable]
public class TilePath: IEnumerable<Vector2I> {
	public List<Vector2I> Path = new List<Vector2I>(0);

	public int Length {
		get { return Path.Count; }
	}
	public TilePath() { Path = new List<Vector2I>(0); }
	public TilePath(int size) { Path = new List<Vector2I>(size); }
	public TilePath(List<Vector2I> _path) { Path = _path; }
	public TilePath(IEnumerable<Vector2I> _path) { Path = new List<Vector2I>(_path); }

	public Vector2I this[int index] {
		get {
			if (Path.InRange(index))
				return Path[index];
			return null;
		}
		set {
			if (Path.InRange(index))
				Path[index] = value;
		}
	}

	public IEnumerator<Vector2I> GetEnumerator() { return Path.GetEnumerator(); }
	IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
}