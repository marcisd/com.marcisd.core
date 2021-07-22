using System;
using UnityEngine;
using UnityEngine.SceneManagement;

/*===============================================================
Project:	MSD - Core
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:		28/11/2018 14:02
===============================================================*/

namespace MSD
{
	[CreateAssetMenu(menuName = "MSD/Core/Scene Loader", order = 500)]
	public class SceneLoader : ScriptableObject
	{
		[SerializeField]
		private SceneReference scene;

		public void LoadScene()
		{
			DoLoadScene(LoadSceneMode.Single);
		}

		public void LoadSceneAdditive()
		{
			DoLoadScene(LoadSceneMode.Additive);
		}

		public void LoadScene(LoadSceneMode mode, Action OnComplete)
		{
			AsyncOperation loadSceneAsyncOperation = DoLoadSceneAsync(mode);
			loadSceneAsyncOperation.completed += (asyncOperation) => OnComplete?.Invoke();
		}

		public void UnloadScene(Action OnComplete)
		{
			AsyncOperation unloadSceneAsyncOperation = DoUnloadSceneAsync();
			unloadSceneAsyncOperation.completed += (asyncOperation) => OnComplete?.Invoke();
		}

		public AsyncOperation LoadSceneAsync(LoadSceneMode mode)
		{
			return DoLoadSceneAsync(mode);
		}

		public AsyncOperation UnloadSceneAsync()
		{
			return DoUnloadSceneAsync();
		}

		private void DoLoadScene(LoadSceneMode mode)
		{
			Debug.Assert(scene != null, "Scene must not be null!");
			scene.LoadScene(mode);
		}

		private AsyncOperation DoLoadSceneAsync(LoadSceneMode mode)
		{
			Debug.Assert(scene != null, "Scene must not be null!");
			return scene.LoadSceneAsync(mode);
		}

		private AsyncOperation DoUnloadSceneAsync()
		{
			Debug.Assert(scene != null, "Scene must not be null!");
			return scene.UnloadSceneAsync();
		}
	}
}
