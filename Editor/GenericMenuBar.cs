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
		private List<Element> _elements;

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

		public void AddLabel(GUIContent content, LabelType type = LabelType.Regular, Func<bool> isDisabledCallback = null)
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

		public void AddInputField(GUIContent content, string text, Action<string> onValueChange, InputFieldType type = InputFieldType.TextField, Func<bool> isDisabledCallback = null)
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

		public void AddToggle(GUIContent content, bool isOn, Action<bool> onValueChange, GUIContent offContent = null, Func<bool> isDisabledCallback = null)
		{
			ToggleElement toggle = new ToggleElement() {
				content = content,
				value = isOn,
				OnValueChanged = onValueChange,
				offContent = offContent,
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
