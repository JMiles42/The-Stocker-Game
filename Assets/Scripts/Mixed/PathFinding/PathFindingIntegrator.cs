using System.Threading.Tasks;
using JMiles42;

public static class PathFindingIntegrator
{
	public static async Task<TilePath> GetPath(Vector2I start, Vector2I end, Map map)
	{
		return await Task<TilePath>.Factory.StartNew(() => GetPathAsync(start, end, map));
	}

	public static TilePath GetPathAsync(Vector2I start, Vector2I end, Map map)
	{
		return new TilePath {start, end};
	}
}