using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*===============================================================
Project:	MSD - Core
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:       06/11/2018 16:03
===============================================================*/

namespace MSD
{
	public abstract class RuntimeSet<T> : ScriptableObject,
		IList<T>,
		IList
	{
		[SerializeField]
		protected List<T> items = new List<T>();

		#region IList<T>

		public T this[int index] {
			get => items[index];
			set => items[index] = value;
		}
		
		public int Count => items.Count;

		bool ICollection<T>.IsReadOnly => ((IList<T>)items).IsReadOnly;

		public void Add(T item)
		{
			items.Add(item);
		}

		public void AddRange(IEnumerable<T> collection)
		{
			items.AddRange(collection);
		}

		public void Clear()
		{
			items.Clear();
		}

		public bool Contains(T item)
		{
			return items.Contains(item);
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			items.CopyTo(array, arrayIndex);
		}

		public IEnumerator<T> GetEnumerator()
		{
			return items.GetEnumerator();
		}

		public int IndexOf(T item)
		{
			return items.IndexOf(item);
		}

		public void Insert(int index, T item)
		{
			items.Insert(index, item);
		}

		public bool Remove(T item)
		{
			return items.Remove(item);
		}

		public void RemoveAt(int index)
		{
			items.RemoveAt(index);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return items.GetEnumerator();
		}

		#endregion IList<T>

		#region IList

		object IList.this[int index] {
			get => (items as IList)[index];
			set => (items as IList)[index] = value;
		}

		bool IList.IsReadOnly => (items as IList).IsReadOnly;

		bool IList.IsFixedSize => (items as IList).IsFixedSize;

		bool ICollection.IsSynchronized => (items as ICollection).IsSynchronized;

		object ICollection.SyncRoot => (items as ICollection).SyncRoot;

		int IList.Add(object value)
		{
			return (items as IList).Add(value);
		}

		bool IList.Contains(object value)
		{
			return (items as IList).Contains(value);
		}

		void ICollection.CopyTo(Array array, int index)
		{
			(items as ICollection).CopyTo(array, index);
		}

		int IList.IndexOf(object value)
		{
			return (items as IList).IndexOf(value);
		}

		void IList.Insert(int index, object value)
		{
			(items as IList).Insert(index, value);
		}

		void IList.Remove(object value)
		{
			(items as IList).Remove(value);
		}

		#endregion IList

	}
}
