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
	public sealed class BoolReference : GenericReference<bool, BoolCustomVariable>
	{
		public BoolReference(bool value) : base(value) { }

		public BoolReference(BoolCustomVariable value) : base(value) { }
	}
}
