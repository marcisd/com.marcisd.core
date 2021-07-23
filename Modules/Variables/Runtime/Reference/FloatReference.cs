using System;

/*===============================================================
Project:	MSD - Core
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:       06/11/2018 15:56
===============================================================*/

namespace MSD
{
	[Serializable]
	public sealed class FloatReference : GenericReference<float, FloatCustomVariable>
	{
		public FloatReference(float value) : base(value) { }

		public FloatReference(FloatCustomVariable value) : base(value) { }
	}
}
