using UnityEngine;
using UnityEngine.Events;

/*===============================================================
Project:	Core Library
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:       19/11/2018 17:08
===============================================================*/

namespace MSD
{
	public class GameEventListener : MonoBehaviour
	{
		[SerializeField]
		private GameEvent _gameEvent;

		[SerializeField]
		private UnityEvent _response;

		private void OnEnable()
		{
			_gameEvent.RegisterListener(this);
		}

		private void OnDisable()
		{
			_gameEvent.UnregisterListener(this);
		}

		public void OnEventRaised()
		{
			_response.Invoke();
		}
	}
}
