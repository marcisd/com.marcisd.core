using System;
using UnityEngine;

/*===============================================================
Project:	MSD - Core
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:		08/04/2019 19:21
===============================================================*/

namespace MSD 
{
#if UNITY_EDITOR
	using MessageType = UnityEditor.MessageType;
#else
	public enum MessageType
    {
        None,
        Info,
        Warning,
        Error,
    }
#endif

	[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
	public class HelpBoxAttribute : PropertyAttribute
	{
		public readonly string text;
		public readonly MessageType type;

		public HelpBoxAttribute(string text, MessageType type = MessageType.Info)
		{
			this.text = text;
			this.type = type;
		}
	}

}