using UnityEngine;

public class RandomSky: MonoBehaviour
{
	public Camera Camera;

	private void Update()
	{
		if(Camera)
			Camera.backgroundColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
	}
}