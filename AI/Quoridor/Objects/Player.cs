using System.Collections.Generic;
using System.Linq;

namespace Quoridor
{
	public class Player
	{
		protected Vector2D _location = new Vector2D();
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

		public virtual bool TryMove(Vector2D direction, int board, List<Wall> walls, Player opponent)
		{
			direction = direction.Clamp(-1, 1);
			var newLoc = this.location + direction;

			if (walls.Any(w => w.LiesOn(newLoc)))
				return false;
			
			newLoc += direction;

			if (opponent.Location == newLoc)
				newLoc += direction * 2;

			bool result = false;
			if (walls.Any(w => w.LiesOn(newLoc)))
			{
				newLoc -= direction;
				if (!walls.Any(w => w.LiesOn(newLoc + direction.RotateLeft())))
				{
					newLoc += direction.RotateLeft() * 2;
					result = true;
				}
				else if (!walls.Any(w => w.LiesOn(newLoc + direction.RotateRight())))
				{
					newLoc += direction.RotateRight() * 2;
					result = true;
				}
				else return false;
			}

			newLoc = newLoc.Clamp(1, board - 1);
			this.location = newLoc;
			return result;
		}

		public virtual bool TryPlaceWall(Vector2D position, bool isVertical, ref List<Wall> walls)
		{
			if (walls.Any(w => w.LiesOn(position)))
				return false;

			this.WallsAmount--;
			this.Steps++;
			Wall wall = new Wall(position, isVertical);
			walls.Add(wall);
			this.Print(wall);
			
			return true;
		}

		public override string ToString() => $"{this.Name}";

		// override object.Equals
		public override bool Equals (object obj)
		{
			Player other = obj as Player ?? Player.Nobody;
			return this.Name == other.Name;
		}
		
		// override object.GetHashCode
		public override int GetHashCode() => base.GetHashCode();

		public static bool operator ==(Player p1, Player p2) => p1.Equals(p2);
		public static bool operator !=(Player p1, Player p2) => !(p1 == p2);

		public static Player Nobody => new Player(null);
	}
}