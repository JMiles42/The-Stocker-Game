using System.Collections.Generic;
using JMiles42.Attributes;
using JMiles42.Components;
using UnityEngine;

namespace JMiles42.Systems.Pool
{
	[System.Serializable]
	public abstract class GenericPoolSystemComponent<T, P>: JMilesBehavior where P: PoolSystemArguments, new()
	{
#if UNITY_EDITOR
		[SerializeField] private string PoolName;
#endif

		public int PoolTotalLength;
		public List<T> PoolObjects;
		[SerializeField, DisableEditing] private int currentIndex = 0;

		public bool PoolEmpty
		{
			get { return PoolTotalLength == 0 || PoolObjects.Count == 0; }
		}

		public P nextSpawnArgs = new P();

		public void InitPool(int poolTotalLength, bool initAllAtStart = false)
		{
			PoolTotalLength = poolTotalLength;
			CheckIfPoolIsNull();
			if (initAllAtStart)
			{
				for (int i = 0; i < PoolTotalLength; i++)
				{
					PoolObjects.Add(GetANewPoolObject(nextSpawnArgs));
				}
			}
		}

		public void InitPool(int poolTotalLength, P[] args)
		{
			PoolTotalLength = poolTotalLength;
			CheckIfPoolIsNull();
			for (int i = 0; i < PoolTotalLength; i++)
			{
				if (args.Length < i)
					PoolObjects.Add(GetANewPoolObject(args[i]));
				else
				{
					PoolObjects.Add(GetANewPoolObject(nextSpawnArgs));
				}
			}
		}

		public void InitObjects(int amountToInit = 1)
		{
			if (amountToInit >= PoolTotalLength)
				PoolTotalLength = amountToInit;
			CheckIfPoolIsNull();
		}

		private void CheckIfPoolIsNull()
		{
			if (PoolObjects == null)
			{
				PoolObjects = new List<T>(PoolTotalLength);
			}
		}

		public bool CheckIfAllObjectsUnUsable()
		{
			foreach (var obj in PoolObjects)
			{
				if (IsObjectUseable(obj))
				{
					return false;
				}
			}
			return true;
		}

		public T NextInPool()
		{
			CheckIfPoolIsNull();
			if (currentIndex > PoolObjects.Count)
			{
				bool allEnabled = false;
				foreach (var obj in PoolObjects)
				{
					if (IsObjectUseable(obj))
					{
						allEnabled = false;
						break;
					}
					allEnabled = true;
				}
				if (allEnabled)
				{
					return AddNewObjectToPool();
				}
			}
			return GetCurrentObject();
		}

		private T GetCurrentObject()
		{
			int num = currentIndex;
			currentIndex++;
			if (currentIndex >= PoolObjects.Count)
				currentIndex = 0;
			return PoolObjects[num];
		}

		private T AddNewObjectToPool()
		{
			if (PoolObjects.Count < PoolTotalLength)
			{
				var t = GetANewPoolObject(nextSpawnArgs);
				PoolObjects.Add(t);
				return t;
			}
			return GetCurrentObject();
		}

		public abstract T GetANewPoolObject(P args);

		public abstract bool IsObjectUseable(T obj);
	}

	[System.Serializable]
	public class PoolSystemArguments
	{
		public PoolSystemArguments() {}
	}
}