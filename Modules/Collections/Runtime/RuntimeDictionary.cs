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
	public abstract class RuntimeDictionary<TKey, TValue> : ScriptableObject,
		IDictionary<TKey, TValue>
	{
		protected Dictionary<TKey, TValue> itemPair = new Dictionary<TKey, TValue>();

		public TValue this[TKey key] {
			get => itemPair[key];
			set => itemPair[key] = value;
		}

		public ICollection<TKey> Keys => itemPair.Keys;

		public ICollection<TValue> Values => itemPair.Values;

		public int Count => itemPair.Count;

		bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly => ((IDictionary<TKey, TValue>)itemPair).IsReadOnly;

		public void Add(TKey key, TValue value)
		{
			itemPair.Add(key, value);
		}

		public void Clear()
		{
			itemPair.Clear();
		}

		public bool ContainsKey(TKey key)
		{
			return itemPair.ContainsKey(key);
		}

		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return itemPair.GetEnumerator();
		}

		public bool Remove(TKey key)
		{
			return itemPair.Remove(key);
		}

		public bool TryGetValue(TKey key, out TValue value)
		{
			return itemPair.TryGetValue(key, out value);
		}

		void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
		{
			((IDictionary<TKey, TValue>)itemPair).Add(item);
		}

		bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item)
		{
			return ((IDictionary<TKey, TValue>)itemPair).Contains(item);
		}

		void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			((IDictionary<TKey, TValue>)itemPair).CopyTo(array, arrayIndex);
		}

		bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
		{
			return ((IDictionary<TKey, TValue>)itemPair).Remove(item);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IDictionary<TKey, TValue>)itemPair).GetEnumerator();
		}
	}
}