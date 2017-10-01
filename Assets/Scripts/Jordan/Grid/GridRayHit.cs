using JMiles42;
using JMiles42.Components;
using UnityEngine;

public class GridRayHit: JMilesBehavior
{
    public Vector2 rayhit = new Vector2I();
    public Vector2I hitPoint = new Vector2I();

    public Vector2I GetHitPosistion(RaycastHit hit)
    {
        int x = Mathf.RoundToInt(hit.point.x);
        int y = Mathf.RoundToInt(hit.point.z);
        rayhit = new Vector3(hit.point.x, hit.point.z);
        hitPoint = new Vector2I(x, y);
        return hitPoint;
    }
}