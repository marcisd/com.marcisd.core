using UnityEngine;

/*===============================================================
Project:	Core Library
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:		18/03/2020 16:34
===============================================================*/

namespace MSD
{
	[CreateAssetMenu(menuName = "MSD/Core/Events/String Game Event", order = 1)]
	public class StringGameEvent : GenericGameEvent<string, IGameEventListener<string>> { }
}
