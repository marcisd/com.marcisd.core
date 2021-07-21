using System;
using UnityEditor;
using UnityEngine;

/*===============================================================
Project:	MSD - Core
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:		29/11/2019 21:13
===============================================================*/

namespace MSD.Editor
{
	public partial class GenericMenuBar
	{
		public enum LabelType
		{
			Regular,
			Bold,
		}

		public enum ButtonType
		{
			Button,
			Dropdown,
			Popup,
		}

		public enum InputFieldType
		{
			TextField,
			SearchField,
		}

		private abstract class Element
		{
			public Func<bool> isDisabledCallback = () => false;

			public void Draw()
			{
				using (new EditorGUI.DisabledScope(isDisabledCallback.Invoke())) {
					DoDraw();
				}
			}

			protected abstract void DoDraw();
		}

		private class SpaceElement : Element
		{
			protected override void DoDraw()
			{
				EditorGUILayout.Space();
			}
		}

		private class FlexibleSpaceElement : Element
		{
			protected override void DoDraw()
			{
				GUILayout.FlexibleSpace();
			}
		}

		private class LabelElement : Element
		{
			public GUIContent content;

			protected virtual GUIStyle style => EditorStyles.miniLabel;

			protected GUILayoutOption[] options = { GUILayout.ExpandWidth(false) };

			protected override void DoDraw()
			{
				EditorGUILayout.LabelField(content, style, options);
			}
		}

		private sealed class BoldLabelElement : LabelElement
		{
			protected override GUIStyle style => EditorStyles.miniBoldLabel;
		}

		private class ButtonElement : LabelElement
		{
			public Action OnClick;

			protected override GUIStyle style => EditorStyles.toolbarButton;

			protected override void DoDraw()
			{
				if (GUILayout.Button(content, style, options)) {
					OnClick?.Invoke();
				}
			}
		}

		private sealed class DropdownElement : ButtonElement
		{
			protected override GUIStyle style => EditorStyles.toolbarDropDown;
		}

		private sealed class PopupElement : ButtonElement
		{
			protected override GUIStyle style => EditorStyles.toolbarPopup;
		}

		private class TextFieldElement : LabelElement
		{
			public string value;

			public Action<string> OnValueChanged;

			protected override GUIStyle style => EditorStyles.toolbarTextField;

			protected override void DoDraw()
			{
				string text = EditorGUILayout.TextField(content, value, style, options);
				if (text != value) {
					value = text;
					OnValueChanged?.Invoke(text);
				}
			}
		}

		private sealed class SearchFieldElement : TextFieldElement
		{
			protected override GUIStyle style => EditorStyles.toolbarSearchField;
		}

		private class ToggleElement : LabelElement
		{
			public bool value;
			public GUIContent offContent = null;

			public Action<bool> OnValueChanged;

			protected override GUIStyle style => EditorStyles.toolbarButton;

			private GUIContent toggleContent => value ? content : offContent ?? content;

			protected override void DoDraw()
			{
				bool isOn = GUILayout.Toggle(value, toggleContent, style, options);
				if (isOn != value) {
					value = isOn;
					OnValueChanged?.Invoke(isOn);
				}
			}
		}
	}
}
