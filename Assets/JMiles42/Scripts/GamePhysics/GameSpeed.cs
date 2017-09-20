namespace JMiles42.Physics.GameSpeed
{
	public class GameSpeed
	{
		public enum SetSpeeds
		{
			PAUSED,
			HALFSPEED,
			ONExSPEED,
			TWOxSPEED,
			THREExSPEED,
			FOURxSPEED,
			FIVExTIMES,
			TENxSPEED,
			TWENTYxSPEED
		}

		#region Consts
		public const float PAUSED = 0f;
		public const float HALFSPEED = 0.5f;
		public const float ONExSPEED = 1f;
		public const float TWOxSPEED = 2f;
		public const float THREExSPEED = 3f;
		public const float FOURxSPEED = 4f;
		public const float FIVExTIMES = 5f;
		public const float TENxSPEED = 10f;
		public const float TWENTYxSPEED = 20f;
		#endregion

		public float currentGameSpeed = 1f;

		public void ResetGameSpeed() { currentGameSpeed = ONExSPEED; }

		public void SetGameSpeed(float newSpeed = 1f) { currentGameSpeed = newSpeed; }

		public void SetGameSpeed(int newSpeed = 1) { currentGameSpeed = newSpeed; }

		public void SetGameSpeed(SetSpeeds newSpeed = SetSpeeds.ONExSPEED)
		{
			switch (newSpeed)
			{
				case SetSpeeds.PAUSED:
					currentGameSpeed = PAUSED;
					break;
				case SetSpeeds.HALFSPEED:
					currentGameSpeed = HALFSPEED;
					break;
				case SetSpeeds.ONExSPEED:
					currentGameSpeed = ONExSPEED;
					break;
				case SetSpeeds.TWOxSPEED:
					currentGameSpeed = TWOxSPEED;
					break;
				case SetSpeeds.THREExSPEED:
					currentGameSpeed = THREExSPEED;
					break;
				case SetSpeeds.FOURxSPEED:
					currentGameSpeed = FOURxSPEED;
					break;
				case SetSpeeds.FIVExTIMES:
					currentGameSpeed = FIVExTIMES;
					break;
				case SetSpeeds.TENxSPEED:
					currentGameSpeed = TENxSPEED;
					break;
				case SetSpeeds.TWENTYxSPEED:
					currentGameSpeed = TWENTYxSPEED;
					break;
			}
		}
	}
}