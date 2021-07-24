using System.Collections;
using System.Collections.Generic;

/*===============================================================
Project:	Core Library
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:		29/01/2020 21:38
===============================================================*/

namespace MSD
{
	public interface ILimitedAccessLinearCollection<T> :
		IEnumerable<T>, IEnumerable, IReadOnlyCollection<T>, ICollection
	{
		new int Count { get; }
		new IEnumerator<T> GetEnumerator();
		T Peek();
		void TrimExcess();
		void Clear();
		bool Contains(T item);
		void CopyTo(T[] array, int arrayIndex);
		T[] ToArray();
	}
}
