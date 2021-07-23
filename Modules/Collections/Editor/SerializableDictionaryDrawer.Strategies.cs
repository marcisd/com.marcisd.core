using System;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

/*===============================================================
Project:	MSD - Core
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:		30/04/2019 23:18
===============================================================*/

namespace MSD.Editor
{
	public partial class SerializableDictionaryDrawer
	{
		#region KeyDrawerStrategy

		public abstract class KeyDrawerStrategy
		{
			public abstract object GetPropertyValue(SerializedProperty serializedProperty);

			public abstract void DrawProperty(Rect rect, SerializedProperty serializedProperty);

			public static class Factory
			{
				public static KeyDrawerStrategy Get(Type type)
				{
					if (type.IsEnum) {
						return new EnumKeyDrawerStrategy();
					}
					if (type == typeof(int)) {
						return new IntKeyDrawerStrategy();
					}
					if (type == typeof(float)) {
						return new FloatKeyDrawerStrategy();
					}
					if (type == typeof(double)) {
						return new DoubleKeyDrawerStrategy();
					}
					if (type == typeof(string)) {
						return new StringKeyDrawerStrategy();
					}
					if (type.IsSubclassOf(typeof(Object))) {
						return new ObjectKeyDrawerStrategy();
					}

					return null;
				}
			}
		}

		public class EnumKeyDrawerStrategy : KeyDrawerStrategy
		{
			public override void DrawProperty(Rect rect, SerializedProperty serializedProperty)
			{
				EditorGUI.PropertyField(rect, serializedProperty, GUIContent.none);
			}

			public override object GetPropertyValue(SerializedProperty serializedProperty)
			{
				return serializedProperty.enumValueIndex;
			}
		}

		public class IntKeyDrawerStrategy : KeyDrawerStrategy
		{
			public override void DrawProperty(Rect rect, SerializedProperty serializedProperty)
			{
				EditorGUI.DelayedIntField(rect, serializedProperty, GUIContent.none);
			}

			public override object GetPropertyValue(SerializedProperty serializedProperty)
			{
				return serializedProperty.intValue;
			}
		}

		public class FloatKeyDrawerStrategy : KeyDrawerStrategy
		{
			public override void DrawProperty(Rect rect, SerializedProperty serializedProperty)
			{
				EditorGUI.DelayedFloatField(rect, serializedProperty, GUIContent.none);
			}

			public override object GetPropertyValue(SerializedProperty serializedProperty)
			{
				return serializedProperty.floatValue;
			}
		}

		public class DoubleKeyDrawerStrategy : KeyDrawerStrategy
		{
			public override void DrawProperty(Rect rect, SerializedProperty serializedProperty)
			{
				// for some reason there is no DelayedDoubleField that takes in a SerializedProperty
				serializedProperty.doubleValue = EditorGUI.DelayedDoubleField(rect, GUIContent.none, serializedProperty.doubleValue);
			}

			public override object GetPropertyValue(SerializedProperty serializedProperty)
			{
				return serializedProperty.doubleValue;
			}
		}

		public class StringKeyDrawerStrategy : KeyDrawerStrategy
		{
			public override void DrawProperty(Rect rect, SerializedProperty serializedProperty)
			{
				EditorGUI.DelayedTextField(rect, serializedProperty, GUIContent.none);
			}

			public override object GetPropertyValue(SerializedProperty serializedProperty)
			{
				return serializedProperty.stringValue;
			}
		}

		public class ObjectKeyDrawerStrategy : KeyDrawerStrategy
		{
			public override void DrawProperty(Rect rect, SerializedProperty serializedProperty)
			{
				EditorGUI.PropertyField(rect, serializedProperty, GUIContent.none);
			}

			public override object GetPropertyValue(SerializedProperty serializedProperty)
			{
				return serializedProperty.objectReferenceValue;
			}
		}

		#endregion KeyDrawerStrategy

		#region TemporaryKeyStrategy

		public abstract class TemporaryKeyStrategy
		{
			protected object _temporaryKey;

			public object Value => _temporaryKey;

			public abstract void DrawField(Rect rect);

			public abstract void Set(SerializedProperty serializedProperty);

			public void Reset()
			{
				_temporaryKey = null;
			}

			public static class Factory
			{
				public static TemporaryKeyStrategy Get(Type type)
				{
					if (type.IsEnum) {
						return new EnumTemporaryKeyStrategy(type);
					}
					if (type == typeof(int)) {
						return new IntTemporaryKeyStrategy();
					}
					if (type == typeof(float)) {
						return new FloatTemporaryKeyStrategy();
					}
					if (type == typeof(double)) {
						return new DoubleTemporaryKeyStrategy();
					}
					if (type == typeof(string)) {
						return new StringTemporaryKeyStrategy();
					}
					if (type.IsSubclassOf(typeof(Object))) {
						return new ObjectTemporaryKeyStrategy(type);
					}

					return null;
				}
			}
		}

		public abstract class TemporaryKeyStrategyWithType : TemporaryKeyStrategy
		{
			protected Type _type;

			protected TemporaryKeyStrategyWithType(Type type)
			{
				_type = type;
			}
		}

		public class EnumTemporaryKeyStrategy : TemporaryKeyStrategyWithType
		{
			public EnumTemporaryKeyStrategy(Type type) : base(type) { }

			private Enum temporaryKeyAsEnum {
				get {
					if (_temporaryKey == null) {
						_temporaryKey = default(int);
					}
					return (Enum)Enum.GetValues(_type).GetValue((int)_temporaryKey);
				}
				set {
					_temporaryKey = Array.IndexOf(Enum.GetValues(_type), value);
				}
			}

			private int temporaryKeyAsEnumValueIndex {
				get {
					return (int)_temporaryKey;
				}
			}

			public override void DrawField(Rect rect)
			{
				temporaryKeyAsEnum = EditorGUI.EnumPopup(rect, temporaryKeyAsEnum);
			}

			public override void Set(SerializedProperty serializedProperty)
			{
				serializedProperty.enumValueIndex = temporaryKeyAsEnumValueIndex;
			}
		}

		public class IntTemporaryKeyStrategy : TemporaryKeyStrategy
		{
			private int temporaryKeyAsInt {
				get {
					if (_temporaryKey == null) {
						_temporaryKey = default(int);
					}
					return (int)_temporaryKey;
				}
			}

			public override void DrawField(Rect rect)
			{
				_temporaryKey = EditorGUI.IntField(rect, temporaryKeyAsInt);
			}

			public override void Set(SerializedProperty serializedProperty)
			{
				serializedProperty.intValue = temporaryKeyAsInt;
			}
		}

		public class FloatTemporaryKeyStrategy : TemporaryKeyStrategy
		{
			private float temporaryKeyAsFloat {
				get {
					if (_temporaryKey == null) {
						_temporaryKey = default(float);
					}
					return (float)_temporaryKey;
				}
			}

			public override void DrawField(Rect rect)
			{
				_temporaryKey = EditorGUI.FloatField(rect, temporaryKeyAsFloat);
			}

			public override void Set(SerializedProperty serializedProperty)
			{
				serializedProperty.floatValue = temporaryKeyAsFloat;
			}
		}

		public class DoubleTemporaryKeyStrategy : TemporaryKeyStrategy
		{
			private double temporaryKeyAsDouble {
				get {
					if (_temporaryKey == null) {
						_temporaryKey = default(double);
					}
					return (double)_temporaryKey;
				}
			}

			public override void DrawField(Rect rect)
			{
				_temporaryKey = EditorGUI.DoubleField(rect, temporaryKeyAsDouble);
			}

			public override void Set(SerializedProperty serializedProperty)
			{
				serializedProperty.doubleValue = temporaryKeyAsDouble;
			}
		}

		public class StringTemporaryKeyStrategy : TemporaryKeyStrategy
		{
			private string temporaryKeyAsString {
				get {
					if (_temporaryKey == null) {
						_temporaryKey = default(string);
					}
					return (string)_temporaryKey;
				}
			}

			public override void DrawField(Rect rect)
			{
				_temporaryKey = EditorGUI.TextField(rect, temporaryKeyAsString);
			}

			public override void Set(SerializedProperty serializedProperty)
			{
				serializedProperty.stringValue = temporaryKeyAsString;
			}
		}

		public class ObjectTemporaryKeyStrategy : TemporaryKeyStrategyWithType
		{
			public ObjectTemporaryKeyStrategy(Type type) : base(type) { }

			public override void DrawField(Rect rect)
			{
				_temporaryKey = EditorGUI.ObjectField(rect, (Object)_temporaryKey, _type, false);
			}

			public override void Set(SerializedProperty serializedProperty)
			{
				serializedProperty.objectReferenceValue = (Object)_temporaryKey;
			}
		}

		#endregion TemporaryKeyStrategy

		#region TemporaryKeyStrategy

		public abstract class ValueStrategy
		{
			public abstract void SetDefault(SerializedProperty serializedProperty);

			public static class Factory
			{
				public static ValueStrategy Get(Type type)
				{
					if (type.IsEnum) {
						return new EnumValueStrategy();
					}
					if (type == typeof(int)) {
						return new IntValueStrategy();
					}
					if (type == typeof(float)) {
						return new FloatValueStrategy();
					}
					if (type == typeof(double)) {
						return new DoubleValueStrategy();
					}
					if (type == typeof(string)) {
						return new StringValueStrategy();
					}
					if (type.IsSubclassOf(typeof(Object))) {
						return new ObjectValueStrategy();
					}
					if (type.IsClass) {
						return new ClassValueStrategy();
					}
					if (type.IsValueType) {
						return new ValueTypeValueStrategy();
					}

					return null;
				}
			}
		}

		public class EnumValueStrategy : ValueStrategy
		{
			public override void SetDefault(SerializedProperty serializedProperty)
			{
				serializedProperty.enumValueIndex = default;
			}
		}

		public class IntValueStrategy : ValueStrategy
		{
			public override void SetDefault(SerializedProperty serializedProperty)
			{
				serializedProperty.intValue = default;
			}
		}

		public class FloatValueStrategy : ValueStrategy
		{
			public override void SetDefault(SerializedProperty serializedProperty)
			{
				serializedProperty.floatValue = default;
			}
		}

		public class DoubleValueStrategy : ValueStrategy
		{
			public override void SetDefault(SerializedProperty serializedProperty)
			{
				serializedProperty.doubleValue = default;
			}
		}

		public class StringValueStrategy : ValueStrategy
		{
			public override void SetDefault(SerializedProperty serializedProperty)
			{
				serializedProperty.stringValue = default;
			}
		}

		public class ObjectValueStrategy : ValueStrategy
		{
			public override void SetDefault(SerializedProperty serializedProperty)
			{
				serializedProperty.objectReferenceValue = null;
			}
		}

		public class ClassValueStrategy : ValueStrategy
		{
			public override void SetDefault(SerializedProperty serializedProperty)
			{
				Debugger.Log("Custom Class: Ignore default value.");
			}
		}

		public class ValueTypeValueStrategy : ValueStrategy
		{
			public override void SetDefault(SerializedProperty serializedProperty)
			{
				Debugger.Log("Custom Struct: Ignore default value.");
			}
		}

		#endregion TemporaryKeyStrategy
	}
}
