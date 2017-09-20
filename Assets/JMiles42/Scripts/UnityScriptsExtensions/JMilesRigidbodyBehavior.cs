using UnityEngine;

namespace JMiles42.Components
{
	[RequireComponent(typeof (Rigidbody))]
	public class JMilesRigidbodyBehavior: JMilesBehavior
	{
		private Rigidbody m_Rigidbody;

		public Rigidbody Rigidbody
		{
			get { return m_Rigidbody ?? (m_Rigidbody = GetComponent<Rigidbody>()); }
			set { m_Rigidbody = value; }
		}

		public Vector3 Velocity
		{
			get { return Rigidbody.velocity; }
			set { Rigidbody.velocity = value; }
		}

		public Vector3 AngularVelocity
		{
			get { return Rigidbody.angularVelocity; }
			set { Rigidbody.angularVelocity = value; }
		}

		public float Drag
		{
			get { return Rigidbody.drag; }
			set { Rigidbody.drag = value; }
		}

		public float AngularDrag
		{
			get { return Rigidbody.angularDrag; }
			set { Rigidbody.angularDrag = value; }
		}

		public float Mass
		{
			get { return Rigidbody.mass; }
			set { Rigidbody.mass = value; }
		}

		public Vector3 CenterOfMass
		{
			get { return Rigidbody.centerOfMass; }
			set { Rigidbody.centerOfMass = value; }
		}
	}

	[RequireComponent(typeof (Rigidbody2D))]
	public class JMiles2DRigidbodyBehavior: JMilesBehavior
	{
		private Rigidbody2D m_Rigidbody2D;

		public Rigidbody2D Rigidbody2D
		{
			get { return m_Rigidbody2D ?? (m_Rigidbody2D = GetComponent<Rigidbody2D>()); }
			set { m_Rigidbody2D = value; }
		}

		public Vector3 Velocity
		{
			get { return Rigidbody2D.velocity; }
			set { Rigidbody2D.velocity = value; }
		}

		public float AngularVelocity
		{
			get { return Rigidbody2D.angularVelocity; }
			set { Rigidbody2D.angularVelocity = value; }
		}

		public float Drag
		{
			get { return Rigidbody2D.drag; }
			set { Rigidbody2D.drag = value; }
		}

		public float AngularDrag
		{
			get { return Rigidbody2D.angularDrag; }
			set { Rigidbody2D.angularDrag = value; }
		}

		public float Mass
		{
			get { return Rigidbody2D.mass; }
			set { Rigidbody2D.mass = value; }
		}

		public Vector3 CenterOfMass
		{
			get { return Rigidbody2D.centerOfMass; }
			set { Rigidbody2D.centerOfMass = value; }
		}
	}
}