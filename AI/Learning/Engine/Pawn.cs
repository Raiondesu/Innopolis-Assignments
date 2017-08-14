namespace Learning.Engine
{
	public class Pawn : Actor
	{
		public Pawn(Vector2D initialLocation) : base(initialLocation) {}
		public Pawn(Pawn copy) : base(copy)
		{
			this.Score = copy.Score;
			this.LastValue = copy.LastValue;
		}

		public int Score { get; protected set; } = 0;
		public int LastValue { get; protected set; } = 0;

		public int MoveUp(Map map) => this.Move((0, 1), map);
		public int MoveDown(Map map) => this.Move((0, -1), map);
		public int MoveLeft(Map map) => this.Move((-1, 0), map);
		public int MoveRight(Map map) => this.Move((1, 0), map);

		private int Move(Vector2D direction, Map map)
		{
			if ((this.Location + direction).FitsIn(-1, map.Size))
			{
				this.Location += direction;
				var c = map.Actors.Find(a => a.Location == this.Location && a != this) as Collectable;
				if (c != default(Collectable) && c != null)
				{
					this.Score += c.Value;
					this.LastValue = c.Value;
					if (c.IsFinal) return 1;
				}
				return 0;
			}
			return -1;
		}
	}
}