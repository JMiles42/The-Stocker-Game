using UnityEngine;
using System;
using System.Linq;
using JMiles42.Components;
using JMiles42.ScriptableObjects;

namespace JMiles42.Generics
{
	[Serializable, DisallowMultipleComponent]
	public class Singleton<S> : JMilesBehavior where S : JMilesBehavior
	{
		protected static S instance;

		public static bool instanceNull
		{
			get { return instance == null; }
		}

		public static S Instance
		{
			get
			{
				if (instance)
					return instance;
				instance = (S)FindObjectOfType(typeof(S));
				if (!instance)
					Debug.LogError(typeof(S) + " is needed in the scene."); //Print error
				return instance;
			}
		}

		public static bool InstanceNull
		{
			get { return Instance == null; }
		}

		public void DestroyOtherInstances()
		{
			var others = FindObjectsOfType<S>();
			if (others.Length == 1)
				return;
			for (var i = others.Length - 1; i >= 0; i--)
			{
				if (Instance == others[1])
					continue;
				Destroy(others[i]);
			}
		}
	}

	[Serializable, DisallowMultipleComponent]
	public class SingletonRigidbody<S> : JMilesRigidbodyBehavior where S : JMilesRigidbodyBehavior
	{
		protected static S instance;

		public static bool instanceNull
		{
			get { return instance == null; }
		}

		public static S Instance
		{
			get
			{
				if (instance)
					return instance;
				instance = (S)FindObjectOfType(typeof(S));
				if (!instance)
					Debug.LogError(typeof(S) + " is needed in the scene."); //Print error
				return instance;
			}
		}

		public static bool InstanceNull
		{
			get { return Instance == null; }
		}

		public void DestroyOtherInstances()
		{
			var others = FindObjectsOfType<S>();
			if (others.Length == 1)
				return;
			for (var i = others.Length - 1; i >= 0; i--)
			{
				if (Instance == others[1])
					continue;
				Destroy(others[i]);
			}
		}
	}

	public class SingletonScriptableObject<S> : ScriptableObject where S : ScriptableObject
	{
		protected static S instance;

		public static bool instanceNull
		{
			get { return instance == null; }
		}

		public static S Instance
		{
			get
			{
				if (instance)
					return instance;
				instance = Resources.FindObjectsOfTypeAll<S>().FirstOrDefault();
				if (!instance)
					Debug.LogError(typeof(S) + " is needed in the scene."); //Print error
				return instance;
			}
		}

		public static bool InstanceNull
		{
			get { return Instance == null; }
		}
	}

	public class SingletonBaseScriptableObject<S> : JMilesScriptableObject where S : JMilesScriptableObject
	{
		protected static S instance;

		public static bool instanceNull
		{
			get { return instance == null; }
		}

		public static S Instance
		{
			get
			{
				if (instance)
					return instance;
				instance = Resources.FindObjectsOfTypeAll<S>().FirstOrDefault();
				if (!instance)
					Debug.LogError(typeof(S) + " is needed"); //Print error
				return instance;
			}
		}

		public static bool InstanceNull
		{
			get { return Instance == null; }
		}
	}
}