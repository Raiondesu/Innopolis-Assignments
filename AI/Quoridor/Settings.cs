namespace Quoridor
{
	public static class Settings
	{
		public const int WallsAmount = 10;
		public const int BoardSize = 9;
		public const int Depth = 2;

		public const System.ConsoleColor FieldColor = System.ConsoleColor.White;
		public static readonly bool PrintFiledInline = true;
		
		public static readonly System.Type Algorithm = typeof(Algorithms.MinMax);
	}
}