using JMiles42.Extensions;
using JMiles42.Generics;
using UnityEngine;

public class PlaceOnValidClick: Singleton<PlaceOnValidClick> {
	public GameObject ObjectToSpawn;
	public void OnEnable() { GameplayInputManager.Instance.OnBlockPressed += Instance_OnBlockPressed; }

	public void OnDisable() { GameplayInputManager.Instance.OnBlockPressed -= Instance_OnBlockPressed; }

	private void Instance_OnBlockPressed(GridBlock arg1, bool arg2) {
		if (ObjectToSpawn == null)
			return;
		if (!arg2)
			return;
		var go = Instantiate(ObjectToSpawn);
		go.transform.position = ((Vector3) arg1.GridPosition).FromX_Y2Z();
		ObjectToSpawn = null;
	}
}