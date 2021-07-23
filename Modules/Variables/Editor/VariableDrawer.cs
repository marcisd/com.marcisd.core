using UnityEditor;
using UnityEngine;

/*===============================================================
Project:	MSD - Core
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:		
===============================================================*/

namespace MSD.Editor
{
	[CustomPropertyDrawer(typeof(VariableBase), true)]
	public class VariableDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			SerializedProperty initialValueProp = property.FindPropertyRelative("_initialValue");

			Rect propertyRect = new Rect(position) {
				height = EditorGUI.GetPropertyHeight(initialValueProp),
			};

			Rect labelRect = new Rect(position) {
				y = propertyRect.yMax + EditorGUIUtility.standardVerticalSpacing,
				height = EditorGUIUtility.singleLineHeight,
			};

			Rect selectableLabelRect = new Rect(position) {
				x = propertyRect.xMin + EditorGUIUtility.labelWidth + EditorGUIUtility.standardVerticalSpacing,
				y = propertyRect.yMax + EditorGUIUtility.standardVerticalSpacing,
				width = propertyRect.width - EditorGUIUtility.labelWidth,
				height = EditorGUIUtility.singleLineHeight,
			};

			EditorGUI.PropertyField(propertyRect, initialValueProp);

			VariableBase obj = (VariableBase)property.GetObjectInstance();
			EditorGUI.LabelField(labelRect, "Runtime Value");
			EditorGUI.SelectableLabel(selectableLabelRect, obj.GetRuntimeValue().ToString());
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			SerializedProperty initialValueProp = property.FindPropertyRelative("_initialValue");

			float height = EditorGUI.GetPropertyHeight(initialValueProp);
			height += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

			return height;
		}
	}
}
