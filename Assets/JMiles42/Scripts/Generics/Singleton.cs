using UnityEngine;
using System;
using System.Linq;
using JMiles42.Components;
using JMiles42.ScriptableObjects;

namespace JMiles42.Generics
{
	[Serializable, DisallowMultipleComponent]
	public class Singleton<S>: JMilesBehavior where S: JMilesBehavior
	{
		protected static S instance;

		public static bool InstanceNull
		{
			get { return instance == null; }
		}

		public static S Instance
		{
			get
			{
				if (instance)
					return instance;
				instance = (S) FindObjectOfType(typeof (S));
				if (!instance)
					Debug.LogError(typeof (S) + " is needed in the scene."); //Print error
				return instance;
			}
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

#if UNITY_EDITOR

		void OnValidate()
		{
			var allComponents = gameObject.GetComponents<Component>();
			if (Instance != this)
			{
				Debug.LogWarning(string.Format("There is more then one ({0}) in the scene, this version is not Instance. So anychanges made to me, won't be relative to any code referance to Instance.",
											   typeof (S)));
				if (allComponents.Length > 2)
					return;
				gameObject.name = string.Format("THIS IS A DUPLICATE OF {0}", typeof (S));
				return;
			}
			if (allComponents.Length > 2)
				return;
			gameObject.name = typeof (S).ToString();
			transform.ResetPosRotScale();
		}

#endif
	}

	[Serializable, DisallowMultipleComponent]
	public class SingletonRigidbody<S>: JMilesRigidbodyBehavior where S: JMilesRigidbodyBehavior
	{
		protected static S instance;

		public static S Instance
		{
			get
			{
				if (instance)
					return instance;
				instance = (S) FindObjectOfType(typeof (S));
				if (!instance)
					Debug.LogError(typeof (S) + " is needed in the scene."); //Print error
				return instance;
			}
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

#if UNITY_EDITOR

		void OnValidate()
		{
			var allComponents = gameObject.GetComponents<Component>();
			if (Instance != this)
			{
				Debug.LogWarning(string.Format("There is more then one ({0}) in the scene, this version is not Instance. So anychanges made to me, won't be relative to any code referance to Instance.",
											   typeof (S)));
				if (allComponents.Length > 3)
					return;
				gameObject.name = string.Format("THIS IS A DUPLICATE OF {0}", typeof (S));
				return;
			}
			if (allComponents.Length > 3)
				return;
			gameObject.name = typeof (S).ToString();
			transform.ResetPosRotScale();
		}

#endif
	}

	public class SingletonScriptableObject<S>: ScriptableObject where S: ScriptableObject
	{
		public static bool InstanceNull
		{
			get { return instance == null; }
		}

		public static bool InstanceCheckNull
		{
			get { return Instance == null; }
		}
		protected static S instance;

		public static S Instance
		{
			get
			{
				if (instance)
					return instance;
				instance = Resources.FindObjectsOfTypeAll<S>().FirstOrDefault();
				if (!instance)
					Debug.LogError(typeof (S) + " is needed in the scene."); //Print error
				return instance;
			}
		}
	}

	public class SingletonBaseScriptableObject<S>: JMilesScriptableObject where S: JMilesScriptableObject
	{
		public static bool InstanceNull
		{
			get { return instance == null; }
		}
		protected static S instance;

		public static S Instance
		{
			get
			{
				if (instance)
					return instance;
				instance = Resources.FindObjectsOfTypeAll<S>().FirstOrDefault();
				if (!instance)
					Debug.LogError(typeof (S) + " is needed"); //Print error
				return instance;
			}
		}
	}

	public class Singleclass: Singleton<Singleclass>
	{}
}