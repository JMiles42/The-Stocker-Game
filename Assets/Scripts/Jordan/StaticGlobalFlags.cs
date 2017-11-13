using JMiles42.Events;

public static class StaticGlobalFlags
{
	static StaticGlobalFlags() { gameInteractable = false; }
	public static BoolEventVariable gameInteractable;
}