namespace Quoridor
{
	public static class Directions
	{
		public static Vector2D Up => new Vector2D(0, -1);
		public static Vector2D Down => new Vector2D(0, 1);
		public static Vector2D Right => new Vector2D(1, 0);
		public static Vector2D Left => new Vector2D(-1, 0);
	}
}