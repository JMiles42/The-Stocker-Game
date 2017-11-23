using System.Threading.Tasks;
using JMiles42;

public static class PathFindingIntegrator
{
	public static Task<TilePath> GetPathAsync(Vector2I start, Vector2I end, Map map)
	{
		return Task<TilePath>.Factory.StartNew(() => GetPath(start, end, map));
	}

	public static TilePath GetPath(Vector2I start, Vector2I end, Map map)
	{
		return new TilePath {start, end};
	}
}