using System.Threading;
using System.Threading.Tasks;
using JMiles42;

public static class PathFindingIntegrator
{
	public static async Task<TilePath> GetPath(Vector2I start, Vector2I end) { return await Task<TilePath>.Factory.StartNew(() => GetPathAsync(start, end)); }

	public static TilePath GetPathAsync(Vector2I start, Vector2I end)
	{
		Thread.Sleep(2000);
		return new TilePath {start, end};
	}
}