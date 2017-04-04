using System.Collections.Generic;
using System.Linq;

namespace Quoridor
{
	public class Player
	{
		private Vector2D location = new Vector2D();

		public Player(string name, int depth = Settings.Depth, int wallsAmount = Settings.WallsAmount)
		{
			this.Name = string.IsNullOrEmpty(name) ? "Nobody" : name;
			this.WallsAmount = wallsAmount;
		}

		public virtual string Name { get; protected set; }
		public int Steps { get; private set; } = 0;
		public int Depth { get; private set; }
		public int WallsAmount { get; private set; }
		public List<Wall> Walls { get; } = new List<Wall>();
		public System.ConsoleColor Color { get; set; } = Settings.FieldColor;
		public Vector2D Location
		{
			get => location;
			set => this.location = this.Steps == 0 ? value : this.location;
		}

		public bool TryMove(Vector2D direction, int board, List<Wall> walls, Player opponent)
		{
			
			direction = direction.Clamp(-1, 1);
			var newLoc = this.location + direction;
			newLoc = newLoc.Clamp(0, board);

			if (walls.Any(w => w.LiesOn(newLoc)))
				return false;
			
			newLoc += direction;

			if (opponent.Location == newLoc)
				newLoc += direction;

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

			this.location = newLoc + direction;
			this.Steps++;
			return result;
		}

		public bool TryPlaceWall(Wall wall, ref List<Wall> walls)
		{
			
			return false;
		}

		public override string ToString() => $"{this.Name}";

		// override object.Equals
		public override bool Equals (object obj)
		{
			Player other = obj as Player ?? Player.Nobody;
			return this.Location == other.Location && this.Name == other.Name;
		}
		
		// override object.GetHashCode
		public override int GetHashCode() => base.GetHashCode();

		public static bool operator ==(Player p1, Player p2) => p1.Equals(p2);
		public static bool operator !=(Player p1, Player p2) => !(p1 == p2);

		public static Player Nobody => new Player(null);
	}
}