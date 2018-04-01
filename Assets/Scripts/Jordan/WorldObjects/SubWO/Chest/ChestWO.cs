public class ChestWO: WorldObject
{
	public ChestSize Size;

	public enum ChestSize
	{
		Common,
		Rare,
		Legendary
	}

	public int GetSize()
	{
		switch(Size)
		{
			case ChestSize.Common:
				return 1;
			case ChestSize.Rare:
				return 2;
			case ChestSize.Legendary:
				return 4;
			default:
				return 1;
		}
	}
}