using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using Object = UnityEngine.Object;

/*===============================================================
Project:	Core Library
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:       15/11/2018 12:49
===============================================================*/

namespace MSD.Editor
{
	public partial class SerializableDictionaryDrawer
	{
		private ReorderableList _reorderableList;

		private void InitReorderableList()
		{
			if (_reorderableList != null) {
				return;
			}

			_reorderableList = new ReorderableList(_serializedObject, _keysProperty) {
				drawHeaderCallback = DrawHeaderCallback,
				drawNoneElementCallback = DrawNoneElementCallback,
				// TODO: fix this when C#9.0 is out
				drawElementCallback = (rect, index, x, _) => DrawElementCallback(rect, index),
				elementHeightCallback = ElementHeightCallback,
				onAddCallback = (_) => OnAddCallback(),
				onRemoveCallback = OnRemoveCallback,
				onReorderCallbackWithDetails = (_, oldIndex, newIndex) => OnReorderCallbackWithDetails(oldIndex, newIndex),
			};
		}

		private void DrawHeaderCallback(Rect rect)
		{
			EditorGUI.PrefixLabel(rect, new GUIContent(ObjectNames.NicifyVariableName(_dictProperty.name)));
		}

		private void DrawNoneElementCallback(Rect rect)
		{
			EditorGUI.LabelField(rect, "Dictionary is Empty");
		}

		private void DrawElementCallback(Rect rect, int index)
		{
			SerializedProperty keyElt = _keysProperty.GetArrayElementAtIndex(index);
			SerializedProperty valueElt = _valuesProperty.GetArrayElementAtIndex(index);
			float fullWidth = rect.width;
			rect.y += EditorGUIUtility.standardVerticalSpacing;
			rect.height = EditorGUIUtility.singleLineHeight;
			rect.width = fullWidth * KeyWidthRatio;

			using (EditorGUI.ChangeCheckScope changeCheck = new EditorGUI.ChangeCheckScope()) {
				_keyDrawerStrategy.DrawProperty(rect, keyElt);
				if (changeCheck.changed) {
					object newKey = _keyDrawerStrategy.GetPropertyValue(keyElt);
					if (!ContainsKey(newKey)) {
						_serializedObject.ApplyModifiedProperties();
						CacheKeys();
					} else {
						_displayError = true;
					}
				}
			}

			rect.x += rect.width + EditorGUIUtility.standardVerticalSpacing;
			rect.width = (fullWidth * ValueWidthRatio) - EditorGUIUtility.standardVerticalSpacing;

			using (EditorGUI.ChangeCheckScope changeCheck = new EditorGUI.ChangeCheckScope()) {
				EditorGUI.PropertyField(rect, valueElt, GUIContent.none);
				if (changeCheck.changed) {
					_serializedObject.ApplyModifiedProperties();
				}
			}
		}

		private void DrawFooterCallback(Rect rect)
		{
			rect.height = EditorGUIUtility.singleLineHeight;
			rect.width = rect.width / 3f;
			EditorGUI.LabelField(rect, "New Key");
			rect.x += rect.width;

			_temporaryKeyStrategy.DrawField(rect);

			rect.x += rect.width;
			if (GUI.Button(rect, "Add")) {
				_reorderableList.drawFooterCallback = null;
				_reorderableList.footerHeight = _footerHeightBackup;
				AddNewDictionaryItem();
			}
		}

		private float ElementHeightCallback(int index)
		{
			float keyHeight = EditorGUI.GetPropertyHeight(_keysProperty.GetArrayElementAtIndex(index));
			float valueHeight = EditorGUI.GetPropertyHeight(_valuesProperty.GetArrayElementAtIndex(index));
			float max = Mathf.Max(keyHeight, valueHeight);
			return max + EditorGUIUtility.standardVerticalSpacing;
		}

		private void OnAddCallback()
		{
			_reorderableList.drawFooterCallback = DrawFooterCallback;
			_footerHeightBackup = _reorderableList.footerHeight;
			_reorderableList.footerHeight = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

			_temporaryKeyStrategy.Reset();
		}

		private void OnRemoveCallback(ReorderableList list)
		{
			int index = list.index;

			if (GetKeyType().IsSubclassOf(typeof(Object))) {
				_keysProperty.GetArrayElementAtIndex(index).objectReferenceValue = null;
			}
			_keysProperty.DeleteArrayElementAtIndex(index);

			if (GetValueType().IsSubclassOf(typeof(Object))) {
				_valuesProperty.GetArrayElementAtIndex(index).objectReferenceValue = null;
			}
			_valuesProperty.DeleteArrayElementAtIndex(index);

			_serializedObject.ApplyModifiedProperties();
		}

		private void OnReorderCallbackWithDetails(int oldIndex, int newIndex)
		{
			_valuesProperty.MoveArrayElement(oldIndex, newIndex);
			_serializedObject.ApplyModifiedProperties();
		}
	}
}
