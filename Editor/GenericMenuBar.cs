using System.Collections.Generic;
using UnityEngine;
using System;

/*===============================================================
Project:	MSD - Core
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:		29/11/2019 20:51
===============================================================*/

namespace MSD.Editor 
{
	public partial class GenericMenuBar 
	{
		private readonly List<Element> _elements;

		public GenericMenuBar()
		{
			_elements = new List<Element>();
		}

		public void AddSpace()
		{
			_elements.Add(new SpaceElement());
		}

		public void AddFlexibleSpace()
		{
			_elements.Add(new FlexibleSpaceElement());
		}

		public void AddLabel(string label)
		{
			AddLabel(new GUIContent(label), LabelType.Regular, null);
		}

		public void AddLabel(GUIContent content)
		{
			AddLabel(content, LabelType.Regular, null);
		}

		public void AddLabel(string label, LabelType type)
		{
			AddLabel(new GUIContent(label), type, null);
		}

		public void AddLabel(GUIContent content, LabelType type)
		{
			AddLabel(content, type, null);
		}

		public void AddLabel(string label, LabelType type, Func<bool> isDisabledCallback)
		{
			AddLabel(new GUIContent(label), type, isDisabledCallback);
		}

		public void AddLabel(GUIContent content, LabelType type, Func<bool> isDisabledCallback)
		{
			LabelElement label;

			switch (type) {
				case LabelType.Bold:
					label = new BoldLabelElement();
					break;
				default:
					label = new LabelElement();
					break;
			}

			label.content = content;
			if (isDisabledCallback != null) {
				label.isDisabledCallback = isDisabledCallback;
			}
			_elements.Add(label);
		}

		public void AddButton(string label, Action onClick)
		{
			AddButton(label, onClick, ButtonType.Button, null);
		}

		public void AddButton(GUIContent content, Action onClick)
		{
			AddButton(content, onClick, ButtonType.Button, null);
		}

		public void AddButton(string label, Action onClick, ButtonType type)
		{
			AddButton(label, onClick, type, null);
		}

		public void AddButton(GUIContent content, Action onClick, ButtonType type)
		{
			AddButton(content, onClick, type, null);
		}

		public void AddButton(string label, Action onClick, ButtonType type, Func<bool> isDisabledCallback)
		{
			AddButton(new GUIContent(label), onClick, type, isDisabledCallback);
		}

		public void AddButton(GUIContent content, Action onClick, ButtonType type = ButtonType.Button, Func<bool> isDisabledCallback = null)
		{
			ButtonElement button;

			switch (type) {
				case ButtonType.Dropdown:
					button = new DropdownElement();
					break;
				case ButtonType.Popup:
					button = new PopupElement();
					break;
				default:
					button = new ButtonElement();
					break;
			}

			button.content = content;
			button.OnClick = onClick;
			if (isDisabledCallback != null) {
				button.isDisabledCallback = isDisabledCallback;
			}
			_elements.Add(button);
		}

		public void AddInputField(string label, string text, Action<string> onValueChange)
		{
			AddInputField(new GUIContent(label), text, onValueChange, InputFieldType.TextField, null);
		}

		public void AddInputField(GUIContent content, string text, Action<string> onValueChange)
		{
			AddInputField(content, text, onValueChange, InputFieldType.TextField, null);
		}

		public void AddInputField(string label, string text, Action<string> onValueChange, InputFieldType type)
		{
			AddInputField(new GUIContent(label), text, onValueChange, type, null);
		}

		public void AddInputField(GUIContent content, string text, Action<string> onValueChange, InputFieldType type)
		{
			AddInputField(content, text, onValueChange, type, null);
		}

		public void AddInputField(string label, string text, Action<string> onValueChange, InputFieldType type, Func<bool> isDisabledCallback)
		{
			AddInputField(new GUIContent(label), text, onValueChange, type, isDisabledCallback);
		}

		public void AddInputField(GUIContent content, string text, Action<string> onValueChange, InputFieldType type, Func<bool> isDisabledCallback)
		{
			TextFieldElement textField;

			switch (type) {
				case InputFieldType.SearchField:
					textField = new SearchFieldElement();
					break;
				default:
					textField = new TextFieldElement();
					break;
			}

			textField.value = text;
			textField.content = content;
			textField.OnValueChanged = onValueChange;
			if (isDisabledCallback != null) {
				textField.isDisabledCallback = isDisabledCallback;
			}
			_elements.Add(textField);
		}

		public void AddToggle(string label, bool isOn, Action<bool> onValueChange)
		{
			AddToggle(label, isOn, onValueChange, null, null);
		}

		public void AddToggle(GUIContent content, bool isOn, Action<bool> onValueChange)
		{
			AddToggle(content, isOn, onValueChange, null, null);
		}

		public void AddToggle(string label, bool isOn, Action<bool> onValueChange, GUIContent offContent)
		{
			AddToggle(label, isOn, onValueChange, offContent, null);
		}

		public void AddToggle(GUIContent content, bool isOn, Action<bool> onValueChange, GUIContent offContent)
		{
			AddToggle(content, isOn, onValueChange, offContent, null);
		}

		public void AddToggle(string label, bool isOn, Action<bool> onValueChange, GUIContent offContent, Func<bool> isDisabledCallback)
		{
			AddToggle(new GUIContent(label), isOn, onValueChange, offContent, isDisabledCallback);
		}

		public void AddToggle(GUIContent content, bool isOn, Action<bool> onValueChange, GUIContent offContent, Func<bool> isDisabledCallback)
		{
			ToggleElement toggle = new ToggleElement() {
				content = content,
				value = isOn,
				offContent = offContent,
				OnValueChanged = onValueChange,
			};

			if (isDisabledCallback != null) {
				toggle.isDisabledCallback = isDisabledCallback;
			}
			_elements.Add(toggle);
		}

		public void Draw()
		{
			using (new MenuBarScope()) {
				foreach (var element in _elements) {
					element.Draw();
				}
			}
		}

	}
}
