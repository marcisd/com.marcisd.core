using System;
using UnityEngine;

/*===============================================================
Project:	MSD - Core
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:		06/05/2020 10:04
===============================================================*/

namespace MSD 
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public class SerializableDictionaryKeyWidthRatioAttribute : Attribute
	{
		public float KeyWidthRatio { get; private set; }

		public SerializableDictionaryKeyWidthRatioAttribute(float keyWidthRatio)
		{
			KeyWidthRatio = Mathf.Clamp01(keyWidthRatio);
		}
	}
}
