namespace Quoridor
{
	public struct Wall
	{
		public Wall(Vector2D center, bool isVertical, Player owner = null)
		{
			this.Center = center;
			this.IsVertical = isVertical;
			this.Owner = owner;
			if (isVertical)
			{
				this.Start = center + Directions.Up;
				this.End = center + Directions.Down;
			}
			else
			{
				this.Start = center + Directions.Left;
				this.End = center + Directions.Right;
			}
		}

		public Vector2D Start { get; }
		public Vector2D Center { get; }
		public Vector2D End { get; }
		public bool IsVertical { get; }
		
		public Player Owner { get; }

		public bool Fits(Vector2D bounds)
			=> this.Center.FitsIn(bounds) && this.Center.X % 2 == 0 && this.Center.Y % 2 == 0;

		public bool LiesOn(Vector2D point)
			=> this.Start == point || this.Center == point || this.End == point;

		public bool Intersects(Wall other)
			=> this.LiesOn(other.Start) || this.LiesOn(other.Center) || this.LiesOn(other.End);

		public override string ToString()
			=> $"({Start}; {Center}; {End})";
	}
}