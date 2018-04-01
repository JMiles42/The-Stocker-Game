public class SpawnerWO: WorldObject
{
	public enum SpawnerSize
	{
		Small,
		Boss = 3
	}

	public SpawnerSize Size;

	public int GetSize()
	{
		switch(Size)
		{
			case SpawnerSize.Small:
				return 1;
			case SpawnerSize.Boss:
				return 5;
			default:
				return 1;
		}
	}

	public int GetDamageDealt()
	{
		switch(Size)
		{
			case SpawnerSize.Small:
				return 4;
			case SpawnerSize.Boss:
				return 20;
			default:
				return 0;
		}
	}
}