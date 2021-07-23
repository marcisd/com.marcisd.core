using UnityEngine;

/*===============================================================
Project:	MSD - Core
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:		29/08/2019 01:50
===============================================================*/

namespace MSD
{
	[CreateAssetMenu(menuName = "MSD/Core/Events/Int Game Event", order = 2)]
	public class IntGameEvent : GenericGameEvent<int, IGameEventListener<int>> { }
}
