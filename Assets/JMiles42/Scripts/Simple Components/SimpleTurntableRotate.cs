using JMiles42.UnityInterfaces;
using UnityEngine;

namespace JMiles42.Components
{
	public class SimpleTurntableRotate: JMilesBehavior, IUpdate
	{
		public Vector3 rotateAngle;
		public Transform transformToMove;
		public Space transformSpace;

		private void Start()
		{
			if (!transformToMove)
				transformToMove = GetComponent<Transform>();
		}

		public void Update() { transformToMove.Rotate(rotateAngle * Time.deltaTime, transformSpace); }
	}
}