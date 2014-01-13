using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SeriusSoft.Tools.Extensions
{
	public static class ExtensionMethods
	{
		public static DateTime FirstDayOfMonth(this DateTime date)
		{
			return new DateTime(date.Year, date.Month, 1);
		}

		public static DateTime LastDayOfMonth(this DateTime date)
		{
			return new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
		}

		public static IQueryable<T> WhereBetween<T,Y>(this IQueryable<T> source, Func<T,Y> searcheeProperty, Y startValue, Y endValue) where Y : IComparable<Y>
		{
			var valuesBetweenStartAndFinish = source.Where(s => searcheeProperty(s).GreaterThanOrEqual(startValue) && searcheeProperty(s).LessThanOrEqual(endValue));
			return valuesBetweenStartAndFinish;
		}

		public static void Alternate<T>(this IEnumerable<T> items, Action<T, bool> action)
		{
			var state = false;
			foreach (var item in items)
				action(item, state = !state);
		}

		public static void AlternateTitle<TModel>(this IEnumerable<TModel> models, string firstTitle, string secondTitle, Action<TModel, string> alternatingAction)
		{
			var isOnFirstTitle = false;
			var titles = new List<string>(){firstTitle, secondTitle};

			foreach (var model in models)
			{
				alternatingAction(model, titles[isOnFirstTitle ? 1 : 0]);
				isOnFirstTitle = !isOnFirstTitle;
			}
		}

		public static string TrimWithEllipsis(this string originalText, int maxLengthBeforeEllipsis = 50)
		{
			if (originalText == null || originalText.Length <= maxLengthBeforeEllipsis)
				return originalText;

			return String.Concat(originalText.Substring(0, maxLengthBeforeEllipsis), "...");
		}

		#region Comparables
		public static bool GreaterThan<T>(this T left, T right) where T : IComparable<T>
		{
			return left.CompareTo(right) > 0;
		}

		public static bool GreaterThanOrEqual<T>(this T left, T right) where T : IComparable<T>
		{
			return left.CompareTo(right) >= 0;
		}

		public static bool LessThan<T>(this T left, T right) where T : IComparable<T>
		{
			return left.CompareTo(right) < 0;
		}

		public static bool LessThanOrEqual<T>(this T left, T right) where T : IComparable<T>
		{
			return left.CompareTo(right) <= 0;
		}

		public static bool Equals<T>(this T left, T right) where T : IComparable<T>
		{
			return left.CompareTo(right) == 0;
		} 
		#endregion
	}
}
