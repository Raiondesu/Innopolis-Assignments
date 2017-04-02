using System;

namespace Quoridor
{
	public static class Extenstions
	{
		public static object Construct(this Type type, params object[] args)
			=> Activator.CreateInstance(type, args);
	}
}