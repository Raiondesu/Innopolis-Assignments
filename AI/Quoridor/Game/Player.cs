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
		public bool IsMaximizing { get;  set; }
		public ConsoleColor Color { get; set; }
		public string OpponentName { get; set; }

		public abstract Player Copy();
		public virtual void Turn(ref Board board, int delay = 0)
		{
			this.Steps++;
            if (delay > 0)
				System.Threading.Thread.Sleep(delay);
		}

		public List<Action<Board>> PossibleMoves(Vector2D direction, Board board)
		{
			var opponent = board.Players[this.OpponentName];
			var newLoc = this.location + direction;
			var result = new List<Action<Board>>();
			Func<Vector2D, Action<Board>> action = (loc) => (state) => {
				state.Players[this.Name].Location = loc;
			};

			if (board.Walls.Any(w => w.LiesOn(newLoc)) || !newLoc.FitsIn(board.Size))
				return new List<Action<Board>>();

			newLoc += direction;

			if (opponent.Location == newLoc)
			{
				newLoc += direction;
				if (board.Walls.Any(w => w.LiesOn(newLoc - direction)) || !(newLoc - direction).FitsIn(board.Size))
				{
					newLoc -= direction;
					if (!board.Walls.Any(w => w.LiesOn(newLoc + direction.RotateLeft())))
						result.Add(action(newLoc + direction.RotateLeft() * 2));
					if (!board.Walls.Any(w => w.LiesOn(newLoc + direction.RotateRight())))
						result.Add(action(newLoc + direction.RotateRight() * 2));
				}
				else result.Add(action(newLoc + direction));
			}
			else result.Add(action(newLoc));

			return result;
		}

		public List<Action<Board>> PossibleWalls(Vector2D position, bool isVertical, Board board)
		{
			var result = new List<Action<Board>>();

			var opponent = board.Players[this.OpponentName];

			if (this.WallsAmount == 0)
				return new List<Action<Board>>();

			Wall wall = new Wall(position, isVertical, this);
			if (!wall.Fits(board.Size) || board.Walls.Any(w => w.Intersects(wall)))
				return new List<Action<Board>>();

			result.Add((state) => {
				state.Walls.Add(wall);
				state.Players[this.Name].WallsAmount--;
			});
			
			if (!AStar.HasPath(this.location, this.Goal, board))
				return new List<Action<Board>>();
			if (!AStar.HasPath(opponent.location, opponent.Goal, board))
				return new List<Action<Board>>();

			return result;
		}
		
		
		public List<Action<Board>> AllMovesOn(Board board)
		{
			var moves = new List<Action<Board>>();

			moves.AddRange(this.PossibleMoves(Directions.Up, new Board(board)));
			moves.AddRange(this.PossibleMoves(Directions.Down, new Board(board)));
			moves.AddRange(this.PossibleMoves(Directions.Left, new Board(board)));
			moves.AddRange(this.PossibleMoves(Directions.Right, new Board(board)));

            // for (int y = 2; y <= board.Size - 2; y += 2)
			// 	for (int x = 2; x <= board.Size - 2; x += 2)
			// 	{
			// 		int _x = x;
			// 		int _y = y;
			// 		moves.AddRange(this.PossibleWalls((_x, _y), true, new Board(board)));
			// 		moves.AddRange(this.PossibleWalls((_x, _y), true, new Board(board)));
			// 	}

			//// Randomization
			// var rnd = new Random();
			// moves = moves.OrderBy(m => rnd.Next()).ToList();
			return moves;
		}

		public static bool operator ==(Player p1, Player p2) => p1?.Name?.Equals(p2?.Name) ?? false;
		public static bool operator !=(Player p1, Player p2) => !(p1 == p2);

		public sealed override bool Equals (object obj) => this?.Name == (obj as Player)?.Name;
		public sealed override int GetHashCode() => base.GetHashCode();
		public sealed override string ToString() => $"{this.Name} {this.Location}";
	}
}