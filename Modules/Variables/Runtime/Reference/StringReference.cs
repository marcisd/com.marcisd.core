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
	public sealed class StringReference : GenericReference<string, StringCustomVariable>
	{
		public StringReference(string value) : base(value) { }

		public StringReference(StringCustomVariable value) : base(value) { }
	}
}
