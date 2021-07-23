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
	public abstract class GenericList<T> : ScriptableObject,
		IList<T>
	{
		[SerializeField]
		protected List<T> items;

		#region IList

		public T this[int index] {
			get { return items[index]; }
			set {
				items[index] = value;
			}
		}

		public int Count {
			get {
				return items.Count;
			}
		}

		public bool IsReadOnly {
			get {
				return ((IList<T>)items).IsReadOnly;
			}
		}

		public void Add(T item)
		{
			items.Add(item);
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

		#endregion IList
	}
}
