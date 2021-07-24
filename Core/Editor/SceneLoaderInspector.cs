using UnityEngine;
using UnityEditor;

/*===============================================================
Project:	Core Library
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:		04/07/2019 21:54
===============================================================*/

namespace MSD.Editor 
{
	[CustomEditor(typeof(SceneLoader))]
	public class SceneLoaderInspector : UnityEditor.Editor 
	{
		public override void OnInspectorGUI() 
		{
			base.OnInspectorGUI();
			EditorGUILayout.Space();
			if (GUILayout.Button("Load Scene")) {
				(target as SceneLoader).LoadScene();
			}
		}
	}
}
