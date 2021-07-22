using UnityEditor;
using UnityEngine;

/*===============================================================
Project:	MSD - Core
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:		25/07/2019 15:44
===============================================================*/

namespace MSD.Editor
{
	[CustomEditor(typeof(URLOpener))]
	public class URLOpenerInspector : UnityEditor.Editor
	{
		URLOpener URLOpener => target as URLOpener;

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			if (!URLOpener.IsValidURL) {
				EditorGUILayout.HelpBox(URLOpener.LOG_ERROR_INVALIDURL, MessageType.Error);
			} else {
				if (!URLOpener.IsSecure) {
					EditorGUILayout.HelpBox(URLOpener.LOG_WARNING_NOTSECUREURL, MessageType.Warning);
				}
			}

			using (new EditorGUI.DisabledScope(!URLOpener.IsValidURL)) {
				if (GUILayout.Button("Open")) {
					URLOpener.Open();
				}
			}

		}
	}
}
