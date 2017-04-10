using System;
using System.Collections.Generic;

namespace Quoridor
{
	public static class Extenstions
	{
		public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
		{
			foreach (var item in collection)
				action(item);
		}
	}
}