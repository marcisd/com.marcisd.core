using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/*===============================================================
Project:	Core Library
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:       20/09/2019 19:08
===============================================================*/

namespace MSD.Editor
{
	using UObject = UnityEngine.Object;

	[CustomPropertyDrawer(typeof(SerializableInterface), true)]
	public class SerializableInterfaceDrawer : PropertyDrawer
	{
		public class SerializableInterfaceProperties
		{
			private static readonly string OBJECT_PROP_NAME = "_object";

			private readonly SerializedProperty _objectProp;
			private readonly Type _objectType;

			public UObject ObjectReference {
				get => _objectProp.objectReferenceValue;
				set => _objectProp.objectReferenceValue = value;
			}
			public Type ObjectType => _objectType;

			public SerializableInterfaceProperties(PropertyDrawer propertyDrawer, SerializedProperty property)
			{
				_objectProp = property.FindPropertyRelative(OBJECT_PROP_NAME);
				_objectType = GetPropertyType(propertyDrawer).BaseType.GetGenericArguments()[0];
			}

			private Type GetPropertyType(PropertyDrawer propertyDrawer)
			{
				Type fieldType = propertyDrawer.fieldInfo.FieldType;
				if (fieldType.IsArray) {
					return fieldType.GetElementType();
				} else if (fieldType.IsGenericType &&
						  fieldType.GetGenericTypeDefinition() == typeof(List<>)) {
					return fieldType.GetGenericArguments()[0];
				} else {
					return fieldType;
				}
			}
		}

		private void DrawProperty(Rect position, SerializedProperty property, GUIContent label)
		{
			SerializableInterfaceProperties properties = new SerializableInterfaceProperties(this, property);

			using (new EditorGUI.PropertyScope(position, label, property)) {
				UObject obj = EditorGUI.ObjectField(position,
					label.text != string.Empty ? $"{property.displayName}.{properties.ObjectType.Name}" : label.text,
					properties.ObjectReference,
					typeof(UObject),
					true);

				dynamic desiredObj = null;
				if (obj != null) {
					// Try ScriptableObject types
					if (desiredObj == null) {
						Type objType = obj.GetType();
						bool isCanCastObj = properties.ObjectType.IsAssignableFrom(objType);
						if (isCanCastObj) { desiredObj = obj; }
					}

					// Try GameObject or Component types
					if (desiredObj == null) {
						GameObject go = obj as GameObject;
						if (go == null) {
							Component comp = obj as Component;
							if (comp != null) { go = comp.gameObject; }
						}

						if (go != null) { desiredObj = go.GetComponent(properties.ObjectType); }
					}

					if (desiredObj == null) {
						EditorUtility.DisplayDialog("SerializableInterface Error", $"Not a valid {properties.ObjectType.Name}", "OK");
					}
				}

				// Assign to the field
				if (desiredObj != null) {
					properties.ObjectReference = desiredObj;
				}
			}
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			return EditorGUIUtility.singleLineHeight;
		}

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			DrawProperty(position, property, label);
		}
	}
}

