using System;
using System.Collections.Generic;
using UnityEngine;

/*===============================================================
Project:	MSD - Core
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:       19/11/2018 17:08
===============================================================*/

namespace MSD
{
	[CreateAssetMenu(menuName = "MSD/Core/Events/Game Event", order = 0)]
	public class GameEvent : ScriptableObject
	{
		[TextArea(2, 5)]
		[SerializeField]
		private string _developerDescription;

		private readonly List<GameEventListener> _listeners = new List<GameEventListener>();

		public event Action OnRaise;

		public string DeveloperDescription => _developerDescription;

		public void Raise()
		{
			for (int i = _listeners.Count - 1; i >= 0; i--) {
				_listeners[i].OnEventRaised();
			}

			OnRaise?.Invoke();
		}

		public void RegisterListener(GameEventListener listener)
		{
			_listeners.Add(listener);
		}

		public void UnregisterListener(GameEventListener listener)
		{
			_listeners.Remove(listener);
		}
	}
}
