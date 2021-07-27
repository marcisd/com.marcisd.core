using UnityEngine;

/*===============================================================
Project:	Core Library
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:       14/11/2018 17:15
===============================================================*/

namespace MSD
{
	public static class UnityObjectExtensions
	{
		/// <summary>
		/// Checks if this Unity Object has the same Instance ID with another Unity Object.
		/// </summary>
		/// <returns><c>true</c>, if instance id is equal, <c>false</c> otherwise.</returns>
		public static bool IsSameInstance (this Object thisObject, Object otherObject)
		{
			return thisObject.GetInstanceID () == otherObject.GetInstanceID ();
		}
	}
}