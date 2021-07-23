using UnityEngine;

/*===============================================================
Project:	MSD - Core
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:		08/04/2020 23:41
===============================================================*/

namespace MSD
{
	[CreateAssetMenu(menuName = "MSD/Core/Events/Bool Game Event", order = 4)]
	public class BoolGameEvent : GenericGameEvent<bool, IGameEventListener<bool>> { }
}
