using JMiles42.Components;
using JMiles42.UnityInterfaces;
using UnityEngine;

public class GridRayShooter: JMilesBehavior, IEventListening
{
    public Camera Camera;

    public void OnEnable()
    {
        GameplayInputManager.Instance.OnPrimaryClick += OnPrimaryClick;
        GameplayInputManager.Instance.OnSecondaryClick += OnSecondaryClick;
    }

    public void OnDisable()
    {
        GameplayInputManager.Instance.OnPrimaryClick -= OnPrimaryClick;
        GameplayInputManager.Instance.OnSecondaryClick -= OnSecondaryClick;
    }

    public void Start()
    {
        if (Camera == null)
        {
            Camera = Camera.main;
        }
    }

    private void OnPrimaryClick(Vector2 screenPos)
    {
        var gB = GetGridBlock(screenPos);
        if (gB)
        {
            gB.GetComponent<Renderer>().material.color = Color.cyan;
        }
    }

    private void OnSecondaryClick(Vector2 screenPos)
    {
        var gB = GetGridBlock(screenPos);
        if (gB)
        {
            gB.GetComponent<Renderer>().material.color = Color.red;
        }
    }

    private void OnKeyDown() { RayShoot(Input.mousePosition); }

    public GridBlock GetGridBlock(Vector2 pos)
    {
        if (Camera == null)
        {
            Camera = Camera.main;
        }

        var ray = Camera.ScreenPointToRay(pos);
        var rayhit = new RaycastHit();
        if (Physics.Raycast(ray, out rayhit, 500f))
        {
            if (!rayhit.transform)
                return null;

            Player.Instance.transform.position = rayhit.point;

            if (rayhit.transform.GetComponent<GridRayHit>())
            {
                var hitPosistion = rayhit.transform.GetComponent<GridRayHit>().GetHitPosistion(rayhit);
                if (GridBlock.Blocks.ContainsKey(hitPosistion))
                {
                    return GridBlock.Blocks[hitPosistion];
                }
            }
        }
        return null;
    }

    private void RayShoot(Vector2 pos)
    {
        if (Camera == null)
        {
            Camera = Camera.main;
        }

        Ray ray = Camera.ScreenPointToRay(pos);
        RaycastHit rayhit = new RaycastHit();
        if (Physics.Raycast(ray, out rayhit, 500f))
        {
            if (!rayhit.transform)
                return;

            Player.Instance.transform.position = rayhit.point;

            if (rayhit.transform.GetComponent<GridRayHit>())
            {
                var hitPosistion = rayhit.transform.GetComponent<GridRayHit>().GetHitPosistion(rayhit);
                if (GridBlock.Blocks.ContainsKey(hitPosistion))
                {
                    GridBlock.Blocks[hitPosistion].GetComponent<MeshRenderer>().material.color = Color.red;
                }
            }
        }
    }
}