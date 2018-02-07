using System;
using JMiles42.AdvVar;
using JMiles42.Maths.Random;

[Serializable]
public class MapSettings
{
	public StringReference Seed = RandomStrings.GetRandomString(8);
	public IntRange corridorLength = new IntRange(6, 10); // The range of lengths corridors between rooms can have
	public IntRange numRooms = new IntRange(15, 20); // The range of the number of rooms there can be.
	public IntRange roomHeight = new IntRange(3, 10); // The range of heights rooms can have.
	public IntRange roomWidth = new IntRange(3, 10); // The range of widths rooms can have.
	public int columns = 100; // The number of columns on the board (how wide it will be)
	public int rows = 100; // The number of rows on the board (how tall it will be).
}