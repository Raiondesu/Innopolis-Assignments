namespace Quoridor
{
	public struct Wall
	{
		public Wall(int x, int y, bool isVertical = false) : this(new Vector2D(x, y), isVertical) {}
		public Wall(Vector2D center, bool isVertical = false)
		{
			this.Center = center;
			this.IsVertical = isVertical;
			if (isVertical)
			{
				this.Start = center + Player.Up;
				this.End = center + Player.Down;
			}
			else
			{
				this.Start = center + Player.Left;
				this.End = center + Player.Right;
			}
		}

		public Vector2D Start { get; }
		public Vector2D Center { get; }
		public Vector2D End { get; }
		public bool IsVertical { get; }

		public bool Fits(Vector2D bounds)
			=> this.Start.FitsIn(bounds) && this.End.FitsIn(bounds) && this.Center.X % 2 == 0 && this.Center.Y % 2 == 0;

		public bool LiesOn(Vector2D point)
			=> this.Start == point || this.Center == point || this.End == point;

		public bool Intersects(Wall other)
			=> this.LiesOn(other.Start) || this.LiesOn(other.Center) || this.LiesOn(other.End);

		public override string ToString()
			=> $"({Start}, {Center}, {End})";
	}
}