using UnityEngine;

namespace JMiles42.Animation
{
	[System.Serializable]
	public struct AnimatorKey
	{
		public enum AnimType
		{
			Int,
			Float,
			Bool,
			Trigger
		}

		public string Key;
		public AnimType KeyType;

		public int intData;
		public float floatData;
		public bool boolData;
		public bool triggerData;

		public AnimatorKey CalculateAnimator(Animator animator)
		{
			if (animator == null)
				return this;
			switch (KeyType)
			{
				case AnimType.Int:
					animator.SetInteger(Key, intData);
					break;
				case AnimType.Float:
					animator.SetFloat(Key, floatData);
					break;
				case AnimType.Bool:
					animator.SetBool(Key, boolData);
					break;
				case AnimType.Trigger:
					if (triggerData)
					{
						triggerData = false;
						animator.SetTrigger(Key);
					}
					break;
			}
			return this;
		}
	}
}