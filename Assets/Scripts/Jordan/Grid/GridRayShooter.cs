using System;
using JMiles42.Components;
using JMiles42.Systems.InputManager;
using JMiles42.UnityInterfaces;
using UnityEngine;

public class GridRayShooter: JMilesBehavior, IEventListening
{
    public Camera Camera;

    public void OnEnable() { PlayerInputManager.Fire1.onKeyDown += OnKeyDown; }
    public void OnDisable() { PlayerInputManager.Fire1.onKeyDown -= OnKeyDown; }

    public void Start()
    {
        if (Camera == null)
        {
            Camera = Camera.main;
        }
    }

    private void OnKeyDown() { RayShoot(); }

    private void RayShoot()
    {
        if (Camera == null)
        {
            Camera = Camera.main;
        }

        Ray ray = Camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit rayhit = new RaycastHit();
        if (Physics.Raycast(ray, out rayhit, 500f))
        {
            if (!rayhit.transform)
                return;

            Player.Instance.transform.position = rayhit.point;

            if (rayhit.transform.GetComponent<GridRayHit>())
            {
                var pos = rayhit.transform.GetComponent<GridRayHit>().GetHitPosistion(rayhit);
                if (GridBlock.Blocks.ContainsKey(pos))
                {
                    GridBlock.Blocks[pos].GetComponent<MeshRenderer>().material.color = Color.red;
                }
            }
        }
    }
}