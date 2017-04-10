using System;
using System.Collections.Generic;
using System.Linq;

namespace Quoridor
{
	public abstract class Player
	{
		private Vector2D location;

		public Player(string name = null, int depth = Settings.Depth, int wallsAmount = Settings.WallsAmount)
		{
			this.Name = string.IsNullOrEmpty(name) ? "Nobody" : name;
			this.Depth = depth;
			this.WallsAmount = wallsAmount;
		}

		public string Name { get; }
		public int Depth { get; }
		public bool HasWon => this.Location.Y == this.Goal;

		public int Steps { get; protected set; }
		public int WallsAmount { get; protected set; }

		public Vector2D OldLocation { get; protected set; }
		
		public Vector2D Location
		{
			get => location;
			set
			{
				OldLocation = location;
				location = value;
			}
		}

		public int Goal { get; set; }
		public ConsoleColor Color { get; set; }
		public string OpponentName { get; set; }

		public List<Action<Board>> Moves { get; protected set; } = new List<Action<Board>>();

		public abstract Player Copy();
		public virtual void Turn(ref Board board, int delay = 0)
		{
			this.Steps++;
            if (delay > 0)
				System.Threading.Thread.Sleep(delay);
		}

		public bool TryMove(Vector2D direction, Board board, bool emergencyLeft = true)
		{
			var opponent = board.Players[this.OpponentName];

			var newLoc = this.location + direction;

			if (board.Walls.Any(w => w.LiesOn(newLoc)) || !newLoc.FitsIn(board.Size))
				return false;

			newLoc += direction;

			if (opponent.Location == newLoc)
			{
				newLoc += direction * 2;
				// if (!newLoc.FitsIn(board.Size)) return false;
				if (board.Walls.Any(w => w.LiesOn(newLoc - direction)) || !(newLoc - direction).FitsIn(board.Size))
				{
					newLoc -= direction * 2;
					if (emergencyLeft && !board.Walls.Any(w => w.LiesOn(newLoc + direction.RotateLeft())))
						newLoc += direction.RotateLeft() * 2;
					else if (!board.Walls.Any(w => w.LiesOn(newLoc + direction.RotateRight())))
						newLoc += direction.RotateRight() * 2;
					else
						return false;
				}
			}

			this.Location = newLoc;
			return true;
		}

		public bool TryPlaceWall(Vector2D position, bool isVertical, ref Board board)
		{
			var opponent = board.Players[this.OpponentName];

			if (this.WallsAmount == 0)
				return false;

			Wall wall = new Wall(position, isVertical, this);
			if (!wall.Fits(board.Size) || board.Walls.Any(w => w.Intersects(wall)))
				return false;

			board.Walls.Add(wall);
			
			if (!AStar.HasPath(this.location, this.Goal, board))
			{
				board.Walls.Remove(wall);
				return false;
			}
			if (!AStar.HasPath(opponent.location, opponent.Goal, board))
			{
				board.Walls.Remove(wall);
				return false;
			}

			this.WallsAmount--;
			return true;
		}

		public static bool operator ==(Player p1, Player p2) => p1?.Name?.Equals(p2?.Name) ?? false;
		public static bool operator !=(Player p1, Player p2) => !(p1 == p2);

		public sealed override bool Equals (object obj) => this?.Name == (obj as Player)?.Name;
		public sealed override int GetHashCode() => base.GetHashCode();
		public sealed override string ToString() => $"{this.Name}, {this.Location}";
	}
}