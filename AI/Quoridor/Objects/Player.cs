using System.Collections.Generic;
using System.Linq;

namespace Quoridor
{
	public abstract class Player
	{
		private Vector2D _location = new Vector2D();
		protected Vector2D location
		{
			get => _location;
			set
			{
				this.Steps++;
				OldLocation = _location;
				_location = value;
				this.Print();
			}
		}

		public Player(string name, int depth = Settings.Depth, int wallsAmount = Settings.WallsAmount)
		{
			this.Name = string.IsNullOrEmpty(name) ? "Nobody" : name;
			this.WallsAmount = wallsAmount;
		}

		public virtual string Name { get; }
		public int Steps { get; protected set; } = 0;
		public int Depth { get; protected set; }
		public int WallsAmount { get; protected set; }
		public System.ConsoleColor Color { get; set; } = Settings.FieldColor;
		public Vector2D OldLocation { get; protected set; } = 0;
		public Vector2D Location
		{
			get => location;
			set
			{
				if (this.Steps == 0)
					this.location = value;
			}
		}
		
		public abstract void Turn(ref Board board, int delay = 0);

		protected virtual bool TryMove(Vector2D direction, int board, List<Wall> walls, Player opponent)
		{
			var newLoc = this.location + direction;

			if (walls.Any(w => w.LiesOn(newLoc)) || !newLoc.FitsIn(1, board - 1))
				return false;

			newLoc += direction;

			if (opponent.Location == newLoc)
			{
				newLoc += direction * 2;
				if (!newLoc.FitsIn(1, board - 1)) return false;
				if (walls.Any(w => w.LiesOn(newLoc - direction)))
				{
					newLoc -= direction * 2;
					if (!walls.Any(w => w.LiesOn(newLoc + direction.RotateLeft())))
						newLoc += direction.RotateLeft() * 2;
					else if (!walls.Any(w => w.LiesOn(newLoc + direction.RotateRight())))
						newLoc += direction.RotateRight() * 2;
					else
						return false;
				}
			}

			this.location = newLoc;
			return true;
		}

		protected virtual bool TryPlaceWall(Vector2D position, bool isVertical, ref Board board)
		{
			if (this.WallsAmount == 0) return false;

			Wall wall = new Wall(position, isVertical);
			if (!wall.Fits(board.Size) || board.Walls.Any(w => w.Intersects(wall)))
				return false;

			this.WallsAmount--;
			this.Steps++;
			board.Walls.Add(wall);
			this.Print(wall);
			
			return true;
		}

		public static bool operator ==(Player p1, Player p2) => p1?.Equals(p2) ?? false;
		public static bool operator !=(Player p1, Player p2) => !(p1 == p2);

		public override bool Equals (object obj) => this?.Name == (obj as Player)?.Name;
		public override int GetHashCode() => base.GetHashCode();
		public override string ToString() => $"{this.Name}";
	}
}