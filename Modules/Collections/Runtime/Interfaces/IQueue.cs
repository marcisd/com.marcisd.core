using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*===============================================================
Project:	Core Library
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:		29/01/2020 20:35
===============================================================*/

namespace MSD 
{
	public interface IQueue<T> : ILimitedAccessLinearCollection<T>
	{
		void Enqueue(T item);
		T Dequeue();
	}
}
