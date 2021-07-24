using System.Collections.Generic;
using System;
using System.Collections;
using Random = UnityEngine.Random;

/*===============================================================
Project:	Core Library
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:       06/11/2018 16:15
===============================================================*/

namespace MSD
{
	public static class ListExtensions
	{
		/// <summary>
		/// Gets random item from the list.
		/// </summary>
		/// <returns>Random item.</returns>
		/// <param name="list">A <see cref="List{T}"/> of elements to choose from.</param>
		public static T RandomItem<T>(this IList<T> list)
		{
			if (list == null) {
				throw new ArgumentNullException("List is null.");
			} else if (list.Count == 0) {
				throw new ArgumentException("Cannot get random item because list is empty");
			}
			return list[Random.Range(0, list.Count)];
		}

		/// <summary>
		/// Determines if the specified list is null or empty.
		/// </summary>
		/// <returns><c>true</c> if the specified list is null or empty; otherwise, <c>false</c>.</returns>
		/// <param name="list">A <see cref="List{T}"/> of elements.</param>
		public static bool IsNullOrEmpty(this IList list)
		{
			return list == null || list.Count == 0;
		}

		/// <summary>
		/// Swap the item from specified oldIndex with the item from the newIndex.
		/// </summary>
		/// <param name="list">A <see cref="List{T}"/> of elements.</param>
		/// <param name="oldIndex">Old index.</param>
		/// <param name="newIndex">New index.</param>
		public static void Swap(this IList list, int oldIndex, int newIndex)
		{
			var item = list[oldIndex];
			list[oldIndex] = list[newIndex];
			list[newIndex] = item;
		}

		/// <summary>
		/// Move the item from specified oldIndex to the newIndex.
		/// </summary>
		/// <param name="list">A <see cref="List{T}"/> of elements.</param>
		/// <param name="oldIndex">Old index.</param>
		/// <param name="newIndex">New index.</param>
		public static void Move(this IList list, int oldIndex, int newIndex)
		{
			var temp = list[oldIndex];
			for (int i = 0; i < list.Count - 1; i++) {
				if (i >= oldIndex) {
					list[i] = list[i + 1];
				}
			}
			for (int i = list.Count - 1; i > 0; i--) {
				if (i > newIndex) {
					list[i] = list[i - 1];
				}
			}
			list[newIndex] = temp;
		}

		/// <summary>
		/// Shuffle the list.
		/// </summary>
		/// <param name="list">A <see cref="List{T}"/> of elements.</param>
		public static void Shuffle(this IList list)
		{
			int n = list.Count;
			while (n > 1) {
				var k = Random.Range(0, n);
				n -= 1;
				if (n != k) {
					var value = list[k];
					list[k] = list[n];
					list[n] = value;
				}
			}
		}
	}
}
