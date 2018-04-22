using System;
using ForestOfChaosLib.AdvVar;
using ForestOfChaosLib.CSharpExtensions;
using ForestOfChaosLib.FoCsUI.Image;
using UnityEngine;

public class TimerDisp : MonoBehaviour
{
	public FloatReference Timer;
	public TextComponentBase Display;
	private void OnEnable()
	{
		Timer.OnValueChange += OnValueChange;
	}

	private void OnValueChange()
	{
		var span = new TimeSpan(0, 0, Timer.Value.CastToInt());
		Display.TextData = $"{(int)span.TotalMinutes}:{span.Seconds:00}";
	}

	private void OnDisable()
	{
		Timer.OnValueChange -= OnValueChange;
	}

	private void Reset()
	{
		Display = GetComponent<TextComponentBase>();
	}
}
