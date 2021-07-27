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
		public string OptionTrueLabel { get; private set; }
		public string OptionFalseLabel { get; private set; }
		public string LabelOverride { get; private set; }

		public SwitchToggleAttribute(string optionTrueLabel, string optionFalseLabel)
			: this (optionTrueLabel, optionFalseLabel, string.Empty) { }

		public SwitchToggleAttribute(string optionTrueLabel, string optionFalseLabel, string labelOverride)
		{
			OptionTrueLabel = optionTrueLabel;
			OptionFalseLabel = optionFalseLabel;
			LabelOverride = labelOverride;
		}
	}
}
