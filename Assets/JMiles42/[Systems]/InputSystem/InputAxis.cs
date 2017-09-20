using System;
using UnityEngine;

namespace JMiles42.Systems.InputManager
{
	[Serializable]
	public class InputAxis
	{
		/// <summary>
		/// The Axis that the coder uses as referance
		/// </summary>
		public string Axis;

		/// <summary>
		/// The Axis that is sent to Unity's input manager
		/// </summary>
		public string UnityAxis;

		public float Value
		{
			get { return ValueInverted? m_Value : -m_Value; }
			set { m_Value = ValueInverted? value : -value; }
		}

		[SerializeField] private float m_Value;
		public bool ValueInverted;

		public Action onKeyDown;
		public Action onKeyUp;
		public Action onKeyPositiveDown;
		public Action onKeyPositiveUp;
		public Action onKeyNegativeDown;
		public Action onKeyNegativeUp;
		public Action onKeyNoValue;
		public Action<float> onKey;

		public InputAxis(string axis, bool invert = false)
		{
			Axis = UnityAxis = axis;
			ValueInverted = invert;
		}

		public InputAxis(string axis, string uAxis, bool invert = false)
		{
			Axis = axis;
			UnityAxis = uAxis;
			ValueInverted = invert;
		}

		public void UpdateData() { m_Value = Input.GetAxis(UnityAxis); }

		public static implicit operator float(InputAxis fp) { return fp.Value; }

		public static implicit operator string(InputAxis fp) { return fp.UnityAxis; }

		public static implicit operator bool(InputAxis fp) { return fp.ValueInverted; }

		public static implicit operator InputAxis(string fp) { return new InputAxis(fp); }

		public static implicit operator InputAxis(PlayerInputValues fp) { return new InputAxis(fp.ToString()); }
	}
}