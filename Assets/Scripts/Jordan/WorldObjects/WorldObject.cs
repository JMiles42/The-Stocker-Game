using ForestOfChaosLib;
using ForestOfChaosLib.Grid;

public class WorldObject: FoCsBehavior
{
	public GridBlockListReference GridBlockList;
	public GridBlock MyGridBlock;
	public GridPosition MyPosition;

	public void SetupObject(GridBlock gb)
	{
		gb.WorldObject = this;
		MyGridBlock = gb;
		MyPosition = gb.GridPosition;
		Position = gb.GridPosition.WorldPosition;
	}
}