using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*===============================================================
Project:	MSD - Core
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:		29/01/2020 21:18
===============================================================*/

namespace MSD
{
	public abstract class RuntimeStack<T> : ScriptableObject,
		IStack<T> 
	{
		protected Stack<T> items = new Stack<T>();

		public int Count => items.Count;

		bool ICollection.IsSynchronized => (items as ICollection).IsSynchronized;

		object ICollection.SyncRoot => (items as ICollection).SyncRoot;

		public void Construct()
		{
			items = new Stack<T>();
		}

		public void Construct(int count)
		{
			items = new Stack<T>(count);
		}

		public void Construct(IEnumerable<T> collection)
		{
			items = new Stack<T>(collection);
		}

		public void Push(T item)
		{
			items.Push(item);
		}

		public T Pop()
		{
			return items.Pop();
		}

		public T Peek()
		{
			return items.Peek();
		}

		public void TrimExcess()
		{
			items.TrimExcess();
		}

		public bool Contains(T item)
		{
			return items.Contains(item);
		}

		public void Clear()
		{
			items.Clear();
		}

		public void CopyTo(T[] array, int idx)
		{
			items.CopyTo(array, idx);
		}

		public T[] ToArray()
		{
			return items.ToArray();
		}

		public IEnumerator<T> GetEnumerator()
		{
			return items.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return items.GetEnumerator();
		}

		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return items.GetEnumerator();
		}

		void ICollection.CopyTo(Array array, int index)
		{
			(items as ICollection).CopyTo(array, index);
		}
	}
}
