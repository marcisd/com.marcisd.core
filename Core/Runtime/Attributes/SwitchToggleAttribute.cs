using System;
using UnityEngine;

/*===============================================================
Project:	Core Library
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:		20/09/2019 14:30
===============================================================*/

namespace MSD
{
	[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
	public class SwitchToggleAttribute : PropertyAttribute
	{
		internal readonly string optionTrueLabel;
		internal readonly string optionFalseLabel;

		public SwitchToggleAttribute(string optionTrueLabel, string optionFalseLabel)
		{
			this.optionTrueLabel = optionTrueLabel;
			this.optionFalseLabel = optionFalseLabel;
		}
	}
}
