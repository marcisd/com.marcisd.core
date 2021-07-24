using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*===============================================================
Project:	Core Library
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:       15/11/2018 02:36
===============================================================*/

namespace MSD
{
	[Serializable]
	public class SerializableDictionary<TKey, TValue> : SerializableDictionaryBase,
		IDictionary<TKey, TValue>,
		IReadOnlyDictionary<TKey, TValue>,
		ISerializationCallbackReceiver
	{
		public SerializableDictionary() : base(typeof(TKey), typeof(TValue))
		{
			_genericDictionary = new Dictionary<TKey, TValue>();
		}

		[SerializeField]
		private List<TKey> _keys = new List<TKey>();

		[SerializeField]
		private List<TValue> _values = new List<TValue>();

		private Dictionary<TKey, TValue> _genericDictionary;

		#region IDictionary

		public TValue this[TKey key] {
			get => _genericDictionary[key];
			set => _genericDictionary[key] = value;
		}

		public ICollection<TKey> Keys => _genericDictionary.Keys;

		public ICollection<TValue> Values => _genericDictionary.Values;

		public int Count => _genericDictionary.Count;

		bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly => (_genericDictionary as IDictionary<TKey, TValue>).IsReadOnly;

		public void Add(TKey key, TValue value)
		{
			_genericDictionary.Add(key, value);
		}

		public bool ContainsKey(TKey key)
		{
			return _genericDictionary.ContainsKey(key);
		}

		public bool Remove(TKey key)
		{
			return _genericDictionary.Remove(key);
		}

		public bool TryGetValue(TKey key, out TValue value)
		{
			return _genericDictionary.TryGetValue(key, out value);
		}

		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return _genericDictionary.GetEnumerator();
		}

		public void Clear()
		{
			_genericDictionary.Clear();
		}

		void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
		{
			(_genericDictionary as IDictionary<TKey, TValue>).Add(item);
		}

		bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
		{
			return (_genericDictionary as IDictionary<TKey, TValue>).Remove(item);
		}

		bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item)
		{
			return (_genericDictionary as IDictionary<TKey, TValue>).Contains(item);
		}

		void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			(_genericDictionary as IDictionary<TKey, TValue>).CopyTo(array, arrayIndex);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return _genericDictionary.GetEnumerator();
		}

		#endregion IDictionary

		#region IReadOnlyDictionary

		IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys => (_genericDictionary as IReadOnlyDictionary<TKey, TValue>).Keys;

		IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values => (_genericDictionary as IReadOnlyDictionary<TKey, TValue>).Values;

		#endregion IReadOnlyDictionary

		#region ISerializationCallbackReceiver

		void ISerializationCallbackReceiver.OnBeforeSerialize()
		{
			_keys.Clear();
			_values.Clear();
			foreach (KeyValuePair<TKey, TValue> pair in this) {
				_keys.Add(pair.Key);
				_values.Add(pair.Value);
			}

			SetKeyValueType(typeof(TKey), typeof(TValue));
		}

		void ISerializationCallbackReceiver.OnAfterDeserialize()
		{
			Clear();

			if (_keys.Count != _values.Count) {
				throw new InvalidOperationException("The size of the keys and the values does not match!");
			}

			for (int i = 0; i < _keys.Count; i++) {
				Add(_keys[i], _values[i]);
			}
		}

		#endregion ISerializationCallbackReceiver
	}

	public abstract class SerializableDictionaryBase
	{
		[SerializeField]
		protected string _keyType;

		[SerializeField]
		protected string _valueType;

		protected SerializableDictionaryBase(Type keyType, Type valueType)
		{
			SetKeyValueType(keyType, valueType);
		}

		protected void SetKeyValueType(Type keyType, Type valueType)
		{
			_keyType = keyType.AssemblyQualifiedName;
			_valueType = valueType.AssemblyQualifiedName;
		}
	}
}
