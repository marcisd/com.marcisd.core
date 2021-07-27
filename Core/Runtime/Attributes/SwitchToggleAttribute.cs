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
		public readonly string optionTrueLabel;
		public readonly string optionFalseLabel;
		public readonly string labelOverride;

		public SwitchToggleAttribute(string optionTrueLabel, string optionFalseLabel)
			: this (optionTrueLabel, optionFalseLabel, string.Empty) { }

		public SwitchToggleAttribute(string optionTrueLabel, string optionFalseLabel, string labelOverride)
		{
			this.optionTrueLabel = optionTrueLabel;
			this.optionFalseLabel = optionFalseLabel;
			this.labelOverride = labelOverride;
		}
	}
}
