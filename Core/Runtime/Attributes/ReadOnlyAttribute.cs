using System;
using UnityEngine;

/*===============================================================
Project:	Core Library
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:       06/11/2018 15:34
===============================================================*/

namespace MSD
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
    public class ReadOnlyAttribute : PropertyAttribute { }
}
