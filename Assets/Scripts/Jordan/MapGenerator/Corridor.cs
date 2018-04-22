using System;
using ForestOfChaosLib.Utilities.Enums;

[Serializable]
public class Corridor
{
	public int corridorLength; // How many units long the corridor is.
	public Direction_NSEW direction; // Which direction the corridor is heading from it's room.
	public int startXPos; // The x coordinate for the start of the corridor.
	public int startYPos; // The y coordinate for the start of the corridor.

	// Get the end position of the corridor based on it's start position and which direction it's heading.
	public int EndPositionX
	{
		get
		{
			if((direction == Direction_NSEW.North) || (direction == Direction_NSEW.South))
				return startXPos;
			if(direction == Direction_NSEW.East)
				return (startXPos + corridorLength) - 1;
			return (startXPos - corridorLength) + 1;
		}
	}

	public int EndPositionY
	{
		get
		{
			if((direction == Direction_NSEW.East) || (direction == Direction_NSEW.West))
				return startYPos;
			if(direction == Direction_NSEW.North)
				return (startYPos + corridorLength) - 1;
			return (startYPos - corridorLength) + 1;
		}
	}
}