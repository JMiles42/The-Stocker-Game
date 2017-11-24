using System;
using UnityEngine;

public class Heap <T> where T: IHeapItems<T>
{
	private T[] items;
	private int CurrentItemCount;

	public Heap(int MaxSize)
	{
		items = new T[MaxSize];
	}

	public void Add(T item)
	{
		item.HeapIndex = CurrentItemCount;
		Debug.Log($"Heap Count = {CurrentItemCount}");
		items[CurrentItemCount] = item;
		SortUp(item);
		CurrentItemCount++;
	}

	public T RemoveFirst()
	{
		T FirstItem = items[0];
		CurrentItemCount--;
		items[0] = items[CurrentItemCount];
		items[0].HeapIndex = 0;
		SortDown(items[0]);
		return FirstItem;
	}

	public void UpdateItem(T item)
	{
		SortUp(item);
	}

	public int Count
	{
		get
		{
			return CurrentItemCount;
		}
	}

	public bool Contains(T item)
	{
		return Equals(items[item.HeapIndex], item);
	}

	public void SortUp(T item)
	{
		int ParentIndex = (item.HeapIndex - 1) / 2;
		while (true)
		{
			T ParentItem = items[ParentIndex];
			if (item.CompareTo(ParentItem) > 0)
			{
				Swap(item, ParentItem);
			}
			else
			{
				break;	
			}

			ParentIndex = (item.HeapIndex - 1) / 2;
		}
	}

	public void SortDown(T item)
	{
		while (true)
		{
			int ChildIndexLeft = item.HeapIndex * 2 + 1;
			int ChildIndexRight = item.HeapIndex * 2 + 2;
			int SwapIndex = 0;

			if (ChildIndexLeft< CurrentItemCount)
			{
				SwapIndex = ChildIndexLeft;
				if (ChildIndexRight < CurrentItemCount)
				{
					if (items[ChildIndexLeft].CompareTo(items[ChildIndexRight]) < 0)
					{
						SwapIndex = ChildIndexRight;
					}
				}

				if (item.CompareTo(items[SwapIndex]) < 0)
				{
					Swap(item, items[SwapIndex]);
				}
				else
				{
					return;
				}
			}
			else
			{
				return;
			}
		}
	}

	public void Swap(T ItemA, T ItemB)
	{
		items[ItemA.HeapIndex] = ItemB;
		items[ItemB.HeapIndex] = ItemA;
		int ItemAIndex = ItemA.HeapIndex;
		ItemA.HeapIndex = ItemB.HeapIndex;
		ItemB.HeapIndex = ItemAIndex;
	}

}

public interface IHeapItems<T>: IComparable<T>
{
	int HeapIndex
	{
		get;
		set;
	}
}