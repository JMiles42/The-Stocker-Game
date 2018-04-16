using ForestOfChaosLib.AdvVar;
using UnityEngine;

public class TimerLogic : MonoBehaviour
{
	public FloatReference Timer;
	public FloatVariable CountdownMax = 5*60;

	public BoolVariable TimerCounting = true;

	void Start()
	{
		Timer.Value = CountdownMax;
	}

	void Update()
	{
		if(TimerCounting.Value)
			Timer.Value -= Time.deltaTime;
	}
}
