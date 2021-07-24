
/*===============================================================
Project:	Core Library
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:		29/08/2019 01:27
===============================================================*/

namespace MSD
{
	public interface IGameEventListener<in T>
	{
		void OnEventRaised(T value);
	}
}

