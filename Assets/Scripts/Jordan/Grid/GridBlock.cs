using JMiles42;
using JMiles42.Grid;
using JMiles42.Types;

public class GridBlock: JMilesBehavior
{
    public GridPosition _gridPosition = Vector2I.Zero;

    public GridPosition GridPosition
    {
        get { return _gridPosition; }
        set { _gridPosition = value; }
    }

    public TileType TileType;
}