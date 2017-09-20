using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Generated;
using JMiles42.Extensions;
using JMiles42.Generics;
using JMiles42.UnityInterfaces;

namespace JMiles42.Systems.InputManager
{
	public class PlayerInputManager: Singleton<PlayerInputManager>, IStart, IUpdate
	{
		public static InputAxis Horizontal
		{
			get { return Instance.InputsToUse[0]; }
			set { Instance.InputsToUse[0] = value; }
		}

		public static InputAxis Vertical
		{
			get { return Instance.InputsToUse[1]; }
			set { Instance.InputsToUse[1] = value; }
		}

		public static InputAxis MouseScroll
		{
			get { return Instance.InputsToUse[2]; }
			set { Instance.InputsToUse[2] = value; }
		}

		public static InputAxis Jump
		{
			get { return Instance.InputsToUse[3]; }
			set { Instance.InputsToUse[3] = value; }
		}

		public static InputAxis Fire1
		{
			get { return Instance.InputsToUse[4]; }
			set { Instance.InputsToUse[4] = value; }
		}

		public InputAxisSetupSO InputAxisSetup;

		public List<InputAxis> InputsToUse
		{
			get { return InputAxisSetup.inputsToUse; }
			set { InputAxisSetup.inputsToUse = value; }
		}

		public static List<InputAxis> Inputs
		{
			get { return Instance.InputsToUse; }
			set { Instance.InputsToUse = value; }
		}

		public float DeadZone = 0.05f;

		public Vector2 MousePos;

		public static Vector2 MousePosition
		{
			get { return Instance.MousePos; }
			set { Instance.MousePos = value; }
		}

		public void Start()
		{
			if (InputAxisSetup == null)
				ResetAxis();
			DontDestroyOnLoad(this);
		}

		public void Update()
		{
			GetAxisValues();
			TriggerInputs();
		}

		private void GetAxisValues()
		{
			foreach (var input in InputsToUse)
				input.UpdateData();
			MousePos = Input.mousePosition;
		}

		public void TriggerInputs()
		{
			foreach (var key in InputsToUse)
			{
				if (Input.GetButtonUp(key))
				{
					key.onKeyUp.Trigger();

					if (key.Value > 0)
						key.onKeyPositiveUp.Trigger();
					else if (key.Value < 0)
						key.onKeyNegativeUp.Trigger();
				}
				else if (Input.GetButtonDown(key))
				{
					key.onKeyDown.Trigger();

					if (key.Value > 0)
						key.onKeyPositiveDown.Trigger();
					else if (key.Value < 0)
						key.onKeyNegativeDown.Trigger();
				}
				else if (Math.Abs(key.Value) > DeadZone)
				{
					key.onKey.Trigger(key.Value);
					key.onKeyNoValue.Trigger();
				}
			}
		}

		public static InputAxis GetAxis(PlayerInputManagerEnum axis) { return Instance.InputsToUse.FirstOrDefault(input => input.Axis == axis.ToString()); }

		public static InputAxis GetAxis(string axis) { return Instance.InputsToUse.FirstOrDefault(input => input.Axis == axis); }

		public static InputAxis GetAxis(int axis) { return axis > Inputs.Count? Instance.InputsToUse[0] : Instance.InputsToUse[axis]; }

		public InputAxis this[int key]
		{
			get { return InputsToUse[key]; }
			set { InputsToUse[key] = value; }
		}

		public InputAxis this[PlayerInputManagerEnum key]
		{
			get { return GetAxis(key); }
		}

		public InputAxis this[string key]
		{
			get { return GetAxis(key); }
		}

		private void ResetAxis()
		{
			InputAxisSetup = Resources.Load<InputAxisSetupSO>("Default Input Axis Setup");

			Inputs = new List<InputAxis> {"Horizontal", "Vertical", "MouseScroll", "Jump", "Fire1"};
		}
	}
}