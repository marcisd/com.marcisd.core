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
			public readonly GUIContent content;

			protected GUILayoutOption[] options = { GUILayout.ExpandWidth(false) };

			protected virtual GUIStyle Style => EditorStyles.miniLabel;

			public LabelElement(GUIContent content)
			{
				this.content = content;
			}

			protected override void DoDraw()
			{
				EditorGUILayout.LabelField(content, Style, options);
			}
		}

		private sealed class BoldLabelElement : LabelElement
		{
			protected override GUIStyle Style => EditorStyles.miniBoldLabel;

			public BoldLabelElement(GUIContent content) : base(content) { }
		}

		private class ButtonElement : LabelElement
		{
			public Action OnClick = delegate { };

			protected override GUIStyle Style => EditorStyles.toolbarButton;

			public ButtonElement(GUIContent content) : base(content) { }

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

			public DropdownElement(GUIContent content) : base(content) { }
		}

		private sealed class PopupElement : ButtonElement
		{
			protected override GUIStyle Style => EditorStyles.toolbarPopup;

			public PopupElement(GUIContent content) : base(content) { }
		}

		private class TextFieldElement : LabelElement
		{
			public Action<string> OnValueChanged = delegate { };

			public string Value { get; private set; }

			protected override GUIStyle Style => EditorStyles.toolbarTextField;

			public TextFieldElement(GUIContent content, string text) : base(content)
			{
				Value = text;
			}

			protected override void DoDraw()
			{
				string text = EditorGUILayout.TextField(content, Value, Style, options);
				if (text != Value) {
					Value = text;
					OnValueChanged?.Invoke(text);
				}
			}
		}

		private sealed class SearchFieldElement : TextFieldElement
		{
			protected override GUIStyle Style => EditorStyles.toolbarSearchField;

			public SearchFieldElement(GUIContent content, string text) : base(content, text) { }
		}

		private class ToggleElement : LabelElement
		{
			public readonly GUIContent offContent;

			public Action<bool> OnValueChanged = delegate { };

			public bool Value { get; private set; }

			protected override GUIStyle Style => EditorStyles.toolbarButton;

			private GUIContent ToggleContent => Value ? content : offContent ?? content;

			public ToggleElement(GUIContent content, bool isOn, GUIContent offContent) : base(content)
			{
				Value = isOn;
				this.offContent = offContent;
			}

			protected override void DoDraw()
			{
				bool isOn = GUILayout.Toggle(Value, ToggleContent, Style, options);
				if (isOn != Value) {
					Value = isOn;
					OnValueChanged?.Invoke(isOn);
				}
			}
		}
	}
}
