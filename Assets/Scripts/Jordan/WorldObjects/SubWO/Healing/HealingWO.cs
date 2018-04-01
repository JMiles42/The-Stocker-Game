public class HealingWO: WorldObject
{
	public enum HealingSize
	{
		Small
	}

	public HealingSize Size;

	public int GetHealingAmount()
	{
		switch(Size)
		{
			case HealingSize.Small:
				return 20;
			default:
				return 1;
		}
	}
}