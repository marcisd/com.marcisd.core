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
			public GUIContent content = GUIContent.none;

			protected GUILayoutOption[] options = { GUILayout.ExpandWidth(false) };

			protected virtual GUIStyle Style => EditorStyles.miniLabel;

			protected override void DoDraw()
			{
				EditorGUILayout.LabelField(content, Style, options);
			}
		}

		private sealed class BoldLabelElement : LabelElement
		{
			protected override GUIStyle Style => EditorStyles.miniBoldLabel;
		}

		private class ButtonElement : LabelElement
		{
			public Action OnClick = delegate { };

			protected override GUIStyle Style => EditorStyles.toolbarButton;

			protected override void DoDraw()
			{
				if (GUILayout.Button(content, Style, options)) {
					OnClick?.Invoke();
				}
			}
		}

		private sealed class DropdownElement : ButtonElement
		{
			protected override GUIStyle Style => EditorStyles.toolbarDropDown;
		}

		private sealed class PopupElement : ButtonElement
		{
			protected override GUIStyle Style => EditorStyles.toolbarPopup;
		}

		private class TextFieldElement : LabelElement
		{
			public string value;

			public Action<string> OnValueChanged = delegate { };

			protected override GUIStyle Style => EditorStyles.toolbarTextField;

			protected override void DoDraw()
			{
				string text = EditorGUILayout.TextField(content, value, Style, options);
				if (text != value) {
					value = text;
					OnValueChanged?.Invoke(text);
				}
			}
		}

		private sealed class SearchFieldElement : TextFieldElement
		{
			protected override GUIStyle Style => EditorStyles.toolbarSearchField;
		}

		private class ToggleElement : LabelElement
		{
			public bool value;
			public GUIContent offContent;

			public Action<bool> OnValueChanged;

			protected override GUIStyle Style => EditorStyles.toolbarButton;

			private GUIContent ToggleContent => value ? content : offContent ?? content;

			protected override void DoDraw()
			{
				bool isOn = GUILayout.Toggle(value, ToggleContent, Style, options);
				if (isOn != value) {
					value = isOn;
					OnValueChanged?.Invoke(isOn);
				}
			}
		}
	}
}
