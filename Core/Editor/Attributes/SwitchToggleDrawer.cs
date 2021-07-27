using UnityEditor;
using UnityEngine;

/*===============================================================
Project:	MSD - Core
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:		20/09/2019 14:30
===============================================================*/

namespace MSD.Editor
{
	[CustomPropertyDrawer(typeof(SwitchToggleAttribute))]
	public class SwitchToggleDrawer : PropertyDrawer
	{
		private SwitchToggleAttribute Attribute => attribute as SwitchToggleAttribute;

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			if (property.propertyType == SerializedPropertyType.Boolean) {
				bool boolValue = property.boolValue;
				GUIContent optionTrue = new GUIContent(Attribute.optionTrueLabel);
				GUIContent optionFalse = new GUIContent(Attribute.optionFalseLabel);
				GUIContent labelOverride = string.IsNullOrWhiteSpace(Attribute.labelOverride) ? label : new GUIContent(Attribute.labelOverride);

				using (new EditorGUI.PropertyScope(position, labelOverride, property)) {

					position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Keyboard), labelOverride);

					float width1 = GUI.skin.button.CalcSize(optionTrue).x;
					float width2 = GUI.skin.button.CalcSize(optionFalse).x;

					position.width = width1 + width2;
					Rect positionTrue = new Rect(position) {
						width = width1
					};
					Rect positionFalse = new Rect(position) {
						x = position.x + width1,
						width = width2
					};

					using (new NoIndentScope()) {
						if (GUI.Button(position, GUIContent.none, Styles.backgroundStyle)) {
							property.boolValue = !boolValue;
						}

						GUIStyle optionTrueStyle = boolValue ? Styles.activeSelectionStyle : Styles.inactiveSelectionStyle;
						GUIStyle optionFalseStyle = !boolValue ? Styles.activeSelectionStyle : Styles.inactiveSelectionStyle;
						using (new EditorGUI.DisabledScope(!boolValue)) {
							EditorGUI.LabelField(positionTrue, optionTrue, optionTrueStyle);
						}
						using (new EditorGUI.DisabledScope(boolValue)) {
							EditorGUI.LabelField(positionFalse, optionFalse, optionFalseStyle);
						}
					}
				}
			} else {
				EditorGUI.HelpBox(position, "Use SwitchToggle with bool!", MessageType.Error);
			}
		}

		private static class Styles
		{
			public static readonly GUIStyle backgroundStyle = new GUIStyle("ShurikenEffectBg");
			public static readonly GUIStyle activeSelectionStyle = GUI.skin.button;
			public static readonly GUIStyle inactiveSelectionStyle = new GUIStyle(EditorStyles.label) {
				alignment = TextAnchor.MiddleCenter
			};
		}
	}
}
