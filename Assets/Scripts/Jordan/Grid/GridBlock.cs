using System.Collections.Generic;
using JMiles42;
using JMiles42.Components;

public class GridBlock: JMilesBehavior
{
    public static Dictionary<Vector2I, GridBlock> Blocks = new Dictionary<Vector2I, GridBlock>();

    public Vector2I _gridPosition = Vector2I.Zero;

    public Vector2I GridPosition
    {
        get { return _gridPosition; }
        set { _gridPosition = value; }
    }
}