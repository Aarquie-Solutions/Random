using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AarquieSolutions.Utility
{
    public static class Random
    {
		/// <summary>
		/// Return random bool value.
		/// </summary>
		public static bool Bool()
		{
			return UnityEngine.Random.Range(0, 2) == 0;
		}

		/// <summary>
		/// Return random item from item1 and item2 set.
		/// </summary>
		public static T Item<T>(T item1, T item2)
		{
			return Bool() ? item1 : item2;
		}

		/// <summary>
		/// Return random item from item1, item2 and item3 set.
		/// </summary>
		public static T Item<T>(T item1, T item2, T item3)
		{
			int n = UnityEngine.Random.Range(0, 3);
			return n switch
			{
				0 => item1,
				1 => item2,
				_ => item3
			};
		}

		/// <summary>
		/// Return a number between a range, this function calls UnityEngine.Random.Range
		/// </summary>
		/// <param name="min">Starting range, included</param>
		/// <param name="max">Ending range, not included</param>
		/// <returns></returns>
		public static int Int(int min, int max) => UnityEngine.Random.Range(min, max);
		
		/// <summary>
		/// Return a number between a range, this function calls UnityEngine.Random.Range
		/// </summary>
		/// <param name="min">Starting range, included</param>
		/// <param name="max">Ending range, included</param>
		/// <returns></returns>
		public static float Float(float min, float max) => UnityEngine.Random.Range(min, max);

		/// <summary>
		/// Return random item from array.
		/// </summary>
		public static T Array<T>(T[] array)
		{
			return array[UnityEngine.Random.Range(0, array.Length)];
		}

		/// <summary>
		/// Return random item from list.
		/// </summary>
		public static T Array<T>(List<T> list)
		{
			return list[UnityEngine.Random.Range(0, list.Count)];
		}

		/// <summary>
		/// Return random enum item.
		/// </summary>
		public static T Enum<T>()
		{
			var values = System.Enum.GetValues(typeof(T));
			return (T)values.GetValue(UnityEngine.Random.Range(0, values.Length));
		}

		/// <summary>
		/// Return random index of passed array. Index random selection is based on array weights.
		/// </summary>
		public static int WeightedIndex(int[] weights)
		{
			int randomPoint = UnityEngine.Random.Range(0, weights.Sum()) + 1;
			int sum = 0;
			for (int i = 0; i < weights.Length; i++)
			{
				sum += weights[i];
				if (randomPoint <= sum)
					return i;
			}
			throw new Exception("Logic error!");
		}

		/// <summary>
		/// Return random index of passed array. Index random selection is based on array weights.
		/// </summary>
		public static int WeightedIndex(float[] weights)
		{
			float randomPoint = UnityEngine.Random.Range(0f, weights.Sum());
			float sum = 0f;
			for (int i = 0; i < weights.Length; i++)
			{
				sum += weights[i];
				if (randomPoint <= sum)
					return i;
			}
			throw new Exception("Logic error!");
		}

		/// <summary>
		/// Return sub-list of random items from origin list without repeating.
		/// </summary>
		public static List<T> SubList<T>(IList<T> list, int count)
		{
			List<T> items = new List<T>();
			List<int> remainedIndexes = Enumerable.Range(0, list.Count).ToList();
			for (int i = 0; i < count; i++)
			{
				int selectedIndex = Array(remainedIndexes);
				remainedIndexes.Remove(selectedIndex);
				items.Add(list[selectedIndex]);
			}
			return items;
		}

		/// <summary>
		/// Shuffle list of items.
		/// </summary>
		public static void Shuffle<T>(this List<T> list)
		{
			for (int i = 1; i < list.Count; i++)
			{
				int indexRandom = UnityEngine.Random.Range(0, i + 1);
				(list[i], list[indexRandom]) = (list[indexRandom], list[i]);
			}
		}

		/// <summary>
		/// Shuffle array of items.
		/// </summary>
		public static void Shuffle<T>(T[] array)
		{
			for (int i = 1; i < array.Length; i++)
			{
				int indexRandom = UnityEngine.Random.Range(0, i + 1);
				(array[i], array[indexRandom]) = (array[indexRandom], array[i]);
			}
		}

		/// <summary>
		/// Return random point on line.
		/// </summary>
		public static Vector2 PointOnLine(Vector2 point1, Vector2 point2)
		{
			float t = UnityEngine.Random.Range(0f, 1f);
			return new Vector2(Mathf.Lerp(point1.x, point2.x, t), Mathf.Lerp(point1.y, point2.y, t));
		}

		/// <summary>
		/// Return random point on line.
		/// </summary>
		public static Vector3 PointOnLine(Vector3 point1, Vector3 point2)
		{
			float t = UnityEngine.Random.Range(0f, 1f);
			return new Vector3(Mathf.Lerp(point1.x, point2.x, t), Mathf.Lerp(point1.y, point2.y, t), Mathf.Lerp(point1.z, point2.z, t));
		}

		/// <summary>
		/// Get a chance with given percentage. If percentage is 25 it will return true each 4th time on an average.
		/// </summary>
		public static bool Chance(int percentage)
		{
			return UnityEngine.Random.Range(0, 100) + 1 <= percentage;
		}

		/// <summary>
		/// Gets a chance with give probability. If probability is 0.25 it will return true each 4th time on an average.
		/// </summary>
		public static bool Chance(float probability)
		{
			return UnityEngine.Random.Range(0f, 1f) < probability;
		}

		/// <summary>
		/// Get random normalized 2D direction as Vector2.
		/// </summary>
		public static Vector2 Direction()
		{
			return UnityEngine.Random.insideUnitCircle.normalized;
		}

		/// <summary>
		/// Return random point from rect bound (inside rect).
		/// </summary>
		public static Vector2 PointInRect(Rect rect)
		{
			return new Vector2(UnityEngine.Random.Range(rect.xMin, rect.xMax), UnityEngine.Random.Range(rect.yMin, rect.yMax));
		}

		/// <summary>
		/// Return random point on rect border (perimeter of rect).
		/// </summary>
		public static Vector2 PointOnRectBorder(Rect rect)
		{
			float perimeterLength = (rect.width + rect.height) * 2f;
			float pointOnPerimeter = UnityEngine.Random.Range(0f, perimeterLength);

			if (pointOnPerimeter < rect.width)//top border
				return new Vector2(rect.xMin + pointOnPerimeter, rect.yMax);

			pointOnPerimeter -= rect.width;

			if (pointOnPerimeter < rect.height)//right border
				return new Vector2(rect.xMax, rect.yMin + pointOnPerimeter);

			pointOnPerimeter -= rect.height;

			if (pointOnPerimeter < rect.width)//bottom border
				return new Vector2(rect.xMin + pointOnPerimeter, rect.yMin);

			pointOnPerimeter -= rect.width;

			//left border
			return new Vector2(rect.xMin, rect.yMin + pointOnPerimeter);
		}
    }
}
