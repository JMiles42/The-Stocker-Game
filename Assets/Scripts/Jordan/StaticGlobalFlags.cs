using JMiles42.EventVariable;

public static class StaticGlobalFlags
{
	static StaticGlobalFlags() { gameInteractable = false; }
	public static BoolEventVariable gameInteractable;
}