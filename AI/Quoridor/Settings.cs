namespace Quoridor
{
	public static class Settings
	{
		public const int WallsAmount = 100;
		public const int BoardSize = 9;
		public const int Depth = 1;
		public const bool Log = false;

		public const System.ConsoleColor FieldColor = System.ConsoleColor.White;
		public const bool ColorizeWalls = true;
		public static readonly System.ConsoleColor[] PlayerColors = new [] 
		{
			System.ConsoleColor.Blue,
			System.ConsoleColor.Cyan,
			System.ConsoleColor.DarkBlue,
			System.ConsoleColor.DarkCyan,
			System.ConsoleColor.DarkGreen,
			System.ConsoleColor.DarkRed,
			System.ConsoleColor.DarkYellow,
			System.ConsoleColor.Green,
			System.ConsoleColor.Magenta,
			System.ConsoleColor.Red,
			System.ConsoleColor.Yellow,
		};
	}
}