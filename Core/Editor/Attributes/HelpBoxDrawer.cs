using UnityEngine;
using UnityEditor;

/*===============================================================
Project:	MSD - Core
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:		08/04/2019 19:48
===============================================================*/

namespace MSD.Editor 
{
	[CustomPropertyDrawer(typeof(HelpBoxAttribute))]
	public class HelpBoxDrawer : DecoratorDrawer 
	{
		private static readonly float MINIMUM_BOX_HEIGHT = 38f;
		private static readonly float TOP_SPACING = 8f;
		private static readonly float BOTTOM_SPACING = EditorGUIUtility.standardVerticalSpacing;
		private static readonly float TOTAL_SPACING = TOP_SPACING + BOTTOM_SPACING;

		HelpBoxAttribute Attribute => attribute as HelpBoxAttribute;

		public override void OnGUI(Rect position) 
		{
			position.y += TOP_SPACING;
			position.height -= TOTAL_SPACING;
			EditorGUI.HelpBox(position, Attribute.Text, Attribute.Type);
		}

		public override float GetHeight()
		{
			var content = new GUIContent(Attribute.Text);
			var height = EditorStyles.helpBox.CalcHeight(content, EditorGUIUtility.currentViewWidth);

			height = Mathf.Max(MINIMUM_BOX_HEIGHT, height);

			height += TOTAL_SPACING;
			return height;
		}
	}
}
