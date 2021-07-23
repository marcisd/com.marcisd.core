using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*===============================================================
Project:	MSD - Core
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:       15/11/2018 02:36
===============================================================*/

namespace MSD
{
	public abstract class GenericDictionary<TKey, TValue> : ScriptableObject,
		IDictionary<TKey, TValue>
	{
		[SerializeField]
		protected SerializableDictionary<TKey, TValue> _dictionary = new SerializableDictionary<TKey, TValue>();

		#region IDictionary

		public TValue this[TKey key] {
			get => _dictionary[key];
			set => _dictionary[key] = value;
		}

		public ICollection<TKey> Keys => _dictionary.Keys;

		public ICollection<TValue> Values => _dictionary.Values;

		public int Count => _dictionary.Count;

		bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly => ((IDictionary<TKey, TValue>)_dictionary).IsReadOnly;

		public void Add(TKey key, TValue value)
		{
			_dictionary.Add(key, value);
		}

		public bool ContainsKey(TKey key)
		{
			return _dictionary.ContainsKey(key);
		}

		public bool Remove(TKey key)
		{
			return _dictionary.Remove(key);
		}

		public bool TryGetValue(TKey key, out TValue value)
		{
			return _dictionary.TryGetValue(key, out value);
		}

		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return _dictionary.GetEnumerator();
		}

		public void Clear()
		{
			_dictionary.Clear();
		}

		void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
		{
			((IDictionary<TKey, TValue>)_dictionary).Add(item);
		}

		bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item)
		{
			return ((IDictionary<TKey, TValue>)_dictionary).Contains(item);
		}

		void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			((IDictionary<TKey, TValue>)_dictionary).CopyTo(array, arrayIndex);
		}

		bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
		{
			return ((IDictionary<TKey, TValue>)_dictionary).Remove(item);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return _dictionary.GetEnumerator();
		}

		#endregion
	}
}
