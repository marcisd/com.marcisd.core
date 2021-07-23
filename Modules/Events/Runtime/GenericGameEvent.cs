using System;
using System.Collections.Generic;
using UnityEngine;

/*===============================================================
Project:	MSD - Core
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:		29/08/2019 01:26
===============================================================*/

namespace MSD
{
	public abstract class GenericGameEvent<T, TGameEventListener> : ScriptableObject
		where TGameEventListener : IGameEventListener<T>
	{
		[TextArea(2, 5)]
		[SerializeField]
		private string _developerDescription;

		private readonly List<TGameEventListener> _listeners = new List<TGameEventListener>();

		public event Action<T> OnRaise;

		public string DeveloperDescription => _developerDescription;

		public void Raise(T value)
		{
			for (int i = _listeners.Count - 1; i >= 0; i--) {
				_listeners[i].OnEventRaised(value);
			}

			OnRaise?.Invoke(value);
		}

		public void RegisterListener(TGameEventListener listener)
		{
			_listeners.Add(listener);
		}

		public void UnregisterListener(TGameEventListener listener)
		{
			_listeners.Remove(listener);
		}
	}
}
