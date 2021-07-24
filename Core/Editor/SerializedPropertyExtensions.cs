using System.Reflection;
using System.Text.RegularExpressions;
using UnityEditor;

/*===============================================================
Project:	Core Library
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:		22/09/2019 10:22
===============================================================*/

namespace MSD.Editor
{
	public static class SerializedPropertyExtensions
	{
		public static bool TryGetArrayIndex(this SerializedProperty serializedProperty, out int index)
		{
			index = -1;
			var pathElements = serializedProperty.propertyPath.Split('.');
			if (pathElements.Length > 2 &&
				pathElements[pathElements.Length - 1].Contains("data[") &&
				pathElements[pathElements.Length - 2].Equals("Array")) {
				var resultString = Regex.Match(pathElements[pathElements.Length - 1], @"\d+").Value;
				index = int.Parse(resultString);
				return true;
			}
			return false;
		}

		public static object GetObjectInstance(this SerializedProperty serializedProperty)
		{
			object targetObject = serializedProperty.serializedObject.targetObject;

			BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
			string propertyPath = serializedProperty.propertyPath;
			FieldInfo fieldInfo = targetObject.GetType().GetField(propertyPath, bindingFlags);

			return fieldInfo.GetValue(targetObject);
		}
	}
}
