using UnityEngine;
using UnityEngine.Events;

/*===============================================================
Project:	Core Library
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:		29/08/2019 01:27
===============================================================*/

namespace MSD
{
	public abstract class GenericGameEventListener<T, TGameEvent> : MonoBehaviour,
		IGameEventListener<T>
		where TGameEvent : GenericGameEvent<T, IGameEventListener<T>>
	{
		[SerializeField]
		private TGameEvent _gameEvent;

		[SerializeField]
		private UnityEvent<T> _response;

		private void OnEnable()
		{
			_gameEvent.RegisterListener(this);
		}

		private void OnDisable()
		{
			_gameEvent.UnregisterListener(this);
		}

		void IGameEventListener<T>.OnEventRaised(T value)
		{
			_response.Invoke(value);
		}
	}
}
