using System.Collections.Generic;
using System;
using System.Linq;
using Random = UnityEngine.Random;

/*===============================================================
Project:	Core Library
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:       06/11/2018 16:15
===============================================================*/

namespace MSD
{
	public static class WeightedListExtensions
	{
		/// <summary>
		/// Selects the heaviest item from a weighted selection.
		/// </summary>
		/// <returns>The heaviest item.</returns>
		/// <param name="elements">A <see cref="List{T}"/> of elements to choose from.</param>
		/// <param name="getElementWeight">A delegate to retrieve the weight of a specific element.</param>
		public static T SelectHeaviestItem<T>(this IList<T> elements, Func<T, int> getElementWeight)
		{
			return elements.OrderByDescending(item => getElementWeight(item)).FirstOrDefault();
		}

		/// <summary>
		/// Select a random item from a weighted selection.
		/// </summary>
		/// <param name="elements">A <see cref="List{T}"/> of elements to choose from.</param>
		/// <param name="getElementWeight">A delegate to retrieve the weight of a specific element.</param>
		/// <returns>Random item.</returns>
		public static T RandomItemByWeight<T>(this IEnumerable<T> elements, Func<T, float> getElementWeight)
		{
			float totalWeight = elements.Sum(getElementWeight);
			float itemWeightIndex = Random.value * totalWeight;
			float currentWeightIndex = 0;

			foreach (T item in elements) {
				currentWeightIndex += getElementWeight(item);

				if (currentWeightIndex >= itemWeightIndex) {
					return item;
				}
			}

			return default;
		}
	}
}
