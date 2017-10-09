public class SpawnerPlaceable: Placeable {
	public int SpawnAmount = 1;
	public override float GetMultiplyer() { return SpawnAmount * 0.25f; }
	public override int GetScore() { return 0; }
}