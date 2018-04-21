using ForestOfChaosLib;
using ForestOfChaosLib.AdvVar;
using ForestOfChaosLib.Animation;
using UnityEngine;

public class TriggerAttack : FoCsBehavior
{
	public AnimatorKey Key = new AnimatorKey{triggerData = false,KeyType = AnimatorKey.AnimType.Trigger,Key = "TriggerAnim"};
	public Animator Animator;
	public bool Animating = true;

	public FloatVariable Min = 1;
	public FloatVariable Max = 20;

	public float startTime;
	public float endTime;
	public BoolReference Event;

	void Start()
	{
		startTime = 0;
		endTime = Random.Range(Min, Max);
	}

	void Update()
	{
		var time = Time.time;
		if(time >= endTime)
		{
			startTime = time;
			endTime = time + Random.Range(Min, Max);
			Key.triggerData = true;
			Key.CalculateAnimator(Animator);
			Event.Value = true;
			Event.Value = false;
		}
	}

	void Reset()
	{
		Animator = GetComponent<Animator>();
		Key = new AnimatorKey
			  {
				  triggerData = true,
				  KeyType = AnimatorKey.AnimType.Trigger,
				  Key = "TriggerAnim"
			  };
	}
}
