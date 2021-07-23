using UnityEngine;

/*===============================================================
Project:	MSD - Core
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:		18/03/2020 16:34
===============================================================*/

namespace MSD
{
	[CreateAssetMenu(menuName = "MSD/Core/Events/Float Game Event", order = 3)]
	public class FloatGameEvent : GenericGameEvent<float, IGameEventListener<float>> { }
}
