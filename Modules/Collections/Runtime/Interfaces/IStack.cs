using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*===============================================================
Project:	Core Library
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:		29/01/2020 21:19
===============================================================*/

namespace MSD 
{
	public interface IStack<T> : ILimitedAccessLinearCollection<T>
	{
		void Push(T item);
		T Pop();
	}
}
