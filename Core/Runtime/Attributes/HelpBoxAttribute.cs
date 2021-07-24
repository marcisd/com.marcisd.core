using System;
using UnityEngine;

/*===============================================================
Project:	Core Library
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
		internal readonly string text;
		internal readonly MessageType type;

		public HelpBoxAttribute(string text)
			: this(text, MessageType.Info) { }
		
		public HelpBoxAttribute(string text, MessageType type)
		{
			this.text = text;
			this.type = type;
		}
	}

}
