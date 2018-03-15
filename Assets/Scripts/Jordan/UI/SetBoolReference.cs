using ForestOfChaosLib;
using ForestOfChaosLib.AdvVar;
using UnityCallsEnums;

public class SetBoolReference: FoCsBehavior
{
	public UnitySingleCalls CallTime;
	public BoolReference Reference;
	public BoolVariable ValueToSetTo = false;

	private void Start()
	{
		if(CallTime == UnitySingleCalls.Start)
			Reference.Value = ValueToSetTo.Value;
	}

	private void Awake()
	{
		if(CallTime == UnitySingleCalls.Awake)
			Reference.Value = ValueToSetTo.Value;
	}

	private void OnDisable()
	{
		if(CallTime == UnitySingleCalls.OnDisable)
			Reference.Value = ValueToSetTo.Value;
	}

	private void OnDestroy()
	{
		if(CallTime == UnitySingleCalls.OnDestroy)
			Reference.Value = ValueToSetTo.Value;
	}

	private void OnEnable()
	{
		if(CallTime == UnitySingleCalls.OnEnable)
			Reference.Value = ValueToSetTo.Value;
	}
}