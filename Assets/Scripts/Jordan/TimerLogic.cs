using ForestOfChaosLib.AdvVar;
using UnityEngine;

public class TimerLogic : MonoBehaviour
{
	public FloatReference Timer;
	public FloatVariable CountdownMax = 5*60;

	void Start()
	{
		Timer.Value = CountdownMax;
	}

	void Update()
	{
		Timer.Value -= Time.deltaTime;
	}
}
