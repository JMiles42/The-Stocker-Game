using ForestOfChaosLib;
using ForestOfChaosLib.Grid;
using ForestOfChaosLib.Types;

public class GridBlock: FoCsBehavior
{
    public GridPosition _gridPosition = Vector2I.Zero;

    public GridPosition GridPosition
    {
        get { return _gridPosition; }
        set { _gridPosition = value; }
    }

    public TileType TileType;
}