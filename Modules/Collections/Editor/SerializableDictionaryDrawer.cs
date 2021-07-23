using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/*===============================================================
Project:	MSD - Core
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:       15/11/2018 12:49
===============================================================*/

namespace MSD.Editor
{
	[CustomPropertyDrawer(typeof(SerializableDictionaryBase), true)]
	public partial class SerializableDictionaryDrawer : PropertyDrawer
	{
		private static readonly float DEFAULT_KEY_WIDTH_RATIO = 0.5f;
		private static readonly float KEY_WIDTH_RATIO_MIN = 0.2f;
		private static readonly float KEY_WIDTH_RATIO_MAX = 0.8f;

		private SerializedObject _serializedObject;
		private SerializedProperty _dictProperty;
		private SerializedProperty _keysProperty;
		private SerializedProperty _valuesProperty;
		private SerializedProperty _keyTypeProp;
		private SerializedProperty _valueTypeProp;
		
		private HashSet<object> _keysCache = new HashSet<object>();

		private KeyDrawerStrategy _keyDrawerStrategy;
		private TemporaryKeyStrategy _temporaryKeyStrategy;
		private ValueStrategy _valueStrategy;

		private float _footerHeightBackup;
		private bool _displayError;

		public float KeyWidthRatio => Mathf.Clamp(GetKeyWidthRatio(), KEY_WIDTH_RATIO_MIN, KEY_WIDTH_RATIO_MAX);

		private float ValueWidthRatio => 1f - KeyWidthRatio;

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			if (_displayError) {
				DisplayKeyErrorDialog();
				_displayError = false;
			}

			Init(property);
			_reorderableList.DoList(position);
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			Init(property);
			return _reorderableList.GetHeight();
		}

		private Type GetKeyType()
		{
			if (_keyTypeProp == null) {
				throw new InvalidOperationException("Property not initialized!");
			}
			return Type.GetType(_keyTypeProp.stringValue);
		}

		private Type GetValueType()
		{
			if (_valueTypeProp == null) {
				throw new InvalidOperationException("Property not initialized!");
			}
			return Type.GetType(_valueTypeProp.stringValue);
		}

		private float GetKeyWidthRatio()
		{
			if (Attribute.GetCustomAttribute(fieldInfo.FieldType, typeof(SerializableDictionaryKeyWidthRatioAttribute))
				is SerializableDictionaryKeyWidthRatioAttribute attribute) {
				return attribute.KeyWidthRatio;
			} else {
				return DEFAULT_KEY_WIDTH_RATIO;
			}
		}

		private void Init(SerializedProperty property)
		{
			InitRelativeProperties(property);
			InitStrategies();
			InitReorderableList();
			CacheKeys();
		}

		private void InitRelativeProperties(SerializedProperty property)
		{
			_serializedObject = property.serializedObject;

			_dictProperty = property;

			_keyTypeProp = property.FindPropertyRelative("_keyType");
			_valueTypeProp = property.FindPropertyRelative("_valueType");

			_keysProperty = property.FindPropertyRelative("_keys");
			_valuesProperty = property.FindPropertyRelative("_values");
		}

		private void InitStrategies()
		{
			if (_keyDrawerStrategy == null) {
				_keyDrawerStrategy = KeyDrawerStrategy.Factory.Get(GetKeyType());
			}

			if (_temporaryKeyStrategy == null) {
				_temporaryKeyStrategy = TemporaryKeyStrategy.Factory.Get(GetKeyType());
			}

			if (_valueStrategy == null) {
				_valueStrategy = ValueStrategy.Factory.Get(GetValueType());
			}
		}

		private void CacheKeys()
		{
			_keysCache = new HashSet<object>();

			for (int i = 0; i < _keysProperty.arraySize; i++) {
				_keysCache.Add(_keyDrawerStrategy.GetPropertyValue(_keysProperty.GetArrayElementAtIndex(i)));
			}
		}

		private bool ContainsKey(object key)
		{
			return _keysCache.Contains(key);
		}

		private void AddNewDictionaryItem()
		{
			if (_temporaryKeyStrategy.Value != null) {
				if (ContainsKey(_temporaryKeyStrategy.Value)) {
					DisplayKeyErrorDialog();
					return;
				}

				AddTemporaryKey();
				AddDefaultValue();

				_serializedObject.ApplyModifiedProperties();
				CacheKeys();
			} else {
				DisplayKeyErrorDialog();
			}
		}

		private void AddTemporaryKey()
		{
			_keysProperty.arraySize += 1;
			int index = _keysProperty.arraySize - 1;
			SerializedProperty sp = _keysProperty.GetArrayElementAtIndex(index);

			_temporaryKeyStrategy.Set(sp);
		}

		private void AddDefaultValue()
		{
			_valuesProperty.arraySize += 1;
			int index = _valuesProperty.arraySize - 1;
			SerializedProperty sp = _valuesProperty.GetArrayElementAtIndex(index);

			_valueStrategy.SetDefault(sp);
		}

		private void DisplayKeyErrorDialog()
		{
			EditorUtility.DisplayDialog("Dictionary Error", "Key already exists in the dictionary!", "Ok");
		}
	}
}
