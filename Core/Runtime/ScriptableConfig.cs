using UnityEngine;
using System.Diagnostics;

#if UNITY_EDITOR
using System.IO;
using UnityEditor;
#endif

/*===============================================================
Project:	Core Library
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:		05/04/2019 19:59
===============================================================*/

namespace MSD
{
	public abstract class ScriptableConfig<T> : ScriptableObject
		where T : ScriptableConfig<T>
	{
		private static T s_instance;
		public static T Instance {
			get {
				if (s_instance == null) {
					string configResourcesPath = typeof(T).ToString();
					s_instance = Resources.Load<T>(configResourcesPath);

					if (s_instance == null) {
						s_instance = CreateInstance<T>();

#if UNITY_EDITOR
						string configFullPath = CreateConfigAssetPath("Resources");
						CreateConfigAsset(s_instance, configFullPath);
#endif
					}
				}
				return s_instance;
			}
		}

		[Conditional("UNITY_EDITOR")]
		protected static void DirtyEditor()
		{
			EditorUtility.SetDirty(Instance);
		}

#if UNITY_EDITOR

		private static string CreateConfigAssetPath(string subPath)
		{
			string assetName = $"{typeof(T)}.asset";
			string relativePath = CreateRelativePath(subPath);
			return Path.Combine(relativePath, assetName);
		}

		private static string CreateRelativePath(string path)
		{
			string fullPath = Path.Combine(Application.dataPath, path);
			if (!Directory.Exists(fullPath)) { Directory.CreateDirectory(fullPath); }
			return Path.Combine("Assets", path);
		}

		private static void CreateConfigAsset(T configInstance, string path)
		{
			AssetDatabase.CreateAsset(configInstance, path);
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();
		}

#endif

	}
}
