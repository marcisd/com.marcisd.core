using System;

/*===============================================================
Project:	MSD - Core
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:       06/11/2018 15:55
===============================================================*/

namespace MSD
{
	[Serializable]
	public sealed class IntReference : GenericReference<int, IntCustomVariable>
	{
		public IntReference(int value) : base(value) { }

		public IntReference(IntCustomVariable value) : base(value) { }
	}
}
