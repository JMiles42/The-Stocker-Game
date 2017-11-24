using System;
using JMiles42.Extensions;
using UnityEngine;

[CreateAssetMenu]
public class MapReferance: ScriptableObject
{
	[SerializeField]
	private Map _builtMap;

	public Map BuiltMap
	{
		get { return _builtMap; }
		set
		{
			_builtMap = value;
			OnMapUpdate.Trigger();
		}
	}

	public event Action OnMapUpdate;
}