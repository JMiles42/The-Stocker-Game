using System.Linq;
using JMiles42.Components;
using JMiles42.Extensions;
using JMiles42.UnityInterfaces;
using UnityEngine;

public class GridRayShooter: JMilesBehavior, IEventListening
{
	public Camera Camera;

	public GridBlockListVariable blocks;

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
		if(Camera == null)
		{
			Camera = Camera.main;
		}
	}

	private void OnPrimaryClick(Vector2 screenPos)
	{
		var gB = GetGridBlock(screenPos);
		if(gB.IsNotNull())
			GameplayInputManager.Instance.GridBlockPressed(gB, true);
	}

	private void OnSecondaryClick(Vector2 screenPos)
	{
		var gB = GetGridBlock(screenPos);
		if(gB.IsNotNull())
			GameplayInputManager.Instance.GridBlockPressed(gB, false);
	}

	public GridBlock GetGridBlock(Vector2 pos)
	{
		if(Camera == null)
			Camera = Camera.main;

		var ray = Camera.ScreenPointToRay(pos);
		var rayhit = new RaycastHit();
		if(Physics.Raycast(ray, out rayhit, 500f))
		{
			if(!rayhit.transform)
				return null;

			if(rayhit.transform.GetComponent<GridRayHit>())
			{
				var hitPosistion = rayhit.transform.GetComponent<GridRayHit>().GetHitPosistion(rayhit);
				var gB = blocks.Value.Find(a => a.GridPosition == hitPosistion);
				if(gB != null)
				{
					return gB;
				}
			}
		}
		return null;
	}
}